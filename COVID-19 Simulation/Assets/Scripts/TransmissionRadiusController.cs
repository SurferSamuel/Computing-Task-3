using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionRadiusController : MonoBehaviour
{	
	public CircleCollider2D transmissionCircle;
	
    // Start is called before the first frame update
    void Start()
    {
        // Assign Circle Collider on start
		transmissionCircle = GetComponent<CircleCollider2D>();
    }
	
	void OnTriggerEnter2D(Collider2D collisionInfo)
	{
		// When a gameObject enters the circle radius, the value InTransmissionRange is set 'true' for the gameObject that entered
		
		// Locate and assign the TransmissionObjectController Script of the gameObject that entered within Transmission Range to a new variable
		var TransmissionObjectControllerScript = (TransmissionObjectController) collisionInfo.GetComponentInParent(typeof(TransmissionObjectController));
		
		// If the current gameObject is infected, call the 'IsInTransmissionRange' function of the TransmissionObjectController script
		
		// Locate and assign the TransmissionObjectController Script of the current gameObject to a new variable
		var ParentTransmissionObjectControllerScript = (TransmissionObjectController) gameObject.GetComponentInParent(typeof(TransmissionObjectController));
		
		// If the current gameObject is infected and their tags match each other's
		if (ParentTransmissionObjectControllerScript.IsInfected == true && (collisionInfo.tag == transform.parent.tag))
		{
			// Call the 'IsInTransmissionRange' function of the TransmissionObjectController script of the object that entered the Transmission Range, and also pass on the name of the parent to this object
			TransmissionObjectControllerScript.InTransmissionRange(transform.parent.name);
		}
	}
	
	void OnTriggerExit2D(Collider2D collisionInfo)
	{
		// When a gameObject exits the circle radius, the value InTransmissionRange is set 'false' for the gameObject that entered
		
		// Locate and assign the TransmissionObjectController Script of the gameObject that entered within Transmission Range to a new variable
		var TransmissionObjectControllerScript = (TransmissionObjectController) collisionInfo.GetComponentInParent(typeof(TransmissionObjectController));

        // If their tags match each other's
        if ((collisionInfo.tag == transform.parent.tag))
        {
            // Call the 'IsNotInTransmissionRange' function of the TransmissionObjectController script
            TransmissionObjectControllerScript.NotInTransmissionRange();
        }	
	}
	
	void FixedUpdate()
	{
		// Locate and assign the variable 'RootParentTransmissionValueHolder' as the gameObject's value holder script
		var RootParentTransmissionValueHolderScript = (TransmissionValueController) transform.parent.GetComponentInParent(typeof(TransmissionValueController));
		
		// Change transmission circle radius to value in value holder script
		transmissionCircle.radius = RootParentTransmissionValueHolderScript.TransmissionRadius;
	}
}
