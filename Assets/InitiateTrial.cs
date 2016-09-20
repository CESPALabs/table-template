using UnityEngine;
using System.Collections;

public class InitiateTrial : MonoBehaviour {

	public int trialnumber;
	// Use this for initialization
	void Awake () {
		trialnumber = PlayerPrefs.GetInt("trialnumber");
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
