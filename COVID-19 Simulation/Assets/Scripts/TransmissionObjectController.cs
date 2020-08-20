using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionObjectController : MonoBehaviour
{
	public bool IsInTransmissionRange;
	public bool IsInfected;
	
	SpriteRenderer spriteRenderer;
	
    // Start is called before the first frame update
    void Start()
    {
        // Assign 'InTransmissionRange' as false on start
		IsInTransmissionRange = false;
		
		// Assign 'IsInfected' as false on start
		IsInfected = false;
		
		// Assign 'spriteRenderer' as the Object's Sprite Renderer on start
		spriteRenderer = GetComponent<SpriteRenderer>();
    }
	
	void FixedUpdate()
	{
		if(IsInfected)
		{
			// Change colour of gameObject to red
			spriteRenderer.color = new Color (255, 0, 0, 255);
		}
		
		if(IsInfected != true)
		{
			// Change colour of gameObject to white
			spriteRenderer.color = new Color (255, 255, 255, 255);
		}
	}
	
    IEnumerator TransmissionTransferMethod()
	{
		while(IsInTransmissionRange && IsInfected != true)
		{
			// Pick a random number between 0 - 9
			var randNum = Random.Range(0, 9);
			
			// If randNum is 0, assign 'IsInfected' variable as true
			if (randNum == 0)
			{
				IsInfected = true;
				
				// End loop
				yield break;
			}
			
			// If randNum is not 0, loop again after 0.5 seconds
			if (randNum != 0)
			{
				yield return new WaitForSeconds(0.5f);
			}
		}
	}
	
	public void InTransmissionRange()
	{	
		// This function is called from the TransmissionRadiusContoller Script
		
		// Assign 'InTransmissionRange' as true
		IsInTransmissionRange = true;
		
		// Start coroutine 'TransmissionTransferMethod'
		StartCoroutine(TransmissionTransferMethod());
	}
	
	public void NotInTransmissionRange()
	{
		// This function is called from the TransmissionRadiusContoller Script
		
		// Assign 'InTransmissionRange' as false
		IsInTransmissionRange = false;
	}
}
