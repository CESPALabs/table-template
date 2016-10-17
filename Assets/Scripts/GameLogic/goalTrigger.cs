// ---------------------------------------
///	<summary>
/// 
/// File: goalTrigger.cs	/	Attach to goal location(s)
/// 
/// Detects whether specified goal location has been hit. If so translates
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

public class goalTrigger : MonoBehaviour {

	public int trialComplete = 0;
	public string colObjectName;
	public string goalNumber; // changes with goals
	public Vector3 goalPosition;
	public int maxhits;


	// Use this for initialization
	public int OnTriggerEnter ( Collider gTrigger ) {

		if (gTrigger.gameObject.tag == "object"){

			colObjectName = gTrigger.gameObject.name;

			Debug.Log("Player entered the goal");	

			TableEvents.endHitCounter.endHit++; // if goal has been hit, increase trigger couter by 1 *see TableEvents.cs

			return trialComplete;

		} else {
			trialComplete = 0;
			return trialComplete;
		}

	}

	void Start () {
		goalNumber = gameObject.name.Substring(4,1);
		goalPosition = transform.position; // if static
		colObjectName = "none";
	}

	void Update () {
		//check to see if maximum goal hits recorded
	}
		
}
