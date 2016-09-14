//using UnityEngine;
//using System.Collections;
//using System.Runtime.InteropServices;
//using System.Collections.Generic;
//using UnityEngine.UI;
//using System.IO;
//using System;
//using System.Threading;
//using U3D.Threading.Tasks; // required for threading
//using UnityEngine.SceneManagement;
//
//public class dataManager : MonoBehaviour {
//
//	public string participantNum;
//	public int trialNumber;
//	public string trialNum;
//
//	public GameObject[] Players; 	// array for players
//	public GameObject[] Objects; 	// array for movable objects
//	public GameObject[] Goals; 		// array for goal objects
//
//	// update timer
//	public List<string> Clockupdate = new List<string>();
//
//
//	// Use this for initialization
//	void Start () {
//
//		// get player, objects, and goals:
//		Players = GameObject.FindGameObjectsWithTag("player");
//		Objects = GameObject.FindGameObjectsWithTag("objects");
//		Goals =	GameObject.FindGameObjectsWithTag("goals");
//
//		// get trial info:
//		trialNum = PlayerPrefs.GetInt("trialNumber").ToString();
//
//
//	}
//
//	// Update is called once per frame
//	void Update () {
//
//		// get time
//		// public List<string> Clockupdate = new List<string>();
//
//		// if trial has ended save data and load ITI:
//		if (TableEvents.triggerCounter.triggerCount > TableEvents.allHitCounter.allHit){
//
//			saveAll(); // save data
//			SceneManager.LoadScene("3.blankTable");		
//		}
//
//	}
//
//
//	// Saves data from all sensors and moving objects
//	void saveAll() {
//
//		/// POSITION TSERIES /// --------------------------
//
//		// sensors (raw and table positions and grab object state / name)
//		for (int i = 0; i < Players.Length; i++){
//
//			// Thread data (raw sensor) from Sensor.cs
//
//			string sensorNum = Players[i].GetComponent<Sensor>().sensor.ToString();
//
//			List<Vector4> SensorData = new List<Vector4>();
//			SensorData = Players[i].GetComponent<Sensor>().SensorData;
//
//			StreamWriter sd = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Tracker_" + sensorNum + "sensor.txt");
//			foreach(Vector4 t in SensorData)
//			{
//				sd.WriteLine(t);
//			}
//			sd.Close();
//
//			// Table data (position data) from Sensor.cs
//
//			List<Vector4> tablePositions = new List<Vector4>();
//			tablePositions = Players[i].GetComponent<Sensor>().tablePositions;
//
//
//			StreamWriter pd = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Tracker_" + sensorNum + "_position.txt");
//			foreach(Vector4 u in tablePositions)
//			{
//				pd.WriteLine(sensorNum, u);
//			}
//			pd.Close();
//		}
//
//		// moving objects positions (table positions)
//		for (int j = 0; j < Objects.Length; j++){
//
//			string objectNum = Objects[j].GetComponent<movedObjectPos>().objectnumber.ToString();
//
//			List<Vector4> objectPos = new List<Vector4>();
//
//			// Thread data (raw sensor)
//			StreamWriter op = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Object_" + objectNum + "objectPos.txt");
//			foreach(Vector4 t in objectPos)
//			{
//				op.WriteLine(objectNum, t);
//			}
//			op.Close();
//
//		}
//
//
//
//
//		/// -----------------------------------------------
//
//		/// TRIAL STATE VALUES ----------------------------
//
//		// information from goals
//
//		// for each goal, get its position and the name (if any) of the object that hit it.
//		for (int k = 0; k < Goals.Length; k++){
//
//			string goalNum = Goals[k].GetComponent<goalTrigger>().goalNumber.ToString();
//			Vector3 goalPos = Goals[k].GetComponent<Transform>().ToString;
//			string goalCol = Goals[k].GetComponent<goalTrigger>().colObjectName;
//
//			Vector4 goalData = Vector4(goalPos,goalCol);
//
//			// Thread data (raw sensor)
//			StreamWriter gc = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Goal_" + goalNum + "_goalCol.txt");
//
//			gc.WriteLine(goalData);
//
//			gc.Close();
//
//		}
//
//
//
//		/// -----------------------------------------------
//
//
//	}
//
//}
