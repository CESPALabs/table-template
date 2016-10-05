/// <summary>
/// showPositionActive.cs
/// 
/// For debugging: shows polhemuis coordinates of tracker during active screen;
/// 
/// </summary>
/// 
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class showPositionActive : MonoBehaviour {
	
	GameObject player;
	Transform playerTransform;
	Vector3 tablePosition;
	Vector3 polhemusPosition;
	Text text;
	Vector3 tableOrigin;
	float timeLeft = 10.0f;
	int sampleNumber = 0;
	int trialNumber;

	
	// Use this for initialization
	void Start () {
		tableOrigin = manualCalibrate.tableOrigin;
		trialNumber = PlayerPrefs.GetInt("trialNumber");;

	}
	
	// Update is called once per frame
	void Update () {
		timeLeft = Time.deltaTime;
		GameObject player = GameObject.Find("Player1");
		Transform playerTransform = player.transform;
		Vector3 tablePosition = playerTransform.position;
		Vector3 polhemusPosition = (tablePosition - tableOrigin);
		sampleNumber++;
		text = GetComponent<Text> ();
		text.text = "Polhemus: " + polhemusPosition.ToString ("F3") + Environment.NewLine + "Table: " + tablePosition.ToString () 
			+ Environment.NewLine + "Trial: " + trialNumber + Environment.NewLine + "Sample: " + sampleNumber.ToString();
	}
}
