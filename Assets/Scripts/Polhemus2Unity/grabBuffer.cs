﻿/// <summary>
/// grabBuffer.cs: script to grab and save data from the plStream (saves all active sensors)
/// NOTE: saves at completion of a trial (this saves memory)
/// </summary>

using UnityEngine;
using System.Collections;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using UnityEngine.UI;
using System.IO;
using System;
using System.Threading;
using UnityEngine.SceneManagement;

public class grabBuffer : MonoBehaviour {

	private PlStream plstream;

	private int trialNumber;

	public List<Vector4> pol_positions = new List<Vector4>();
	public List<Vector4> pol_rotations = new List<Vector4>();

	public float countdown = 10;
	public List<string> Clockhardware = new List<string>();
	public List<string> Clockupdate = new List<string>(); // better to attach to dataManager.cs

	private Thread conThread;
	public int start_line;
	public int end_line;




	void Awake () {

		// get the stream component from PlStream.cs
		plstream = GameObject.Find("Polhemus").GetComponent<PlStream>();

	}

	void Start () {
		// grab trial number from the System Preferences:
//		trialNumber = 

		// get the active thread from the plstream:
		conThread = plstream.conThread;
		start_line = plstream.buffer_line;
		trialNumber = PlayerPrefs.GetInt("trialnumber");
	
	}
	
	// Update is called once per frame
	void Update () {

		// countdown for testing:
		countdown = countdown - Time.deltaTime;


		if (countdown < 0)
		{
			//plstream.enabled = false;
			//conThread.Abort();
			get_buffer(); // get the data
			save_buffer(); // save the data

			SceneManager.LoadScene("ITI");
		}

		// get the time (add to list)
		Clockupdate.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));

	
	}

	private void get_buffer()
	{
		end_line = plstream.buffer_line;

		for (int line = start_line; line < end_line; line++){
			Vector4 posData = plstream.posData[line];
			Vector4 rotData = plstream.rotData[line];
			pol_positions.Add(posData);
			pol_rotations.Add(rotData);
		}

	}

	private void save_buffer()
	{
		StreamWriter sd = new StreamWriter("trial_" + trialNumber + ".polhemus");

		foreach(Vector4 sp in pol_positions)
				{
					sd.WriteLine(sp);
				}

		sd.Close();

//		StreamWriter shc = new StreamWriter("hardclock.txt");
//
//		foreach(string ch in Clockhardware)
//		{
//			shc.WriteLine(ch);
//		}
//
//		shc.Close();

		StreamWriter suc = new StreamWriter("updateclock.txt");

		foreach(string cu in Clockupdate)
		{
			suc.WriteLine(cu);
		}

		suc.Close();
	}

}
