/// <summary>
/// Manual calibrate. Uses raw polhemus data from 4 positions (corners) of table to scale polhemus space to table space.
/// 
/// This script returns tableOrigin (polhemus position that corresponds to 0,0,0 on table) and tableScale which is used by Sensors2Players.cs.
/// 
/// When using attach to TableBG in calibration scene.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;
 

public class manualCalibrate : MonoBehaviour {

	public float grabHeight;
	public List<float> xpos, ypos, zpos;
	public int targets = 0;
	public Vector3 PDIposition = new Vector3();
	public static Vector3 tableOrigin = new Vector3();
	public static Vector3 crossProd = new Vector3();
	public static Vector2 tableScale = new Vector3();
	static float xOrigin, yOrigin, zOrigin, xScale, yScale; 
	GameObject calibrationPoints;
	public GameObject removePoint;
	string targetNumber;
	public string pointNumber;
	private Vector3 v1, v2;

	private PlStream plstream;  
	public int Plactive;
	private int[] dropped;
	public int i;

	public GameObject[] players;

	private GameObject polhemus;



	public Vector3 getVirtualOrigin(){
		// calculate origin
		xOrigin = (xpos [0] + xpos [1] + xpos [2] + xpos [3]) / 4;
		yOrigin = (ypos [0] + ypos [1] + ypos [2] + ypos [3]) / 4;
		zOrigin = (zpos [0] + zpos [1] + zpos [2] + zpos [3]) / 4;

		// save origin
		PlayerPrefs.SetFloat("xOrigin", xOrigin);
		PlayerPrefs.SetFloat("yOrigin", yOrigin);
		PlayerPrefs.SetFloat("zOrigin", zOrigin);

		// return for display
		tableOrigin = new Vector3 (xOrigin, yOrigin, zOrigin);
		return tableOrigin;

		}

//	float getVirtualSurface(){
//		List<float> surfacePos = new List<float>(){zpos [0],zpos [1],zpos [2],zpos [3]};
//		float surfacePlane = surfacePos.Max();
//	}

	public Vector2 getTableScale(){
		float xDist = ((xpos [0] - xpos [1]) + (xpos [0] - xpos [2]) + (xpos [3] - xpos [1]) + (xpos [3] - xpos [2]))/4f; 
		xScale = xDist/0.8f;
	
		float yDist = ((ypos [0] - ypos [2]) + (ypos [0] - ypos [3]) + (ypos [1] - ypos [2]) + (ypos [1] - ypos [3]))/4f; 
		yScale = yDist/1.4f;

		PlayerPrefs.SetFloat("xScale", xScale);
		PlayerPrefs.SetFloat("yScale", yScale);

		tableScale = new Vector2 (xScale,yScale);
		return tableScale;
	}
		

	void Awake () {





	}

	void Start (){
		// get the stream component from PlStream.cs
		plstream = GameObject.Find("Polhemus").GetComponent<PlStream>();

		// get players
		players = GameObject.FindGameObjectsWithTag("Player");
		dropped = new int[players.Length];
		}


	// Update is called once per frame

	void Update()
	{
		Plactive = plstream.active.Length;
		// for each Player up to sensors slider value, update the position
		for (int i = 0; plstream != null && i < plstream.active.Length; ++i)
		{
			if (plstream.active[i])
			{
				Vector4 plstream_pos = plstream.positions[i];
				Vector3 pol_position = new Vector3(plstream_pos.x, plstream_pos.y, plstream_pos.z) * .01f;
				Vector4 pol_rotation = plstream.orientations[i];

				// doing crude (90 degree) rotations into frame
				Vector3 unity_position;
				unity_position.x = pol_position.x;
				unity_position.y = pol_position.y;
				unity_position.z = -1f * pol_position.z;


				Quaternion unity_rotation;
				unity_rotation.w = pol_rotation[0];
				unity_rotation.x = -pol_rotation[2];
				unity_rotation.y = pol_rotation[3];
				unity_rotation.z = -pol_rotation[1];
				//unity_rotation = Quaternion.Inverse(unity_rotation);

				if (!players[i].activeSelf)
				players[i].SetActive(true);
				players[i].transform.position = unity_position;
				players[i].transform.rotation = unity_rotation;

				// set deactivate frame count to 10
				dropped[i] = 10;

				PDIposition = unity_position;

				//transform.position = PDIposition;

				if (Input.GetKeyDown("space"))
				{
					xpos.Add(PDIposition[0]);
					ypos.Add(PDIposition[1]);
					zpos.Add(PDIposition[2]);

					targets++;
					targetNumber = targets.ToString();
					pointNumber = string.Concat("point",targetNumber);

					GameObject removePoint = GameObject.Find(pointNumber);
					Destroy(removePoint);

				} else {

				}

				if (targets > 3) {

					getVirtualOrigin();
					getTableScale();




					Debug.Log("Calibration Completed");
					Debug.Log (tableOrigin);



					if (Input.GetKeyDown("s"))
					{
						SceneManager.LoadScene("TableActive");

						//plstream.conThread.Abort();

					}
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
		
}

