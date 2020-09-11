using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulationChecker : MonoBehaviour
{
    public bool checkerEnabled = true;
	
	public DataExporter dataExporter;
	
	public float timeStamp;
   
   	public int infectedCount;
	public int nonInfectedCount;
	public int deadCount;
	public int recoveredCount;
	
    void Start()
	{
		// Start circleChecker coroutine
		StartCoroutine(CircleChecker());
		
		// Set timeStamp to 0
		timeStamp = 0f;
	}
	
	IEnumerator CircleChecker()
	{
		while(checkerEnabled != false)
		{
			// Reset count
			infectedCount = 0;
			nonInfectedCount = 0;
			deadCount = 0;
			recoveredCount = 0;
			
			// Add 0.5 to timeStamp
			timeStamp += 0.5f;
			
			// For every circle
			foreach(Transform child in transform)
			{
				// Locate and assign TransmissionObjectController script
				var TransmissionObjectController  = child.GetComponent(typeof(TransmissionObjectController)) as TransmissionObjectController;
				
				// If the circle is infected, add 1 to the count
				if(TransmissionObjectController.IsInfected == true)
					infectedCount += 1;
				
				// If the circle is not infected, dead or recovered, add 1 to the count
				if(TransmissionObjectController.IsInfected != true && TransmissionObjectController.IsDead != true && TransmissionObjectController.IsRecovered != true)
					nonInfectedCount += 1;
				
				// If the circle is dead, add 1 to the count
				if(TransmissionObjectController.IsDead == true)
					deadCount += 1;
				
				// If the circle is recovered, add 1 to the count
				if(TransmissionObjectController.IsRecovered == true)
					recoveredCount += 1;
			}
			
			// Put results into a csv file
			dataExporter.addRecord(timeStamp.ToString(), nonInfectedCount.ToString(), infectedCount.ToString(), deadCount.ToString(), recoveredCount.ToString());
			
			// Wait for 0.5 seconds (the refresh rate of the counter)
			yield return new WaitForSeconds(0.5f);
		}
	}
}
