using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartTriggerController : MonoBehaviour
{
	public Toggle StartTrigger;
	
    // Update is called once per frame
    void Update()
    {
		// When the toggle button is initially clicked
        if(StartTrigger.isOn != false && StartTrigger.interactable != false)
		{
			// Make the toggle button no longer interactable once clicked
			StartTrigger.interactable = false;
		}
    }
}
