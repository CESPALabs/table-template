/// <summary>
/// grabBuffer.cs: script to grab and save data from the plStream (saves all active sensors)
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

	public List<Vector4> pol_positions = new List<Vector4>();
	public List<Vector4> pol_rotations = new List<Vector4>();

	public float countdown = 10;
	public List<string> Clockhardware = new List<string>();
	public List<string> Clockupdate = new List<string>(); // better to attach to dataManager.cs

	private Thread conThread;




	void Awake () {

		// get the stream component from PlStream.cs
		plstream = GetComponent<PlStream>();

	}

	void Start () {

		// get the active thread from the plstream:
		conThread = plstream.conThread;
	
	}
	
	// Update is called once per frame
	void Update () {
		countdown = countdown - Time.deltaTime;

		if (countdown < 0)
		{
			conThread.Abort(); // stop the thread
			get_buffer(); // get the data
			save_buffer(); // save the data
		}

		// get the time (add to list)
		Clockupdate.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));

	
	}

	private void get_buffer()
	{

		pol_positions = plstream.posData;
		pol_rotations = plstream.rotData;

	}

	private void save_buffer()
	{
		StreamWriter sd = new StreamWriter("test.txt");

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
