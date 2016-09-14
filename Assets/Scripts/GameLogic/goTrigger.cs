// ---------------------------------------
///	<summary>
/// 
/// File: goTrigger.cs
/// 
/// Keeps splashcreen scene on for N seconds (splashLoadtime)
/// 
/// </summary>
// ---------------------------------------

using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class goTrigger : MonoBehaviour {

	int ready;
	float trialCountdown;
	public int trialNumber;
	public bool trialStart;

	void Start () {
		trialCountdown = 300;
		trialNumber = PlayerPrefs.GetInt ("trialNumber"); // get the trial number
		trialStart = false;
	}


	public bool OnTriggerEnter ( Collider myTrigger ) {

		if (myTrigger.gameObject.tag == "Player"){

			//			lastCoords = Vector3 movement;
			trialStart = true;
			Debug.Log("Player entered the trial trigger");	

			trialNumber++;
			PlayerPrefs.SetInt("trialNumber", trialNumber);
			ready = 1;

			return trialStart;

		} else {
			trialStart = true;
			return trialStart;

			//Debug.Log ("not hitting trigger");
		}

	}
	
	// Update is called once per frame
	void Update () {

		if (ready == 1){
			GetComponent<SpriteRenderer>().materials[0].color = Color.green;
			trialCountdown--;

		}

		if (trialCountdown < 0){
			PlayerPrefs.SetInt("trialNumber", trialNumber);
			SceneManager.LoadScene( "TableActive" );
		}
	}
}
