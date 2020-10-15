using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CounterController : MonoBehaviour
{
	public GameObject sliderTarget;
	
	public bool percentage;
	
	public bool seconds;
	
    // Update is called once per frame
    void Update()
    {
        // Assign 'counterText' as the value from the designated slider
		var counterText = sliderTarget.GetComponent<Slider>().value;
		
		if (percentage)
		{
			// Update text to match the value from the slider and add the percentage at the end
			gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = counterText.ToString() + " %";
		}
		
		else if (seconds)
		{
			// Update text to match the value from the slider and add the seconds at the end
			gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = counterText.ToString() + " secs";
		}
		
		else
		{
			// Update text to match the value from the slider
			gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = counterText.ToString("F1");
		}
    }
}
