// ---------------------------------------
///	<summary>
/// 
/// File: goalTrigger.cs	/	Attach to goal location(s)
/// 
/// Detects whether specified goal location has been hit
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

	public bool trialComplete = false;
	public string colObjectName;
	public string goalNumber; // changes with goals
	public Vector3 goalPosition;


	// Use this for initialization
	public bool OnTriggerEnter ( Collider gTrigger ) {

		if (gTrigger.gameObject.tag == "object"){

			colObjectName = gTrigger.gameObject.name;

			Debug.Log("Player entered the goal");	

			trialComplete = true;

			TableEvents.triggerCounter.triggerCount++; // if goal has been hit, increase trigger couter by 1 *see TableEvents.cs

			return trialComplete;



		} else {
			trialComplete = false;
			return trialComplete;
		}

	}

	void Start () {
		goalNumber = gameObject.name.Substring(4,1);
		goalPosition = transform.position; // if static
		colObjectName = "none";
	}

	void Update () {
		//goalPosition = transform.position; // if dynamic
	}
		
}
