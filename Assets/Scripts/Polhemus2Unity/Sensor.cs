// ---------------------------------------
///	<summary>
/// 
/// File: Sensor.cs  /  Attach to Player
/// 
/// Script that collects Polhemus data from specified tracker [int sensor] in VRPN.
/// Also creates a speed variable that detects the instantaneous velocitiy of movement
/// in x,y,z space from one sample to the next.
/// 
/// </summary>
// ---------------------------------------


using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using System.Threading;
using U3D.Threading.Tasks; // required for threading
using UnityEngine.SceneManagement;




public class Sensor : MonoBehaviour {


	//// <POLHEMUS INTEGRATION VARIABLES>  ////////////////////////////


	// connect to Polhemus VRPN 
	public string deviceName = "TrackerJohn";
	public string deviceIP = "localhost";

	//public TrackerRemote tracker;

	//public Transform transformHandled; // what is this for?

	// Polhemus sensor number ** note: name script for sensor... 
	public int sensor;  // e.g., Sensor1.cs
	public string sensorName; 

	// data streams (raw polhemus for co-thread and player position on table for update thread

	// container for sensor data output * co-routines may be better


	// public List<string> SensorString = new List<string>();
	//// </POLHEMUS INTEGRATION VARIABLES>



	//// <TABLE INTEGRATION VARIABLES>  ////////////////////////////
	/// "update" prefix = variables updated w/ framerate
	/// 
	float scale = 1; // scaling factor for table in unity (likely always 10: 1 polhemus ft. = 10 unity ft.)
	public Vector3 updatePDIposition = new Vector3(); // vector3 for raw polhemus data
	Vector3 updatescalePDIposition = new Vector3(); // vector3 for scaled polhemus data (may be combined later)

	Quaternion updatePDIrotation = new Quaternion(); // quaternion for raw polhemus data (comment in as needed)

	Vector3 tableOrigin = new Vector3 (); // origin for table coordinates (from manualcalibrate.cs)
	Vector2 tableScale = new Vector2 (); // scale for the table coordinates
	float tableScaleX, tableScaleY;


	// Avatar position
	public Vector3 tablePosition = new Vector3 (); // current table position
//	float tableXpos, tableYpos, tableZpos; // x,y,z seperated of tablePosition
	public Vector3 tablePositionLast = new Vector3 (); // table position one step behind

	public Vector3 sensorVelocity = new Vector3 (); // instaneous velocity (tablePosition- tablePositionLast)
	public float velX, velY, velZ; // instantaneous velocity measures (change in table position from one sample to the next)


	////  </TABLE INTEGRATION VARIABLES>
	public Transform transformHandled;
	//public Vector3 PDIposition;



	//// <SCENE INTEGRATION VARIABLES> ////////////////////////////

	public int frame = 0; // how many updates?
	//GameObject Player1; // what object is being controlled
	// public int dataStreamLength; // DEBUG: test to make sure output list is updating

	//// </SCENE INTEGRATION VARIABLES>

	public string participantNum;
	public int trialNumber; // 
	public float countdown;
	public int polhemusSample;




	// <DATA MANAGEMENT VARS & STRUCTS> //////////////////////



	//////////////////////

	public List<Vector3> SensorData = new List<Vector3>(); //
	public List<Vector3> tablePositions = new List<Vector3>();

	// Timers
	public List<string> Clockupdate = new List<string>(); // better to attach to dataManager.cs
	public List<string> Clockhardware = new List<string>();
	public List<string> childNames = new List<string>();


	public string PlayerName;





	// Use this for initialization
	void Start () {

		PlayerName = gameObject.name;
		sensorName = PlayerName.Substring(6,1); 
		sensor = int.Parse(sensorName);

		// initiate gamelogic values
		participantNum = PlayerPrefs.GetString("participantNum");
		trialNumber = PlayerPrefs.GetInt("trialNumber");

		// polhemus variables
		tableOrigin = scale * manualCalibrate.tableOrigin; // set 0,0,0 of table
		tableScale = manualCalibrate.tableScale;
		tableScaleX = tableScale[0];
		tableScaleY = tableScale[1];


		// start Polhemus data thread in background called faster than framerate
		U3D.Threading.Dispatcher.Initialize ();
		CreateThread();
	}



