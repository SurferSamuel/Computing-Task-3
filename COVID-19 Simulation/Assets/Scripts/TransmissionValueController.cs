using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TransmissionValueController : MonoBehaviour
{

    // All values used throughout the simulation is stored here

    [Header("Initiate Spread")]
    [Range(0, 300)]
    public int NumberOfCircles;
    public GameObject CirclePrefab;
    public bool InstantiateCircles;
    public bool MovementTrigger = false;
    public bool DiseaseTrigger = false;
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
    [Range(0.0f, 1.0f)]
    public float SocialDistancingSpeed;

    [Header("Sliders")]
    public GameObject slider1;
    public GameObject slider2;
    public GameObject slider3;
    public GameObject slider4;
    public GameObject slider5;
    public GameObject slider6;
    public GameObject slider7;
    public GameObject slider8;

    [HideInInspector]
	public PopulationChecker populationCheckerScript;



	// Functions used for the initial disease transmission
	
    void FixedUpdate()
    {
		// When the StartTrigger is initially clicked
        if (DiseaseTrigger != false && StartLoopTrigger != false)
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



    // Public functions used to the change values of the variables stored within this script via the UI toggles

    public void UIInstantiateCircles()
    {
        InstantiateCircles = true;

        var i = 0;

        while (i <= NumberOfCircles)
        {
            // Instantiate circles into the sim 
            Instantiate(CirclePrefab, gameObject.transform.position, Quaternion.identity, gameObject.transform);
            i += 1;
        }
    }

    public void UIMovementTrigger()
    {
        MovementTrigger = true;
    }

	public void UIDiseaseTrigger()
	{
        DiseaseTrigger = true;
	}

    // Update functions used to the change values of the variables stored within this script via the UI sliders

    void Update()
    {
        NumberOfCircles = (int)slider1.GetComponent<Slider>().value;

        TransmissionChance = (int)slider2.GetComponent<Slider>().value;

        TransmissionRadius = slider3.GetComponent<Slider>().value;

        RecoveryTime = (int)slider4.GetComponent<Slider>().value;

        DeathChance = (int)slider5.GetComponent<Slider>().value;

        SocialDistancingFactor = (int)slider6.GetComponent<Slider>().value;

        SocialDistancingDistance = slider7.GetComponent<Slider>().value;

        SocialDistancingSpeed = slider8.GetComponent<Slider>().value;
    }
}
