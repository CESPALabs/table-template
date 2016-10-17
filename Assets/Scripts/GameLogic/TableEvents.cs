// ---------------------------------------
///	<summary>
/// 
/// File: TableEvents.cs
/// 
/// General table events. For now contains event counters that may be accessed by other scripts
/// 
/// </summary>
// ---------------------------------------

using UnityEngine;
using System.Collections;

public class TableEvents : MonoBehaviour {

	public static TableEvents triggerCounter;
	public int triggerCount; // polls all possible end trial triggers; how many have been triggered?

	public static TableEvents endHitCounter;
	public int endHit;

	public static TableEvents startHitCounter;
	public int startHit;

	public static TableEvents trialEnded;
	public bool trialStop;

	public static TableEvents trialStarted;
	public bool trialStart;

	public int startHitsMax = 1;

	public int endHitsMax = 1;


	// Use this for initialization
	void Start () {
		trialStart = false;
		trialStop = false;

		triggerCounter = this;
		triggerCount = 0;

		// for use on trial screen to endtrial:
		endHitCounter = this;
		endHit = 0; // how many hits necessary to end trial minus 1

		// for use on ITI screen to start trial:
		startHitCounter = this;
		startHit = 0; // how many hits necessary to end trial minus 1
	}
	
	// Update is called once per frame
	void Update () {

		// stop trial when maximum hits made:
		if (endHit == endHitsMax)
		{
			trialStop = true;
		}

		// start trial when all start hits made:
		if (startHit == startHitsMax)
		{
			trialStart = true;
		}
	}
}
