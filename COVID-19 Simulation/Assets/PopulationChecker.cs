using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationChecker : MonoBehaviour
{
    public bool checkerEnabled = true;
   
   	public int infectedCount;
	public int nonInfectedCount;
	public int deadCount;
	public int recoveredCount;
	
	private Component[] circleChildren;
	
    void Start()
	{
		// Start circleChecker coroutine
		StartCoroutine(CircleChecker());
		
		// TransmissionObjectController circleChildren = gameObject.GetComponentsInChildren(typeof(TransmissionObjectController)) as TransmissionObjectController;

		// circleChildren = GetComponentsInChildren<TransmissionObjectController>();
	}
	
	IEnumerator CircleChecker()
	{
		while(checkerEnabled != false)
		{
			infectedCount = 0;
			nonInfectedCount = 0;
			deadCount = 0;
			recoveredCount = 0;
			
			foreach(TransmissionObjectController child in circleChildren)
			{
				if(child.IsInfected == true)
					infectedCount += 1;
					
				if(child.IsInfected != true)
					nonInfectedCount += 1;
				
				if(child.IsDead == true)
					deadCount += 1;
				
				if(child.IsRecovered == true)
					recoveredCount += 1;
			}
			
			Debug.Log("Infected: " + infectedCount);
			Debug.Log("Not Infected: " + nonInfectedCount);
			Debug.Log("Dead: " + deadCount);
			Debug.Log("Recovered: " + recoveredCount);
			
			yield return new WaitForSeconds(3f);
		}
	}
}
