using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;


public class countdowntrigger : MonoBehaviour {
	float countdown = 4;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		countdown = countdown - Time.deltaTime;
		if (countdown < 0)
		{
			SceneManager.LoadScene("TableActive");
		}

	}
}
