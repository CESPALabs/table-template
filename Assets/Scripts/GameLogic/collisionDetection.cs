// ---------------------------------------
///	<summary>
/// 
/// File: collisionDetection.cs
/// 
/// Placement: should be attach to a movable object
/// 
/// Detects whether moveable object has bumped into an obstacle. If collision has occurs notes time since collision
/// (useful for simulating "drops" because of collision).
/// 
/// </summary>
// ---------------------------------------



using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class collisionDetection : MonoBehaviour {

	public bool hitObstacle; // has the object collider with an obstacle?
	public float timeUntilActive;
	public SpriteRenderer renderer;
	public Color defColor;

	// Use this for initialization
	void Start () {
		hitObstacle = false;
		timeUntilActive = 0f;
		renderer = gameObject.GetComponent<SpriteRenderer>();
		defColor = 	renderer.color;
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

		if(hitObstacle== true)
		{
			renderer.color = new Color(0f,0f,0f, .5f);
		} else {
			renderer.color = defColor;
		}

		if(timeUntilActive < 0){
			hitObstacle = false;
		}
	}
}
