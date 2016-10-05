// ---------------------------------------
///	<summary>
/// 
/// File: startTrial.cs
/// Scene: ITI
/// Object: attach to every trialTrigger Object
/// Purpose: detects whether a player object has collided with the trigger.
/// InputFrom: goTrigger.cs
/// ExportsTo: NA
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

//	public int trigger1ready;
//	public int ready;
//	public int trialNumber;
//	public float trialCountdown;
//	public bool trialStart = false;
//
//	//public GameObject touchedCircle;
//	//bool grab = false;
//
//	public bool OnStartTriggerEnter ( Collider myTrigger ) {
//
//		if (myTrigger.gameObject.tag == "startTrial"){
//
//			//			lastCoords = Vector3 movement;
//
//			Debug.Log("Player entered the trial trigger");	
//
//			trialStart = true;
//
////			trialNumber++;
////			PlayerPrefs.SetInt("trialNumber", trialNumber);
////			ready = 1;
////			trialCountdown--;
//
//			return trialStart;
//
//		} else {
//
//			//Debug.Log ("not hitting trigger");
//		}
//
//	}
	
	void Start (){

		trialNumber = PlayerPrefs.GetInt ("trialNumber"); // get the trial number
		trialCountdown = 300;
	}
//		
//
void Update () 
	{ 

		int trigger1ready = GameObject.Find("trialTrigger1").GetComponent<goTrigger>().ready;
		int trigger2ready = GameObject.Find("trialTrigger2").GetComponent<goTrigger>().ready;
		//int triggerNready = GameObject.Find("trialTriggerN").GetComponent<goTrigger>().ready;

		readyStart = trigger1ready + trigger2ready;

		if (readyStart == 2){ // (if readystart == N)
			GetComponent<SpriteRenderer>().materials[0].color = Color.green;
			trialCountdown--;

		}

		if (trialCountdown < 0){
			
			PlayerPrefs.SetInt("trialNumber", trialNumber);
			trialNumber++;
			PlayerPrefs.SetInt("trialNumber", trialNumber);

			SceneManager.LoadScene( "TableActive" );
		}

////		// Raycast interaction
////		RaycastHit hit;
////		Ray grabbingRay = new Ray (transform.position, Vector3.back);
////		Debug.DrawRay (transform.position, Vector3.back * grabHeight);
////		
////		
////
////		// if the raycast touches the object
////		if (Physics.Raycast (grabbingRay, out hit, grabHeight) && hit.collider.tag=="startTrial")
////		{
////			//Debug.Log ("hit the target");
////			//ready=true;
////			trialNumber++;
////			PlayerPrefs.SetInt("trialNumber", trialNumber);
////			ready = 1;
////			trialCountdown--;
////
////		}else {
////			//Debug.Log ("not hitting target");
////		}	
////
//		if (trialCountdown < 0){
//			trialNumber++;
//			PlayerPrefs.SetInt("trialNumber", trialNumber);
//			Application.LoadLevel( "4.TableActive" );
//		}
////
	}	
	
}