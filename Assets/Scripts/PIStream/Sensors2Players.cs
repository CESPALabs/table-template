/// <summary>
/// Sensors2players.cs: a script that takes the active Player controlled objects in UNITY and
/// pipes the PL stream to them. Both this and PlStream should be attached to same general object
/// </summary>

//  VSS $Header: /PiDevTools11/Inc/PDIg4.h 18    1/09/14 1:05p Suzanne $  

using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class Sensors2Players : MonoBehaviour {
	// create object for datastream from PIStream.cs
	private PlStream plstream; 

	// uses origin and scale parameters from manualCalibration.cs
	private Vector3 prime_position;
	private Vector2 table_scale;

	// make public positions and orientations
	// useful for sending to other scripts (e.g., calibrations)

	// creates array to popluate with player controlled objects
	public GameObject[] players;

	public int Plactive;
	public bool[] active;

	// unknown
	private int[] dropped;

	// Use this for initialization
	void Awake () {

		// get the stream component from PlStream.cs
		plstream = GameObject.Find("Polhemus").GetComponent<PlStream>();

		// get players

				
		//players = GameObject.FindGameObjectsWithTag("Player");
		players = GameObject.FindGameObjectsWithTag("object");
		dropped = new int[players.Length];

	}
	
	void Start () {

		//plstream.conThread.Start();
		// initializes arrays, fixes positions
		active =  plstream.active;

		zero();
		prime_position = new Vector3(PlayerPrefs.GetFloat("xOrigin"), PlayerPrefs.GetFloat("yOrigin"), PlayerPrefs.GetFloat("zOrigin")) ;
		table_scale = new Vector2(PlayerPrefs.GetFloat("xScale"), PlayerPrefs.GetFloat("yScale")); 
		//table_scale = new Vector2(1, 1); 
	}

	// Update is called once per frame
	void Update () {

		// allow us to escape application
		if (Input.GetKeyDown("escape"))
			Application.Quit();
	}

	// called before performing any physics calculations
	void FixedUpdate()
	{
		Plactive = plstream.active.Length;
		// for each Player up to sensors slider value, update the position
		for (int i = 0; plstream != null && i < plstream.active.Length; ++i)
		{
			if (plstream.active[i])
			{
				Vector4 plstream_pos = plstream.positions[i];
				Vector3 pol_position = new Vector3(plstream_pos.x, plstream_pos.y, -1f* plstream_pos.z+3f) *.01f - prime_position;
				Vector4 pol_rotation = plstream.orientations[i];

				// doing crude (90 degree) rotations into frame
				Vector3 unity_position;
				unity_position.x = pol_position.x / table_scale.x;
				unity_position.y = pol_position.y / table_scale.y;
				unity_position.z = pol_position.z + 0.1f;


				Quaternion unity_rotation;
				unity_rotation.w = pol_rotation[0];
				unity_rotation.x = -pol_rotation[2];
				unity_rotation.y = pol_rotation[3];
				unity_rotation.z = -pol_rotation[1];
				//unity_rotation = Quaternion.Inverse(unity_rotation);

				if (!players[i].activeSelf)
					players[i].SetActive(true);
				players[i].transform.position = unity_position;
				//players[i].transform.rotation = unity_rotation;

				// set deactivate frame count to 10
				dropped[i] = 10;

				if (plstream.digio[i] != 0)
				{
					zero();
				}
			}
			else
			{
				if (players[i].activeSelf)
				{
					dropped[i] -= 1;
					if (dropped[i] <= 0)
						players[i].SetActive(false);
				}
			}
		}
	}

	public void zero()
	{
		for (var i = 0; i < players.Length; ++i)
			players[i].transform.position = new Vector3(-1000, -1000, -1000);

		for (var i = 0; i < dropped.Length; ++i)
			dropped[i] = 0;

		for (var i = 0; i < plstream.active.Length; ++i)
		{
			if (plstream.active[i])
			{
				prime_position = plstream.positions[i];
				break;
			}
		}
	}
}