	// Update is called once per frame
	void Update () {

		// get the time (add to list)
		//Clockupdate.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));


		// check for children
		// if the required number of targets have been hit *see TableEvents.cs trigger:
//		if (TableEvents.triggerCounter.triggerCount > TableEvents.goalHitCounter.goalHit){
//			
//			saveAll(); // save Polhemus data
//			//SceneManager.LoadScene("4.TableActive");		
//		}


		// if trials are timed: 
//		countdown -= Time.deltaTime;


//		if (countdown < 0){
//			saveAll();
//			SceneManager.LoadScene("1.splashScreen");		
//
//		}


		// Get data from the Polhemus
		updatePDIposition = VRPN.vrpnTrackerPos(deviceName + "@" + deviceIP,sensor);
		float xpos = updatePDIposition [0];
		float ypos = updatePDIposition [1]+.025f; // tweeks to center avatar on marker
		float zpos = -1*updatePDIposition [2] + .05f;

		// rescale to UNITY Table dimensions
		updatescalePDIposition = new Vector3 (xpos/tableScaleX, ypos/tableScaleY, zpos);


		// frames necessary if using velocity measure to move:

//		frame++;
//
//		if (frame > 1){
//			tablePositionLast = tablePosition; // set last position
//		}

		////// TESTING positions in Console: /////

		//Debug.Log (newPDIposition);
		//Debug.Log (tablePosition);



		// transform Pohlemus space to table space:
		tablePosition = updatescalePDIposition - tableOrigin;
//		tableXpos = tablePosition[0];
//		tableYpos = tablePosition[1];
//		tableZpos = tablePosition[2];

		// tableSensor object
		transform.position = Vector3.Lerp(tablePosition, tablePositionLast, Time.deltaTime);


		tablePositions.Add(tablePosition);



//		if (frame > 1){
//
//			// position differentiation (instantaneous velocity)
//			sensorVelocity = (tablePosition - tablePositionLast)/Time.deltaTime;
//
//			// velX = (tablePosition[0]-tablePositionLast[0])/ Time.deltaTime;  // isolated by axis
//			// velY = (tablePosition[1]-tablePositionLast[1])/ Time.deltaTime;
//			// velZ = (tablePosition[2]-tablePositionLast[2])/ Time.deltaTime;
//		}


		////////// ROTATION DATA ////////////////////////

		//updatePDIrotation = VRPN.vrpnTrackerQuat("TrackerJohn@localhost",sensor); 


	}





	////////////// THREADING ///////////////////

	// Put what you want threaded in Task.Run *see below)
	// This gets called in Start();

	void GetPolhemusData () {
		Vector3 PDIposition = VRPN.vrpnTrackerPos(deviceName + "@" + deviceIP,sensor);

		float xpos = PDIposition [0];
		float ypos = PDIposition [1];
		float zpos = -1*PDIposition [2];

		//string dataString = (xpos + "," + ypos + "," + zpos.ToString);
		Vector3 scalePDIposition = new Vector3 (tableScaleX*xpos, tableScaleY*ypos, zpos);
		//Vector3 tablePosition = scalePDIposition - tableOrigin;
//		SensorString.Add(dataString);
		SensorData.Add(scalePDIposition);
		//SensorData.Add(scalePDIposition,DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));
	}


	void MockUpSomeDelay()
	{
		System.Threading.Thread.Sleep (8); // sleep between steps in milisec
	}


	// create a thread using two functions above:
	public void CreateThread()
	{
		Task.Run (() => {
			//int totSampleint = Convert.ToInt32(totSample);
			// [...] Code to be executed in auxiliary thread
			for(int i=0; i<10000; i++)
			{
				//  this for loop iterates through the data collection
				int value = i;
				polhemusSample = i;
				GetPolhemusData();
				Clockhardware.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));
				MockUpSomeDelay();
			}
		});//.ContinueInMainThreadWith(Update);
	}
		
	// Save data *** no longer saving here. Saving in saveTrials.cs
//	void saveAll()
//	{
////		string trialNum = trialNumber.ToString();
////
////		// Thread data (raw sensor)
////		StreamWriter sd = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "Tracker_" + sensorNum + "sensor.txt");
////		foreach(rawDataStream t in SensorData)
////		{
////			sd.WriteLine(t);
////		}
////		sd.Close();
////
////		// Table data (position data)
////		StreamWriter pd = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "Tracker_" + sensorNum + "_position.txt");
////		foreach(playerPosStream u in tablePositions)
////		{
////			pd.WriteLine(u);
////		}
////		pd.Close();
//	}

}

