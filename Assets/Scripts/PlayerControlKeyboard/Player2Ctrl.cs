using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;


public class Player2Ctrl : MonoBehaviour {
	public float speed = 1.0f;
	
	void Update() {
		var move = new Vector3(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"), 0);
		transform.position += move * speed * Time.deltaTime;
	}
}