using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	private Rigidbody2D rb;
	
	public bool wander_enabled;
    private bool wander_enabled_trigger;

    private float square_radius;
    private TransmissionValueController ParentTransmissionValueHolderScript;

    private bool InstantiateTriggerLoop;
    private bool MovementTriggerLoop;

    // Start is called on the first frame
    void Start()
    {
        // Assign rigidbody2D
        rb = GetComponent<Rigidbody2D>();

        // Assign 'ParentTransmissionValueHolderScript' as the gameObject's parent 'TransmissionValueController' script
        ParentTransmissionValueHolderScript = (TransmissionValueController)gameObject.GetComponentInParent(typeof(TransmissionValueController));

        // Assign 'square_radius' as the value from the holder script
        square_radius = ParentTransmissionValueHolderScript.SquareBorderRadius;

        // Assign wander to be disabled before trigger has started
        wander_enabled = false;

        // Assign MovementTriggerLoop to be true before trigger has started
        MovementTriggerLoop = true;

        // Assign InstantiateTriggerLoop to be true before trigger has started
        InstantiateTriggerLoop = true;
    }

    void FixedUpdate()
    {
        if (ParentTransmissionValueHolderScript.InstantiateCircles && InstantiateTriggerLoop)
        {
            // Assign InstantiateTriggerLoop as false so it doesn't loop
            InstantiateTriggerLoop = false;

            // Pick a random position within the border
            var randPos = new Vector2(Random.Range(square_radius, -(square_radius)) + transform.parent.position.x, Random.Range(square_radius, -(square_radius)) + transform.parent.position.y);

            // Move circle to random position
            transform.position = randPos;
        }

        if (ParentTransmissionValueHolderScript.MovementTrigger && MovementTriggerLoop)
        {
            // Assign MovementTriggerLoop as false so it doesn't loop
            MovementTriggerLoop = false;

            // Assign wander to be enabled
            wander_enabled = true;

            // Assign 'wander_enabled_trigger' as true
            wander_enabled_trigger = true;

            // Start wandering
            StartCoroutine(Wander());
        }
    }
	
	IEnumerator Wander()
	{
		// Pick a random direction on start
		transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)));	
		
		// Pick a random speed on start
		var wander_speed = Random.Range(0.40f, 0.80f);
		
		// Start initial movement
		rb.AddRelativeForce(transform.up * wander_speed, ForceMode2D.Impulse);
		
		while(wander_enabled)
		{
			// Reset velocity to 0
			rb.velocity = Vector2.zero;
			
			// Reset angular velocity to 0
			rb.angularVelocity = 0f;
			
			// Reset direction
			transform.Rotate(new Vector3(0f, 0f, 0f));
			
			// Pick random direction
			var wander_direction = Random.Range(0f, 360f);
			
			// Apply direction change
			transform.Rotate(new Vector3(0f, 0f, wander_direction));
			
			// Add force for movement
			rb.AddForce(transform.up * wander_speed, ForceMode2D.Impulse);

			// Wait for random number seconds between 1.0 second and 3.0 seconds
			yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
		}
	}
	
	void Update()
	{
		if(wander_enabled == false && wander_enabled_trigger != false)
		{
            // Assign 'wander_enabled_trigger' as false
            wander_enabled_trigger = false;

            // Reset velocity to 0
            rb.velocity = Vector2.zero;
			
			// Reset angular velocity to 0
			rb.angularVelocity = 0f;
        }

        if(wander_enabled == true && wander_enabled_trigger != true)
        {
            // Assign 'wander_enabled_trigger' as true
            wander_enabled_trigger = true;

            // Reset velocity to 0
            rb.velocity = Vector2.zero;

            // Reset angular velocity to 0
            rb.angularVelocity = 0f;

            // Restart coroutine to wander again
            StartCoroutine(Wander());
        }
	}
}
