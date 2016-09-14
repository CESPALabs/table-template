/// <summary>
/// Application exit. Escapes application. Usually attached to table or camera (something that persists in every scene).
/// </summary>

using UnityEngine;
using System.Collections;

public class applicationExit : MonoBehaviour {

	// Use this for initialization
	void Awake (){
		QualitySettings.vSyncCount = 1; 
		//Application.targetFrameRate = 160;
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown("escape")){
			Application.Quit();
		}
	}
}
