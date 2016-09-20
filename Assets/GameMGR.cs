using UnityEngine;
using System.Collections;

public class GameMGR : MonoBehaviour {

	public int trialnumber; // iterate a trial during every ITI screen

	// Use this for initialization
	void Start () {
		trialnumber = PlayerPrefs.GetInt("trialnumber") + 1;
		PlayerPrefs.SetInt("trialnumber", trialnumber);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
