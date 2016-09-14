using UnityEngine;
using System.Collections;

public class trialConditions : MonoBehaviour {

	public static trialConditions trialCounter;
	public int trialNumber = 0;

	public static float grabHeight = 0.1f;



	// Use this for initialization
	void Start () {

		PlayerPrefs.SetInt("trialNumber", trialNumber);

	}

}
