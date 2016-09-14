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

public class movedObjectPos : MonoBehaviour {

	public string participantNum;
	public int trialNumber;
	public string objectnumber;

	public List<Vector3> objectPos = new List<Vector3>();
	//public List<string> Clockupdate = new List<string>();
	public List<string> parentName = new List<string>(); // gets name of parent object


	// Use this for initialization
	void Start () {
		objectnumber = gameObject.name.Substring(6,1);
	}
	
	// Update is called once per frame
	void Update () {
		//Clockupdate.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));
		objectPos.Add(transform.position);
	
		parentName.Add(transform.parent.name);

	}

	void saveall () {
//		// Thread data (raw sensor)
//		StreamWriter op = new StreamWriter("P" + participantNum + "Trial_" + trialNum + "Tracker_" + sensorNum + "sensor.txt");
//		foreach(Vector3 t in objectPos)
//		{
//			op.WriteLine(t);
//		}
//		op.Close();
	}
//

}
