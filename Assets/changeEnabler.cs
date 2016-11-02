//Enable scripts (ChangeSize, ChangeDiscSize) depending on the trial
//trial 1 enable ChangeDiscSize, disable ChangeSize
//trial 2 enable ChangeSize, disable ChangeDiscSize




using UnityEngine;
using System.Collections;



public class changeEnabler : MonoBehaviour {
	public bool enabled;
	GameObject Object1;
	public int ChangeOn;
	public int trialNumber;



	// Use this for initialization
	void Start () {

		Object1 = GameObject.Find ("WorldBuilder");
		Object1.GetComponent<ChangeDiscSize> ().enabled = false;


	}

	// Update is called once per frame
	void Update () {
		ChangeOn = int.Parse(Object1.GetComponent<setEnvironment>().changeDiscList[trialNumber]);

		if (ChangeOn == 1) {
			Object1.GetComponent<ChangeDiscSize> ().enabled = true;
		} else {
			Object1.GetComponent<ChangeDiscSize> ().enabled = false;
		}


	}
}
