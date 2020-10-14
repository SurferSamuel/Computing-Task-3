using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionValueController : MonoBehaviour
{	

	// All values used throughout the simulation is stored here

	[Header("Initiate Spread")]
    public bool StartTrigger = false;
    private bool StartLoopTrigger = true;
    [Range(0.0f, 10.0f)]
    public float SquareBorderRadius;

    [Header("Transmission Parameters")]
	[Range(0, 100)]
	public int TransmissionChance;
	
	[Range(0.0f, 10.0f)]
	public float TransmissionRadius;
	
	[Header("Disease Parameters")]
	[Range(0, 100)]
	public int RecoveryTime;
	
	[Range(0, 100)]
	public int DeathChance;

    [Header("Human Parameters")]
    public bool SocialDistancing;
    [Range(0, 100)]
    public int SocialDistancingFactor;
    [Range(0.0f, 2.0f)]
	public float SocialDistancingDistance;
    [Range(0.0f, 10.0f)]
    public float SocialDistancingSpeed;

    [HideInInspector]
	public PopulationChecker populationCheckerScript;



	// Functions used for the initial disease transmission
	
    void Update()
    {
		// When the StartTrigger is initially clicked
        if (StartTrigger != false && StartLoopTrigger != false)
        {
            // Start function to randomly assign a circle the disease
            AssignInfectant();

            // Start the Population Checker Script
            populationCheckerScript.StartCoroutine("CircleChecker");

            // Assign variable 'StartLoopTrigger' to false so coroutine doesn't loop
            StartLoopTrigger = false;
        }
    }

    void AssignInfectant()
    {
        // Find how many circles/children there are
        int childCount = gameObject.transform.childCount;

        // Choose one of them
        var selectedChild = transform.GetChild(Random.Range(1, childCount));

        // Get the 'TransmissionObjectController' script of the child chosen
        var TransmissionObjectControllerScript = (TransmissionObjectController) selectedChild.GetComponent(typeof(TransmissionObjectController));

        // Infect the chosen child
        TransmissionObjectControllerScript.IsInfected = true;

        // Start recovery countdown
        TransmissionObjectControllerScript.StartCoroutine("TransmissionRecoveryMethod");
    }
	
	
	
	// Public functions used to the change values of the variables stored within this script via the UI sliders and toggles
	
	public void UIStartTrigger()
	{
		StartTrigger = true;
	}
}
