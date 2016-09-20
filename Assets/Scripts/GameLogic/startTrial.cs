using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using System.Threading;

public class startTrial : MonoBehaviour {

//	public float grabHeight;
//	public static startTrial readyStart;
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
	
//	void Start (){
//		readyStart = this;
//		ready = 0;
//
//		trialNumber = PlayerPrefs.GetInt ("trialNumber"); // get the trial number
//		grabHeight = trialConditions.grabHeight; // get the grabbing height
//		trialCountdown = 300;
//	}
//		
//
//void Update () 
//	{
////
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
//	}	
//	
}