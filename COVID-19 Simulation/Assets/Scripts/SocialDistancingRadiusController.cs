﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistancingRadiusController : MonoBehaviour
{
    public CircleCollider2D circleCollider;

    private bool socialDistancing;
    private float radiusDistance;

    private TransmissionValueController ParentTransmissionValueHolderScript;
    private MovementController ParentMovementControllerScript;
    private TransmissionObjectController ParentTransmissionObjectControllerScript;
    
    private Transform nearest;

	void Start()
    {
        // Assign 'ParentTransmissionValueHolderScript' as the gameObject's parent-parent 'TransmissionValueController' script
		ParentTransmissionValueHolderScript = (TransmissionValueController) gameObject.GetComponentInParent(typeof(TransmissionValueController));

        // Assign 'ParentMovementControllerScript' as the gameObject's parent 'TransmissionMovementController' script
        ParentMovementControllerScript = (MovementController) gameObject.GetComponentInParent(typeof(MovementController));

        // Assign 'ParentTransmissionObjectControllerScript' as the gameObject's parent 'TransmissionObjectController' script
        ParentTransmissionObjectControllerScript = (TransmissionObjectController)gameObject.GetComponentInParent(typeof(TransmissionObjectController));
    }

    void Update()
    {
        // Assign 'socialDistancing' as the value from the holder script
        socialDistancing = ParentTransmissionValueHolderScript.SocialDistancing;

        // Assign 'radiusDistance' as the value from the holder script
        radiusDistance = ParentTransmissionValueHolderScript.SocialDistancingDistance;
		
		// Change the circleCollider2D radius to the 'radiusDistance'
		circleCollider.radius = radiusDistance;

        if (socialDistancing != true && ParentTransmissionObjectControllerScript.IsDead != true)
        {
            // Turn wander on
            ParentMovementControllerScript.wander_enabled = true;
        }
    }

    void OnTriggerStay2D(Collider2D collisionInfo)
    {
        if (socialDistancing != false && ParentTransmissionObjectControllerScript.IsDead != true)
        {
            // Turn off wandering (so it doesn't interfere with the socialDistancing forces)
            ParentMovementControllerScript.wander_enabled = false;

            // Assign 'closestDistance' as largest possible number 
            var closestDistance = Mathf.Infinity;

            // For each circle inside social distancing radius
            foreach (Transform obj in collisionInfo.transform)
            {
                // Assign 'distance' as the vector2 distance between the current circle and the circles within the social distancing radius
                var distance = Vector2.Distance(transform.position, collisionInfo.transform.position);

                // If 'distance' is closer than the 'closestDistance'
                if (distance < closestDistance)
                {
                    // Assign 'closestDistance' as the distance between the previous two circles
                    closestDistance = distance;

                    // Assign 'nearest' as the circle that is the closest
                    nearest = collisionInfo.transform;
                }
            }

            // Assign 'ParentTransmissionObjectControllerScript' as the gameObject's parent 'TransmissionObjectController' script
            var ParentTransmissionObjectControllerScript = (TransmissionObjectController) gameObject.GetComponentInParent(typeof(TransmissionObjectController));

            // Send the ParentTransmissionObjectControllerScript the nearest circle
            ParentTransmissionObjectControllerScript.SocialDistancing(nearest);
        }
    }

    void OnTriggerExit2D(Collider2D collisionInfo)
    {
        // If gameObject is not 'dead'
        if (ParentTransmissionObjectControllerScript.IsDead != true)
        {
            // Turn wander back on
            ParentMovementControllerScript.wander_enabled = true;
        } 
    }
}