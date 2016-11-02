// ---------------------------------------
///	<summary>
/// 
/// File: grabObject.cs  /  Attach to Player
/// 
/// Raycasts from player. Detects when ray hits an object (specified by collider tag). ID's grabbed object.
/// Attaches to Player
/// 
/// </summary>
// ---------------------------------------

using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class grabObject : MonoBehaviour {

	//public GameObject Player1;
	public float grabHeight;
	public Vector3 movedCircle;
	bool grab = false;
	public Vector3 avatarObjPos = new Vector3 ();
	public GameObject movedObj;
	public string grabbedObjectName;
	public GameObject grabbedObject;
	public Vector3 grabbedObjectVel;
	Transform grabbedObjectPos;
	public Vector3 curVel, lastPos, latchPoint;
	public bool grabbedIt;
	public float grabCountdown;
	public bool hitObstacle;
	public bool trialComplete;
	public List<string> grabList;




		

	void Start (){
		grabHeight =0.20f;
		grabCountdown = 0f;
		}


	void Update () 
	{


		grabCountdown -= Time.deltaTime;

		// calculate instaneous velocity
		curVel = (transform.position-lastPos);

		// stamp in position for next update
		lastPos = transform.position;

		// what object being grabbed

 		// Raycast based interaction with objects
 		
		RaycastHit hit;
		//RaycastHit hold;
		Ray grabbingRay = new Ray (transform.position, Vector3.back);
		Debug.DrawRay (transform.position, Vector3.back * grabHeight);

		 


		if (Physics.Raycast (grabbingRay, out hit, grabHeight) && hit.collider.tag=="object" && grabCountdown < 0 && Mathf.Abs(transform.position.z) < .175)
 		{
			grabbedIt = true;
		} else {
			grabbedIt = false;
		}
			



		if (grabbedIt == true)
		{
			grabbedObjectName = hit.collider.gameObject.name; 
			grabbedObject = GameObject.Find(grabbedObjectName);



			grabbedObjectPos = hit.transform;	

			collisionDetection collide = grabbedObject.GetComponent<collisionDetection>(); // collision detection from collisionDetection.cs
			grabCountdown = collide.timeUntilActive; // see collisionDetection.cs   this value needs to be less than 0 in order to grab

			grabbedObjectPos.parent = transform; // attach to parent (Player)

//			Vector3 returnObjtoTable = new Vector3();
//			returnObjtoTable = grabbedObjectPos.position;
//			returnObjtoTable.z = .02f;
//			grabbedObjectPos.transform.position = returnObjtoTable;

		} else {
			
			Vector3 returnObjtoTable = new Vector3();
			returnObjtoTable = grabbedObjectPos.position;
			returnObjtoTable.z = .00f;
			grabbedObjectPos.transform.position = returnObjtoTable;
			transform.DetachChildren();
			grabbedObjectName = "none";
			grabbedObject = null;
			grabbedIt = false;

			}

		// was an object grabbed in this frame?
		grabList.Add(grabbedObjectName);

		}
		

	}
		
