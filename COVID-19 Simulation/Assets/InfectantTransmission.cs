using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfectantTransmission : MonoBehaviour
{	
	public Transform gameObjects;
	
	private Transform circles;
	
	SpriteRenderer spriteRenderer;

	
    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Assign components in circles
		circles = gameObjects.GetComponentInChildren<Transform>();
		
		foreach (Transform child in circles)
		{
			var distance = Vector2.Distance(child.transform.position, gameObject.transform.position);
			
			if (distance != 0 && distance < 1.5)
			{
				spriteRenderer.color = new Color (255, 0, 0, 255);
			}
			if (distance >= 1.5)
			{
				spriteRenderer.color = new Color (255, 255, 255, 255);
			}
		}
    }
}
