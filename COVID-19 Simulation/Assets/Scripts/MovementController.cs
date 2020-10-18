using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{
	private Rigidbody2D rb;

    private float t;
    private float timeToReachTarget;
    private Vector2 centralZone;

    private float randPosX;
    private float randPosY;

    private bool MoveToCentralZoneTrigger;
    private bool MoveOutOfCentralZoneTrigger;

    public bool wander_enabled;
    private bool wander_enabled_trigger;
    private bool socialDistancingTrigger;

    private TransmissionValueController ParentTransmissionValueHolderScript;
    private TransmissionObjectController TransmissionObjectControllerScript;
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
        ParentTransmissionValueHolderScript = (TransmissionValueController) gameObject.GetComponentInParent(typeof(TransmissionValueController));

        // Assign 'TransmissionObjectControllerScript' as the gameObject's parent 'TransmissionObjectController' script
        TransmissionObjectControllerScript = (TransmissionObjectController) gameObject.GetComponentInParent(typeof(TransmissionObjectController));

        // Assign 'square_radius' as the value from the holder script
        square_radius = ParentTransmissionValueHolderScript.SquareBorderRadius;

        // Assign 'central_radius' as the value from the holder script
        central_radius = ParentTransmissionValueHolderScript.CentralBorderRadius;

        // Assign wander to be disabled before trigger has started
        wander_enabled = false;

        // Assign 'MovementTriggerLoop' to be true before trigger has started
        MovementTriggerLoop = true;

        // Assign 'InstantiateTriggerLoop' to be true before trigger has started
        InstantiateTriggerLoop = true;

        // Assign 'MoveToCentralZoneTrigger' to be false before trigger has started
        MoveToCentralZoneTrigger = false;

        // Assign 'MoveOutOfCentralZoneTrigger' to be false before trigger has started
        MoveOutOfCentralZoneTrigger = false;

        // Assign 'centralZone' as the middle of the simulation area
        centralZone = new Vector2(transform.parent.position.x, transform.parent.position.y);

        // Assign 'timeToReachTarget' as 2 (seconds)
        timeToReachTarget = 2;
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

        if (MoveToCentralZoneTrigger)
        {
            if (t < 0.1)
            {
                // If the MoveToCentralZoneTrigger is true and t is less than 1, move the circle towards the central zone
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector2.Lerp(transform.position, centralZone, t);
            }
            else
            {
                // If t >= 1, it must mean the circle has reached the central zone

                // Disable the MoveToCentralZoneTrigger
                MoveToCentralZoneTrigger = false;

                // Reset the value of t
                t = 0f;

                // Enable wandering again
                wander_enabled = true;
                StartCoroutine(Wander());

                // Start CentralZoneCountdown in the TransmissionObjectControllerScript
                TransmissionObjectControllerScript.StartCoroutine("CentralZoneCountdown");
            }
        }

        if (MoveOutOfCentralZoneTrigger)
        {
            if (t < 0.1)
            {
                // If the MoveToCentralZoneTrigger is true and t is less than 0.1, move the circle towards a random location
                t += Time.deltaTime / timeToReachTarget;
                transform.position = Vector2.Lerp(transform.position, new Vector2(randPosX, randPosY), t);
            }
            else
            {
                // If t >= 0.1, it must mean the circle has reached the random position

                // Disable the MoveToCentralZoneTrigger
                MoveOutOfCentralZoneTrigger = false;

                // Reset the value of t
                t = 0f;

                // Enable wandering again
                wander_enabled = true;
                StartCoroutine(Wander());

                // Change tag of the circle to wander
                gameObject.tag = "Wander";

                // Assign CentralZoneAllocationTrigger as true to allow circle to go to the central zone again
                TransmissionObjectControllerScript.CentralZoneAllocationTrigger = true;
            }
        }

        // Notice:
        // There was an issue where if the user turned on then off social distancing the wander mechanics would mess up.
        // What happened was that while social distancing was turned on, the wander coroutine would keep on starting itself.
        // Thus over time the wander coroutine would accumulate, where there should only be 1 running instance.

        // I could prevent the signal being sent while social distancing is on to fix the issue, but then the social distancing itself also breaks down...
        // Hence the two if statements below provide a fix to the issue by simply stoping all currently running coroutines when the user turns off social distancing,
        // so that there is only one instance where the wander coroutine is running.

        // Although this is a fix, there is still minor issues. While social distancing is enabled, the wander coroutine will still accumulate (thousands per second). 
        // This is using up unnecessary cpu processing power by calculating thousands of positions per frame, but as of right now it hasn't greatly affected the simulation.
        // Furthermore the StopAllCoroutines() function in the first if statement will interfer with other coroutines (not just the wander coroutine). Thus, for example, if
        // the function is called, any other coroutines that are running in the script will be stopped.

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

    void FixedUpdate()
    {
        if (ParentTransmissionValueHolderScript.InstantiateCircles && InstantiateTriggerLoop)
        {
            // Assign InstantiateTriggerLoop as false so it doesn't loop
            InstantiateTriggerLoop = false;

            // Instantiate the circle into the simulation
            InstantiateCircle();
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

    void InstantiateCircle()
    {
        // If the Central Zone is disabled
        if (ParentTransmissionValueHolderScript.CentralZone == false)
        {
            // Pick a random position within the border
            randPosX = Random.Range(square_radius, -(square_radius)) + transform.parent.position.x;
            randPosY = Random.Range(square_radius, -(square_radius)) + transform.parent.position.y;

            // Move circle to random position
            transform.position = new Vector2(randPosX, randPosY);
        }

        // If the Central Zone is enabled
        if (ParentTransmissionValueHolderScript.CentralZone)
        {
            // Pick a random position within the border, but not including the central zone
            randPosX = Random.Range(square_radius, -(square_radius)) + transform.parent.position.x;
            randPosY = Random.Range(square_radius, -(square_radius)) + transform.parent.position.y;

            // If random position is inside the central zone
            if (randPosX <= central_radius + transform.parent.position.x && randPosX >= -(central_radius) + transform.parent.position.x 
                && randPosY <= central_radius + transform.parent.position.y && randPosY >= -(central_radius) + transform.parent.position.y)
            {
                // Loop function until random position is not inside the central zone
                InstantiateCircle();
            }
            else
            {
                // Move circle to the random position
                transform.position = new Vector2(randPosX, randPosY);
            }
        }
    }

    public void MoveToCentralZone()
    {
        // Disable wandering
        wander_enabled = false;

        // Change tag of the circle to central zone
        gameObject.tag = "Central Zone";

        // Set t as 0
        t = 0f;

        // Assign MoveToCentralZoneTrigger as true so that the Update funtion will start to move the circle
        MoveToCentralZoneTrigger = true;
    }

    public void MoveOutOfCentralZone()
    {
        // Disable wandering
        wander_enabled = false;

        // Set t as 0
        t = 0f;

        // Assign MoveToCentralZoneTrigger as true so that the Update funtion will start to move the circle
        MoveOutOfCentralZoneTrigger = true;
    }

    IEnumerator Wander()
	{
        // Pick a random direction on start
        transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)));	
		
		// Pick a random speed on start
		var wander_speed = Random.Range(0.40f, 0.80f);
		
		// Start initial movement
		rb.AddForce(transform.up * wander_speed, ForceMode2D.Impulse);
		
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
