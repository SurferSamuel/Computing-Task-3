using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	private Rigidbody2D rb;
	
	public bool wander_enabled;
    private bool wander_enabled_trigger;
    private bool socialDistancingTrigger;

    private TransmissionValueController ParentTransmissionValueHolderScript;
    private float square_radius;
    private float central_radius;

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

        // Assign 'central_radius' as the value from the holder script
        central_radius = ParentTransmissionValueHolderScript.CentralBorderRadius;

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

            // Instantiate the circles into the simulation
            InstantiateCircles();
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

    void InstantiateCircles()
    {
        // If the Central Zone is disabled
        if (ParentTransmissionValueHolderScript.CentralZone == false)
        {
            // Pick a random position within the border
            var randPosX = Random.Range(square_radius, -(square_radius)) + transform.parent.position.x;
            var randPosY = Random.Range(square_radius, -(square_radius)) + transform.parent.position.y;

            // Move circle to random position
            transform.position = new Vector2(randPosX, randPosY);
        }

        // If the Central Zone is enabled
        if (ParentTransmissionValueHolderScript.CentralZone)
        {
            // Pick a random position within the border, but not including the central zone
            var randPosX = Random.Range(square_radius, -(square_radius)) + transform.parent.position.x;
            var randPosY = Random.Range(square_radius, -(square_radius)) + transform.parent.position.y;

            if (randPosX <= central_radius + transform.parent.position.x && randPosX >= -(central_radius) + transform.parent.position.x 
                && randPosY <= central_radius + transform.parent.position.y && randPosY >= -(central_radius) + transform.parent.position.y)
            {
                InstantiateCircles();
            }
            else
            {
                // Move circle to random position
                transform.position = new Vector2(randPosX, randPosY);
            }
        }
    }

    void Update()
    {
        if (wander_enabled && wander_enabled_trigger == false)
        {
            // Assign 'wander_enabled_trigger' as true (to prevent looping)
            wander_enabled_trigger = true;

            // Reset velocity to 0
            rb.velocity = Vector2.zero;

            // Reset angular velocity to 0
            rb.angularVelocity = 0f;

            // Restart coroutine to wander again
            StartCoroutine(Wander());
        }

        if (wander_enabled == false && wander_enabled_trigger)
        {
            // Assign 'wander_enabled_trigger' as false (to prevent looping)
            wander_enabled_trigger = false;

            // Reset velocity to 0
            rb.velocity = Vector2.zero;

            // Reset angular velocity to 0
            rb.angularVelocity = 0f;
        }

        // Notice:
        // There was an issue where if the user turned on then off social distancing the wander mechanics would mess up.
        // What happened was that while social distancing was turned on, the wander coroutine would keep on starting itself.
        // Thus over time the wander coroutine would accumulate, where there should only be 1 running instance.

        // I could prevent the signal being sent while social distancing is on to fix the issue, but then the social distancing itself also breaks down...
        // Hence the two if statements below provide a fix to the issue by simply stoping all currently running coroutines when the user turns off social distancing,
        // so that there is only one instance where the wander coroutine is running.

        // Although this is a fix, there is still a minor issue. While social distancing is enabled, the wander coroutine will still accumulate (thousands per second). 
        // This is using up unnecessary cpu processing power by calculating thousands of positions per frame, but as of right now it hasn't greatly affected the simulation.
        // If the issue persists, in the two if statements above include the parameters " && ParentTransmissionValueHolderScript.SocialDistancing == false" for the first
        // and " && ParentTransmissionValueHolderScript.SocialDistancing" for the second - then you can remove the if statements below.
        // Although this will fix the issue, as mentioned before, you'll see that the social distancing mechanics would work as well.

        if (ParentTransmissionValueHolderScript.SocialDistancing == false && socialDistancingTrigger)
        {
            // Assign socialDistancingTrigger as false (to prevent looping)
            socialDistancingTrigger = false;

            // Stop all current running coroutines
            StopAllCoroutines();
        }

        if (ParentTransmissionValueHolderScript.SocialDistancing && socialDistancingTrigger == false)
        {
            // Assign socialDistancingTrigger as true (to reset trigger and to prevent looping)
            socialDistancingTrigger = true;
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

            // Debug Test for Unity Console
            // Debug.Log("Wandering...");

			// Wait for random number seconds between 1.0 second and 3.0 seconds
			yield return new WaitForSeconds(Random.Range(1.0f, 3.0f));
		}

        if (wander_enabled != true)
        {
            // If wander is disabled, stop running the coroutine
            yield break;
        }
	}
}
