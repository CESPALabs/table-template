// ---------------------------------------
///	<summary>
/// 
/// File: startTrial.cs
/// Scene: ITI
/// Object: attach to every globaltrialTrigger Object
/// Purpose: detects whether a player object has collided with the trigger.
/// 
/// Functions: increases and sets playerPrefs for trialNumber trialNumbers
/// 
/// InputFrom: startTrialTrigger.cs
///  
/// 
/// </summary>
// ---------------------------------------
using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class startTrial : MonoBehaviour {

//	public float grabHeight;
	public int readyStart;
	public float trialCountdown;
	public int trialNumber;

	
	void Start (){

		trialNumber = PlayerPrefs.GetInt ("trialNumber"); // get the trial number
		trialCountdown = 300;
	}
//		
//
void Update () 
	{ 

		int trigger1ready = GameObject.Find("trialTrigger1").GetComponent<startTrialTrigger>().ready;
		int trigger2ready = GameObject.Find("trialTrigger2").GetComponent<startTrialTrigger>().ready;
		//int triggerNready = GameObject.Find("trialTriggerN").GetComponent<goTrigger>().ready;

		readyStart = trigger1ready + trigger2ready;

		if (readyStart == 2){ // (if readystart == N)
			GetComponent<SpriteRenderer>().materials[0].color = Color.green;
			trialCountdown--;

		}

		if (trialCountdown < 0){
			
			trialNumber++;
			PlayerPrefs.SetInt("trialNumber", trialNumber);

			SceneManager.LoadScene( "TableActive" );
		}
			
	}	
	
}