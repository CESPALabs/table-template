using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using System.IO;
using System;

public class setEnvironment : MonoBehaviour {

	public GameObject[] walls;
	public GameObject[] objects;
	public float leftwallY;
	public float rightwallY;
	public float midwallY;

	public float leftYchange;

	public List<string> trialNumberList = new List<string>();
	public List<string> changeDiscList =  new List<string>();
	public List<string> leftWallList =  new List<string>();



	// Use this for initialization
	void Start () {

		string iniFileame = "trialData.txt";
		string [] lines = File.ReadAllLines (iniFileame);

		foreach(string line in lines){
			//split by delimiter
			string [] values = line.Split(new string[] {"\t"}, StringSplitOptions.None);
			trialNumberList.Add(values[0]);
			changeDiscList.Add(values[1]);
			leftWallList.Add(values[2]);
			// do for other walls

			leftwallY = GameObject.Find("left.Wall").GetComponent<Transform>().localScale.y;


		}
		//leftwall.GetComponent<Transform>.
	}

	// read in lines of trial data info; sets up environment:



			// create lists for each trial parameter
				

		
	
	// Update is called once per frame
	void Update () {
		leftwallY = leftYchange;
	}
}
