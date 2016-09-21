using UnityEngine;
using System.Collections;


//As the disc moves towards the wall (y-axis?), it needs to get slightly larger 
//First: find the disc
//Second: grab the distance between the disc and the wall
//Third: grab the scale (x,y) of the disc
//Fourth: change the scale/size of the disc as it approaches the wall


public class ChangeDiscSize : MonoBehaviour {

	public GameObject disc;
	public Vector3 discScaleChange;
	public Vector3 discPosition;
	//public float discPosition;
	public GameObject wall;
	public float wallPlane=0;
	public float Xdist;
	public float xPosition;


	// Use this for initialization
	void Start () {
		disc = GameObject.Find ("Object1 ");
		wall = GameObject.Find ("mid.Wall");
	}

	// Update is called once per frame
	void Update () {

		//capture position of disc: does only capturing the x-scale work?
		xPosition = disc.transform.position.x;

		//distance from disc to wall = Xdist
		Xdist = xPosition - wallPlane;


		//change the x and y scale of disc as it approaches x-axis
		//stop when at critical line

		float critline =.35f;

		if (disc.transform.position.x > critline) {
			disc.gameObject.transform.localScale = new Vector3 (.1f - Xdist, .1f - Xdist, 0);
		} 

	}
}