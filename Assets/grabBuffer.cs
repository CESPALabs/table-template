/// <summary>
/// grabBuffer.cs: script to create buffer stream of trialData from plStream
/// </summary>

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

public class grabBuffer : MonoBehaviour {

	private PlStream plstream;

	//public Vector3 pol_position;
	//public Vector4 pol_rotation;
	public List<Vector3> pol_positions = new List<Vector3>();
	public List<Vector4> pol_rotations = new List<Vector4>();

	public float countdown = 10;
	public List<string> Clockhardware = new List<string>();
	public List<string> Clockupdate = new List<string>(); // better to attach to dataManager.cs



	void Awake () {

		// get the stream component from PlStream.cs
		plstream = GetComponent<PlStream>();

	}

	void Start () {

		// start Polhemus data thread in background called faster than framerate
		U3D.Threading.Dispatcher.Initialize ();
		CreateThread();
	
	}
	
	// Update is called once per frame
	void Update () {
		countdown = countdown - Time.deltaTime;

		if (countdown < 0)
		{
			save_buffer();
		}

		// get the time (add to list)
		Clockupdate.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));

	
	}

	private void get_buffer()
	{

		Vector3 pol_position = plstream.positions[1];
		Vector4 pol_rotation = plstream.orientations[1];

		pol_positions.Add(pol_position);
		pol_rotations.Add(pol_rotation);


	}

	private void save_buffer()
	{
		StreamWriter sd = new StreamWriter("test.txt");

		foreach(Vector3 sp in pol_positions)
				{
					sd.WriteLine(sp);
				}

		sd.Close();

		StreamWriter shc = new StreamWriter("hardclock.txt");

		foreach(string ch in Clockhardware)
		{
			shc.WriteLine(ch);
		}

		shc.Close();

		StreamWriter suc = new StreamWriter("updateclock.txt");

		foreach(string cu in Clockupdate)
		{
			suc.WriteLine(cu);
		}

		suc.Close();
	}




	public void CreateThread()
	{
		Task.Run (() => {
			//int totSampleint = Convert.ToInt32(totSample);
			// [...] Code to be executed in auxiliary thread
			for(int i=0; i<1000; i++)
			{
				//  this for loop iterates through the data collection
				int value = i;
//				polhemusSample = i;
				get_buffer();
				Clockhardware.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));
				MockUpSomeDelay();
			}
		});//.ContinueInMainThreadWith(Update);
	}

	void MockUpSomeDelay()
	{
		System.Threading.Thread.Sleep (0); // sleep between steps in milisec
	}
}
