using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectiveReproductionNumberCalculator : MonoBehaviour
{
	public float EffectiveAverageReproductiveNum;
	
	private float EffectiveTempReproductiveNum;
	private float infectedCount;
	
    void Update()
    {	
		// Reset counters
		EffectiveTempReproductiveNum = 0f;
		infectedCount = 0f;
		
		// For every child
        foreach(Transform child in transform)
		{
			// Locate and assign TransmissionObjectController script
			var TransmissionObjectController = child.GetComponent(typeof(TransmissionObjectController)) as TransmissionObjectController;
			
			// If the circle is infected
			if(TransmissionObjectController.IsInfected == true && TransmissionObjectController.r != 0)
			{
				// Add value of r to the counter (EffectiveTempReproductiveNum)
				EffectiveTempReproductiveNum += TransmissionObjectController.r;
					
				// Add one the the infectedCounter
				infectedCount += 1;
			}
		}
		
		// Calculate the EffectiveAverageReproductiveNum by dividing the total effectivereproductivenum (EffectiveTempReproductiveNum) by the total number of infected circles (infectedCount)
		
		EffectiveAverageReproductiveNum = EffectiveTempReproductiveNum / infectedCount;
    }
}
