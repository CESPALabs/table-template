// SensorN.cs: script that collects Polhemus data from specified tracker X in VRPN.
// Importantly, creates a speed variable that detects the instantaneous velocitiy of movement
// in x,y,z space from one sample to the next.


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



public class Sensor1VP : MonoBehaviour {


	//// <POLHEMUS INTEGRATION VARIABLES>  ////////////////////////////


	// connect to Polhemus VRPN 
	public string deviceName = "TrackerJohn";
	public string deviceIP = "localhost";

	//public TrackerRemote tracker;

	//public Transform transformHandled; // what is this for?

	// Polhemus sensor number ** note: name script for sensor... 
	int sensor = 1;  // e.g., Sensor1.cs
	string sensorNum; 

	// container for sensor data output * co-routines may be better
	public List<Vector3> SensorData = new List<Vector3>(); //

	//// </POLHEMUS INTEGRATION VARIABLES>



	//// <TABLE INTEGRATION VARIABLES>  ////////////////////////////
	/// "update" prefix = variables updated w/ framerate
	/// 
	float scale = 10.0f; // scaling factor for table in unity (likely always 10: 1 polhemus ft. = 10 unity ft.)
	Vector3 updatePDIposition = new Vector3(); // vector3 for raw polhemus data
	Vector3 updatescalePDIposition = new Vector3(); // vector3 for scaled polhemus data (may be combined later)

	Quaternion updatePDIrotation = new Quaternion(); // quaternion for raw polhemus data (comment in as needed)

	Vector3 tableOrigin = new Vector3 (); // origin for table coordinates (from manualcalibrate.cs)

	// Avatar position
	public Vector3 updatetablePosition = new Vector3 (); // current table position
	float tableXpos, tableYpos, tableZpos; // x,y,z seperated of tablePosition
	public Vector3 updatetablePositionLast = new Vector3 (); // table position one step behind

	public Vector3 sensorVelocity = new Vector3 (); // instaneous velocity (tablePosition- tablePositionLast)
	public float velX, velY, velZ; // instantaneous velocity measures (change in table position from one sample to the next)


	////  </TABLE INTEGRATION VARIABLES>
	public Transform transformHandled;




	//// <SCENE INTEGRATION VARIABLES> ////////////////////////////

	public int frame = 0; // how many updates?
                          //GameObject Player1; // what object is being controlled
                          //public int dataStreamLength; // DEBUG: test to make sure output list is updating

    //// </SCENE INTEGRATION VARIABLES>

    //public int trial; // GameLogic




    private Vector3 positionOffset = new Vector3();



	// Use this for initialization
	void Start () {
		//tableOrigin = scale * manualCalibrate.tableOrigin; // set 0,0,0 of table
		sensorNum = sensor.ToString();

		// start Polhemus data thread in background called faster than framerate
		U3D.Threading.Dispatcher.Initialize (); // Initiazes Package *U3D is mandatory for threading
		PolhemusDataThread(); // Thread Function begins
        //positionOffset = VRPN.vrpnTrackerPos(deviceName + "@" + deviceIP, sensor);
    }



	// Update is called once per frame
	void Update () {

		// Get data from the Polhemus
		updatePDIposition = VRPN.vrpnTrackerPos(deviceName + "@" + deviceIP,sensor) - positionOffset;
		float xpos = updatePDIposition [0];
		float ypos = updatePDIposition [1];
		float zpos = updatePDIposition [2];

//		// rescale to UNITY Table dimensions
//		updatescalePDIposition = new Vector3 (scale*xpos, scale*ypos, scale*zpos+2);

		frame++;

		//		if (frame > 1){
		//			updatetablePositionLast = updatetablePosition; // set last position
		//		}

		////// TESTING positions in Console: /////

		//Debug.Log (newPDIposition);
		//Debug.Log (tablePosition);



		// transform Pohlemus space to table space:
		//updatetablePosition = updatescalePDIposition - tableOrigin;
		// tableXpos = tablePosition[0];
		// tableYpos = tablePosition[1];
		// tableZpos = tablePosition[2];

		// tableSensor object
		transform.position = new Vector3(zpos, transform.position.y, transform.position.z);



		//		if (frame > 1){
		//
		//			// position differentiation (instantaneous velocity)
		//			sensorVelocity = (updatetablePosition - updatetablePositionLast)/Time.deltaTime;
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
	public void PolhemusDataThread()
	{
		Task.Run (() => {
			//int totSampleint = Convert.ToInt32(totSample);
			// [...] Code to be executed in auxiliary thread
			for(int i=0; i<500; i++)
			{
				//  this for loop iterates through the data collection
				int value = i;
				Vector3 PDIposition = VRPN.vrpnTrackerPos(deviceName + "@" + deviceIP,sensor);
				float xpos = PDIposition [0];
				float ypos = PDIposition [1];
				float zpos = -1*PDIposition [2];

				SensorData.Add(PDIposition);
				//Vector3 scalePDIposition = new Vector3 (scale*xpos, scale*ypos, scale*zpos+2);
				//Vector3 tablePosition = scalePDIposition - tableOrigin;
				//SensorData.Add(tablePosition);

				// sleep in millisec (1/ (sleep/1000) = sampleRate
				System.Threading.Thread.Sleep (8);

			}
		});//.ContinueInMainThreadWith(Update);
	}



//	void saveAll()
//	{
//		int trialnumber = PlayerPrefs.GetInt ("Trial Number");
//		string trialNum = trialnumber.ToString();
//		StreamWriter sw = new StreamWriter("Trial_" + trialNum + "Tracker_" + sensorNum + "_position.txt");
//		foreach(Vector3 t in SensorData)
//		{
//			sw.WriteLine(t);
//		}
//		sw.Close();
//	}

	public void SaveData2(List<Vector3> data, string directory, string filename)
	{
		// It saves participants' data during performing the task.
		string whereToSave = directory + filename;
		StreamWriter writer = new StreamWriter(whereToSave);
		using (writer)
		{
			int len = data.Count;
			foreach (Vector3 v in data)
			{
				writer.WriteLine(v);
			}
		}
	}
}

