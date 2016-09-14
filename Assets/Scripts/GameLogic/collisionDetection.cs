// ---------------------------------------
///	<summary>
/// 
/// File: collisionDetection.cs		/	Attach to movable object
/// 
/// Detects whether moveable object has bumped into an obstacle.
/// 
/// </summary>
// ---------------------------------------



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collisionDetection : MonoBehaviour {

	public bool hitObstacle; // has the object collider with an obstacle?
	public float timeUntilActive;

	// Use this for initialization
	void Start () {
		hitObstacle = false;
		timeUntilActive = 0f;
	}

	void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "obstacle")
		{
			hitObstacle = true;
			timeUntilActive = 0.5f;
		}
	}
	// Update is called once per frame
	void Update () {
		timeUntilActive -= Time.deltaTime;

		if(timeUntilActive < 0){
			hitObstacle = false;
		}
	}
}
