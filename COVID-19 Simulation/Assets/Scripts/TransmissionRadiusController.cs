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
		var TransmissionObjectControllerScript = (TransmissionObjectController) collisionInfo.GetComponent(typeof(TransmissionObjectController));
		
		// If the current gameObject is infected, call the 'IsInTransmissionRange' function of the TransmissionObjectController script
		
		// Locate and assign the TransmissionObjectController Script of the current gameObject to a new variable
		var ParentTransmissionObjectControllerScript = (TransmissionObjectController) gameObject.GetComponentInParent(typeof(TransmissionObjectController));
		
		// If the current gameObject is infected
		if (ParentTransmissionObjectControllerScript.IsInfected == true)
		{
			// Call the 'IsInTransmissionRange' function of the TransmissionObjectController script of the object that entered the Transmission Range
			TransmissionObjectControllerScript.InTransmissionRange();
		}
	}
	
	void OnTriggerExit2D(Collider2D collisionInfo)
	{
		// When a gameObject exits the circle radius, the value InTransmissionRange is set 'false' for the gameObject that entered
		
		// Locate and assign the TransmissionObjectController Script of the gameObject that entered within Transmission Range to a new variable
		var TransmissionObjectControllerScript = (TransmissionObjectController) collisionInfo.GetComponent(typeof(TransmissionObjectController));
		
		// Call the 'IsNotInTransmissionRange' function of the TransmissionObjectController script
		TransmissionObjectControllerScript.NotInTransmissionRange();	
	}
}
