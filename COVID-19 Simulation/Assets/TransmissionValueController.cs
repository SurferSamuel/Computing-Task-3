using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransmissionValueController : MonoBehaviour
{
    [Range(0, 100)]
	public int TransmissionChance;
	
	[Range(0.0f, 10.0f)]
	public float TransmissionRadius;
	
	[Range(0, 100)]
	public int RecoveryTime;
	
	[Range(0, 100)]
	public int DeathChance;
	
	
}
