using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionObjectController : MonoBehaviour
{
	public bool IsInTransmissionRange;
	public bool IsInfected;
	public bool IsDead;
	public bool IsRecovered;
	
	private TransmissionValueController ParentTransmissionValueHolderScript;
	
	SpriteRenderer spriteRenderer;
	
    // Start is called before the first frame update
    void Start()
    {
        // Assign 'InTransmissionRange' as false on start
		IsInTransmissionRange = false;
		
		// Assign 'IsInfected' as false on start
		IsInfected = false;
		
		// Assign 'IsDead' as false on start
		IsDead = false;
		
		// Assign 'IsRecovered' as false on start
		IsRecovered = false;
		
		// Assign 'spriteRenderer' as the gameObject's Sprite Renderer on start
		spriteRenderer = GetComponent<SpriteRenderer>();
		
		// Assign 'ParentTransmissionValueHolderScript' as the gameObject's parent 'TransmissionValueController' script
		ParentTransmissionValueHolderScript = (TransmissionValueController) gameObject.GetComponentInParent(typeof(TransmissionValueController));
    }
	
	void FixedUpdate()
	{
		if (IsInfected)
		{
			// Change colour of gameObject to red
			spriteRenderer.color = new Color (255, 0, 0, 255);
		}
		
		if (IsInfected != true && IsDead != true && IsRecovered != true)
		{
			// Change colour of gameObject to white
			spriteRenderer.color = new Color (255, 255, 255, 255);
		}
		
		if (IsDead)
		{
			// Change colour of gameObject to grey
			spriteRenderer.color = new Color (255, 0, 255, 255);
			
			// Locate and assign Movement script
			var MovementController = (MovementController) gameObject.GetComponent(typeof(MovementController));
			
			// Disable wandering for the movement script
			MovementController.wander_enabled = false;
		}
		
		if (IsRecovered)
		{
			// Change colour of gameObject to blue
			spriteRenderer.color = new Color (0, 0, 255, 255);
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
	
    IEnumerator TransmissionTransferMethod()
	{
		while(IsInTransmissionRange && IsInfected != true && IsDead != true && IsRecovered != true)
		{	
			// Pick a random number between 1 - 100
			var randNum = Random.Range(1, 100);
			
			// If randNum is less than or equal to Transmission Chance, assign 'IsInfected' variable as true
			if (randNum <= ParentTransmissionValueHolderScript.TransmissionChance)
			{
				IsInfected = true;
				
				// Start TransmissionDecayMethod
				StartCoroutine(TransmissionDecayMethod());
				
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
	
	IEnumerator TransmissionDecayMethod()
	{	
		// Wait a random amount of time (between 8 and 12 seconds)
		yield return new WaitForSeconds(ParentTransmissionValueHolderScript.RecoveryTime);
		
		// Assign 'IsInfected' variable as false
		IsInfected = false;
		
		// Pick a random number between 1 - 100
		var randNum = Random.Range(1, 100);
			
		// If randNum is less than or equal to the Death Chance, assign 'IsDead' variable as true
		if (randNum <= ParentTransmissionValueHolderScript.DeathChance)
		{
			IsDead = true;
			yield break;
		}
		
		// If randNum is greater than to the Death Chance, assign 'IsRecovered' variable as true
		if (randNum > ParentTransmissionValueHolderScript.DeathChance)
		{
			IsRecovered = true;
			yield break;
		}
	}
}
