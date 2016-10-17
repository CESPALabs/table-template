// ---------------------------------------
///	<summary>
/// 
/// File: startTrialTrigger.cs
/// Scene: ITI
/// Object: attach to every trialTrigger Object
/// Purpose: detects whether a player object has collided with the trigger.
/// InputFrom: NA
/// ExportsTo: startTrial.cs
/// 
/// </summary>
// ---------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class startTrialTrigger : MonoBehaviour {

	public int ready;
	bool trialStart;

	void Start () {
		trialStart = false;
		ready = 0;
	}


	public bool OnTriggerEnter ( Collider myTrigger ) {

		if (myTrigger.gameObject.tag == "Player"){

			//			lastCoords = Vector3 movement;
			trialStart = true;
			Debug.Log("Player entered the trial trigger");	



			return trialStart;

		} else { 
			Debug.Log ("not hitting trigger");
			return trialStart;

		}

	}
	
	// Update is called once per frame
	void Update () {

		if (trialStart == true){
			ready = 1;
		}

		if (ready == 1){
			GetComponent<SpriteRenderer>().materials[0].color = Color.green;

		}
			
	}
}
