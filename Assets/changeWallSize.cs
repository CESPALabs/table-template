using UnityEngine;
using System.Collections;

public class changeWallSize : MonoBehaviour {

	// create bin for disc
	public GameObject disc;
	public float wallPlane=0;
	public float Xdist;
	public float xPosition;
	public Vector3 discPosition;
	public GameObject changedObject;
	public Vector3 changedObjectScale;
	public float tempSize;
	public Transform changedObjectTransform;
	// Use this for initialization
	void Start () {
		disc = GameObject.Find ("Object1");
		changedObject = GameObject.Find ("right.Wall");

		//for(i=1, i<3, i++)
	}

	// Update is called once per frame
	void Update () {
		//discPosition = disc.transform.position;
		//	xPosition = discPosition.x;

		// this combine previous commented lines
		// gets xposition of of the disc
		xPosition = disc.transform.position.x;

		// get the distance in x from disc to wallplane

		Xdist = xPosition - wallPlane;

		//grab the y value scale of changedObject

		changedObjectScale = changedObject.transform.localScale;
		tempSize = 4f + (.4f - Xdist);
		changedObjectScale.y = tempSize;
		changedObject.gameObject.transform.localScale = new Vector3 (5f, tempSize,  5f);

		//attach tempsize to right.Wall
		//changedObject.transform.localScale = tempSize;
	}
}