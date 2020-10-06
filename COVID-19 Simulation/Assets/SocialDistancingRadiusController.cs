using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SocialDistancingRadiusController : MonoBehaviour
{
	public CircleCollider2D circleCol;
	
	private float radiusDistance;
	
	private TransmissionValueController ParentTransmissionValueHolderScript;

	void Start()
    {
        // Assign 'ParentTransmissionValueHolderScript' as the gameObject's parent-parent 'TransmissionValueController' script
		ParentTransmissionValueHolderScript = (TransmissionValueController) gameObject.GetComponentInParent(typeof(TransmissionValueController));
    }

    void Update()
    {
		// Assign distance from the value holder script
        radiusDistance = ParentTransmissionValueHolderScript.SocialDistancingDistance;
		
		// Change the circleCollider2D radius to the radiusDistance value from the value holder script
		circleCol.radius = radiusDistance;
    }
}
