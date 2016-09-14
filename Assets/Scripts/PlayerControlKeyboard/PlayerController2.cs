using UnityEngine;
using System.Collections;

public class PlayerController2 : MonoBehaviour 
{
	//void Update () // called before rendering a frame

	public float speed;
	
	void FixedUpdate ()  // called before physics calculations
	{
		float moveHorizontal2 = Input.GetAxis ("Horizontal2");
		float moveVertical2 = Input.GetAxis ("Vertical2");

		Vector3 movement = new Vector3 (moveHorizontal2, 0.0f, moveVertical2);

		GetComponent<Rigidbody>().AddForce(movement * speed * Time.deltaTime);
	}
}
