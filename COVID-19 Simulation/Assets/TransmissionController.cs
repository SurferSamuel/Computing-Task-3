using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionController : MonoBehaviour
{	
	
	public Transform gameObjects;
	private Transform circles;
	
	SpriteRenderer spriteRenderer;
	
	public float refreshRate;

	
    // Start is called before the first frame update
    void Start()
    {	
		// Assign spriteRenderer and the gameObject Sprite Renderer
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		// Start UpdateMethod
		StartCoroutine(UpdateMethod());
    }

    IEnumerator UpdateMethod()
    {
		// Repeat everything in while section forever
        while(true)
		{
			// Assign components in circles
			circles = gameObjects.GetComponentInChildren<Transform>();

			// For every circle
			foreach (Transform child in circles)
			{
				// Assign the variable distance as the distance between the current circle and another circle
				var distance = Vector2.Distance(child.transform.position, gameObject.transform.position);

				// If distance does not equal 0 (isn't itself) and is less than 1.5 units
				if (distance != 0 && distance < 1.5)
				{	
					// Change colour to red
					spriteRenderer.color = new Color (255, 0, 0, 255);
				}
				// If distance is greater than 1.5 units
				if (distance >= 1.5)
				{	
					// Change colour to white
					spriteRenderer.color = new Color (255, 255, 255, 255);
				}
			}
			
			// Repeat for number of 'refreshRate' times per seconds
			yield return new WaitForSeconds(1f / refreshRate);
		}
    }
}
