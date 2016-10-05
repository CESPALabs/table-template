using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class saveTrialData : MonoBehaviour {

//	public string participantNum;
//	public int trialNumber;
//	public string trialNum;
//	public List<string> updateClock = new List<string>();
//
//	public GameObject[] Players; 	// array for players
//	public GameObject[] Objects; 	// array for movable objects
//	public GameObject[] Goals; 		// array for goal objects
//
//
//	// Use this for initialization
//	void Start () {
//
//		// get player, objects, and goals:
//		Players = GameObject.FindGameObjectsWithTag("Player");
//		Objects = GameObject.FindGameObjectsWithTag("object");
//		Goals =	GameObject.FindGameObjectsWithTag("goal");
//
//
//		// get trial info:
//		trialNum = PlayerPrefs.GetInt("trialNumber").ToString("D2");
//		participantNum = PlayerPrefs.GetString("playerName");
//
//
//	}
//	
//	// Update is called once per frame
//	void Update () {
//
//		updateClock.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));
//
//
//		// if trial has ended save data and load ITI:
//		if (TableEvents.triggerCounter.triggerCount > TableEvents.goalHitCounter.goalHit){
//
//			saveAll(); // save data
//			SceneManager.LoadScene("BlankTable");		
//		}
//	
//	}
//
//
//	// Saves data from all sensors and moving objects
//	void saveAll() {
//
//		/// SAVE UPDATE CLOCK
//		StreamWriter uc = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_updateClock.txt");
//		foreach(string u in updateClock)
//		{
//			uc.WriteLine(u);
//		}
//		uc.Close();
//
//
//
//		/// POSITION TSERIES /// --------------------------
//
//		// sensors (raw and table positions and grab object state / name)
//		for (int i = 0; i < Players.Length; i++){
//
//			// Thread data (raw sensor) from Sensor.cs
//
//			string sensorNum = Players[i].GetComponent<Sensor>().sensorName;
//
//			List<Vector3> SensorData = new List<Vector3>();
//			List<string> SensorClock = new List<string>();
//
//			SensorData = Players[i].GetComponent<Sensor>().SensorData;
//			SensorClock = Players[i].GetComponent<Sensor>().Clockhardware; // hardware clock
//
//			StreamWriter sd = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Tracker_" + sensorNum + "_sensor.txt");
//			foreach(Vector3 t in SensorData)
//			{
//				sd.WriteLine(t);
//			}
//			sd.Close();
//
//			StreamWriter sc = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Tracker_" + sensorNum + "_sensorClock.txt");
//			foreach(string t in SensorClock)
//			{
//				sc.WriteLine(t);
//			}
//			sc.Close();
//
//
//			// Table data (position data) from Sensor.cs
//
//			List<Vector3> tablePositions = new List<Vector3>();
//			List<string> playerChild = new List<string>();
//
//			tablePositions = Players[i].GetComponent<Sensor>().tablePositions;
//			playerChild = Players[i].GetComponent<Sensor>().Clockupdate; // hardware clock
//
//
//			StreamWriter pd = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Tracker_" + sensorNum + "_position.txt");
//			foreach(Vector3 u in tablePositions)
//			{
//				pd.WriteLine(u);
//			}
//			pd.Close();
//
////			StreamWriter pc = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Tracker_" + sensorNum + "_positionClock.txt");
////			foreach(string u in PositionClock)
////			{
////				pc.WriteLine(u);
////			}
////			pc.Close();
//		}
//
//		// moving objects positions (table positions)
//		for (int j = 0; j < Objects.Length; j++){
//
//			string objectNum = Objects[j].GetComponent<movedObjectPos>().objectnumber;
//
//			List<Vector3> objectPositions = new List<Vector3>();
//			List<string> objectParents = new List<string>();
//
//			objectPositions = Objects[j].GetComponent<movedObjectPos>().objectPos;
//			objectParents = Objects[j].GetComponent<movedObjectPos>().parentName;
//
////			objectClock = Objects[j].GetComponent<movedObjectPos>().Clockupdate;
//
//
//
//			// object position data
//			StreamWriter op = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Object_" + objectNum + "_objectPos.txt");
//			foreach(Vector3 t in objectPositions)
//			{
//				op.WriteLine(t);
//			}
//			op.Close();
//
//			// object parents name
//			StreamWriter po = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Object_" + objectNum + "_objectParent.txt");
//			foreach(string t in objectParents)
//			{
//				po.WriteLine(t);
//			}
//			po.Close();
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
//			string goalNum = Goals[k].GetComponent<goalTrigger>().goalNumber;
//			Vector3 goalPos = Goals[k].GetComponent<goalTrigger>().goalPosition;
//			string goalCol = Goals[k].GetComponent<goalTrigger>().colObjectName;
//
//			//Vector4 goalData = Vector4(goalPos,goalCol);
//
//			// Thread data (raw sensor)
//			StreamWriter gp = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Goal_" + goalNum + "_goalPos.txt");
//
//			gp.WriteLine(goalPos);
//	
//			gp.Close();
//
//
//
//			StreamWriter gc = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "_Goal_" + goalNum + "_goalCol.txt");
//
//			gc.WriteLine(goalCol);
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
		
}
