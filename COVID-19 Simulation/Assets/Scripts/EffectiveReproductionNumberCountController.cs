using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectiveReproductionNumberCountController : MonoBehaviour
{
    public EffectiveReproductionNumberCalculator rCalculatorScript;

    void FixedUpdate()
    {
        var r = rCalculatorScript.EffectiveAverageReproductiveNum;

        // Update the text to the r value
        gameObject.GetComponent<TMPro.TextMeshProUGUI>().text = "= " + r.ToString("F2");
    }
}
