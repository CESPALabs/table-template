using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;

public class PlayerController : MonoBehaviour 
{
	public float speed = 1.0f;
	public int trialNumber;

	void Update() {
		var move = new Vector3(Input.GetAxis("Vertical"), Input.GetAxis("Horizontal"), Input.GetAxis("Depth"));
		transform.position += move * speed * Time.deltaTime;
		if (TableEvents.triggerCounter.triggerCount > 0){

			trialNumber = PlayerPrefs.GetInt ("trialNumber");

			//Application.LoadLevel( "3.blankTable" );
		}
	}
}
