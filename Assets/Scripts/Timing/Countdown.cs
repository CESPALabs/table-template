using UnityEngine;
using System.Collections;

public class Countdown : MonoBehaviour {

	public float timeLeft = 10;
	public float endTrial = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		timeLeft -= Time.deltaTime;

		if (timeLeft < 0){
			endTrial = 1;
		}
	
	}
}
