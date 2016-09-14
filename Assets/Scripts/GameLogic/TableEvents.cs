// ---------------------------------------
///	<summary>
/// 
/// File: TableEvents.cs
/// 
/// General table events. For now, imcludes trial trigger detection.
/// 
/// </summary>
// ---------------------------------------

using UnityEngine;
using System.Collections;

public class TableEvents : MonoBehaviour {

	public static TableEvents triggerCounter;
	public int triggerCount; // polls all possible end trial triggers; how many have been triggered?

	public static TableEvents goalHitCounter;
	public int goalHit;

	public static TableEvents startHitCounter;
	public int startHit;


	// Use this for initialization
	void Start () {
		triggerCounter = this;
		triggerCount = 0;

		// for use on trial screen to endtrial:
		goalHitCounter = this;
		goalHit = 0; // how many hits necessary to end trial minus 1

		// for use on ITI screen to start trial:
		startHitCounter = this;
		startHit = 0; // how many hits necessary to end trial minus 1
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
