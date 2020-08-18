using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectMovement : MonoBehaviour
{
	private Rigidbody2D rb;
	
	private bool wander_enabled;
	
	private float wander_speed;
	private float wander_direction;
	
	// Start is called before the first frame update
    void Start()
    {	
		// Assign rigidbody2D
		rb = GetComponent<Rigidbody2D>();
		
		// Assign wander to be enabled
		wander_enabled = true;
		
		// Start wandering
		StartCoroutine(Wander());

    }
	
	IEnumerator Wander()
	{
		// Pick a random direction on start
		transform.Rotate(new Vector3(0, 0, Random.Range(0f, 360f)));	
		
		// Pick a random speed on start
		wander_speed = Random.Range(0.80f, 1.20f);
		
		// Start initial movement
		rb.AddRelativeForce(transform.up * wander_speed, ForceMode2D.Impulse);
		
		while(wander_enabled)
		{
			// Reset velocity to 0
			rb.velocity = Vector2.zero;
			
			// Reset angular velocity to 0
			rb.angularVelocity = 0f;
			
			// Reset direction
			//transform.Rotate(new Vector3(0f, 0f, 0f));
			
			// Pick random direction
			wander_direction = Random.Range(0f, 360f);
			
			// Apply direction change
			transform.Rotate(new Vector3(0f, 0f, wander_direction));
			
			// Add force for movement
			rb.AddForce(transform.up * wander_speed, ForceMode2D.Impulse);

			// Wait for 0.5 seconds
			yield return new WaitForSeconds(2f);
		}
	}
}
