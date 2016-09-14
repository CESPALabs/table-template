using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class showPosition : MonoBehaviour {

	GameObject player;
	Transform playerTransform;
	Vector3 position;
	Text text;
	public float timeLeft=20.0f;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		timeLeft = Time.deltaTime;
		GameObject player = GameObject.Find("Player");
		Transform playerTransform = player.transform;
		Vector3 position = playerTransform.position;
		Vector3 origin = manualCalibrate.tableOrigin;
		Vector2 scale = manualCalibrate.tableScale;

		text = GetComponent<Text> ();
		text.text = "Polhemus: " + position.ToString () + Environment.NewLine + 
				"Table Origin: " + origin.ToString () + Environment.NewLine + 
				"Table Scale (x,y): " + scale.ToString ();
	
	}
}
