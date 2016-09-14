/// <summary>
/// 
/// OBSOLETE: see grabObject.cs
/// 
/// pushObject.cs: allows Polhemus sensor to interact with rigid bodies in the scene.
/// Calculates the instantaneous velocity of the sensor and uses it to apply a Force vector
/// to the raycasted object.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class pushObject : MonoBehaviour {

	//public GameObject Player1;
	public float grabHeight = 4;
	public Transform movedCircle;
	//bool grab = false;
	//public List<Vector3> avatarObjPos;
	public Vector3 avatarObjPos = new Vector3 ();
	public GameObject avatarObj;

	Vector3 lastPos;
	public Vector3 curVel;
	public Rigidbody rb;

	void Start (){
		avatarObj = GameObject.FindWithTag ("avatar");
	}

	//	void OnTriggerEnter(Collider myTrigger) {
	//
	//		if (myTrigger.gameObject.tag == "avatar") {
	//			
	//			Debug.Log ("hit the avatar");
	//			movedCircle = myTrigger.gameObject.transform;
	//			movedCircle.parent = transform;
	//			
	//		}
	//	}

	// Update is called once per frame
	void FixedUpdate () 
	{
		avatarObj = GameObject.FindWithTag ("avatar"); // find movable objects
		avatarObjPos = avatarObj.transform.position;


		//foreach (Transform child in transform)
		//{
		//				childPos = transform.position;// do whatever you want with child transform object here
		//}

		// Raycast based interaction

		RaycastHit hit;
		Ray grabbingRay = new Ray (transform.position, Vector3.back);

		// calculate instaneous velocity
		curVel = (transform.position-lastPos)/Time.fixedDeltaTime;

		// stamp in position for next update
		lastPos = transform.position;

		//CharacterController charContr = GetComponent<CharacterController>();
		//Vector3 p1 = 
		//Debug.DrawRay (transform.position, Vector3.back * grabHeight);
		//rb = movedCircle.GetComponent<Rigidbody>();
		//rb.AddForce(curVel,	ForceMode.VelocityChange);

		if (Physics.Raycast (grabbingRay, out hit, grabHeight) && hit.collider.tag=="avatar" && transform.position.z<4)
		{
			//Debug.Log ("hit the avatar");

			movedCircle = hit.transform; // gets transform of "hit" object
		
			//rb = movedCircle.GetComponent<Rigidbody>(); // gets rigidbody component of hit object

			//hit.transform += curVel;
			//rb.AddForce(curVel,	ForceMode.VelocityChange); // applies force: curVel

			//hit.transform.SetParent(grabbingRay.transform, false);

//			Transform moveME = Transform.Find(movedCircle);


			//movedCircle.parent = transform;

			//foreach (Transform child in transform)
			//{
			//				childPos = transform.position;// do whatever you want with child transform object here
			//}

		} else {
			//Debug.Log ("not hitting avatar");

			//if the moved object drag = 0, then the following two lines act as a brake:
			rb.velocity = Vector3.zero;
			rb.angularVelocity = Vector3.zero;

			// this below may be unnecessary
			Vector3 temp = avatarObjPos;
			temp.z = 1.0f;
			avatarObj.transform.position = temp;
			movedCircle = null;

		}


	}

	void LateUpdate(){


	}
}