/// <summary>
/// Manual calibrate. Uses raw polhemus data from 4 positions (corners) of table to scale polhemus space to table space.
/// 
/// This script returns tableOrigin (polhemus position that corresponds to 0,0,0 on table) and tableScale which is used by Sensor.cs.
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
	public static Vector2 tableScale = new Vector3();
	static float xOrigin, yOrigin, zOrigin, xScale, yScale; 
	GameObject calibrationPoints;
	public GameObject removePoint;
	string targetNumber;
	public string pointNumber;


	public Vector3 getVirtualOrigin(){
				xOrigin = (xpos [0] + xpos [1] + xpos [2] + xpos [3]) / 4;
				yOrigin = (ypos [0] + ypos [1] + ypos [2] + ypos [3]) / 4;
				zOrigin = (zpos [0] + zpos [1] + zpos [2] + zpos [3]) / -4;

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

		tableScale = new Vector2 (xScale,yScale);
		return tableScale;
	}



	void Start (){
		GameObject table = GameObject.Find("tableBG");
		//Countdown timeScript = table.GetComponent<Countdown>();
		}


	// Update is called once per frame
	void Update () 
	{
		//targetText.text = targets.ToString();
		//RaycastHit hit;
		//Ray grabbingRay = new Ray (transform.position, Vector3.forward);
		//CharacterController charContr = GetComponent<CharacterController>();
		//Debug.DrawRay (transform.position, Vector3.forward * grabHeight);



		PDIposition = VRPN.vrpnTrackerPos("TrackerJohn@localhost",1);

		transform.position = PDIposition;

		if (Input.GetKeyDown("space"))
		//if (Physics.Raycast (grabbingRay, out hit, grabHeight) && hit.collider.tag=="calibrate")
		{
			//Debug.Log ("hit the avatar");
			xpos.Add(PDIposition[0]);
			ypos.Add(PDIposition[1]);
			zpos.Add(PDIposition[2]);





			//				hit.collider.transform
			//movedCircle = GameObject.FindWithTag("avatar");
			//Destroy(hit.transform.gameObject);
			//touchedCircle = caliCircles.GetChild(targets);
		//	DestroyObject(touchedCircle);
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
			//getVirtualSurface();

			

			Debug.Log("Calibration Completed");
			Debug.Log (tableOrigin);



			if (Input.GetKeyDown("s"))
			    {
				SceneManager.LoadScene("TableActive");
				}
			}
	}
}

