/// <summary>
/// Timer check. This was used for debugging. Test sampling rate of Update thread and multithread.
/// ONLY USE FOR TESTING PURPOSES.
/// 
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine.SceneManagement;
using System;
using U3D.Threading.Tasks;




public class TimerCheck : MonoBehaviour {

	public float countdown = 10;
	public float sampleRate = .01f;
	private DateTime oldtime;
	public decimal deltaT;


	//public float totSample = 1000;
	public int totSampleint = 1000;
	private float updateTimer;
	private float fixedTimer;
	private List<float> updateTimerList = new List<float>();
	private List<float> fixedTimerList = new List<float>();
	//public int numFrames = 2000;

	private string updateTS, fixedTS;
	//private HashSet<DateTime> updateTSList = new HashSet<DateTime>();
	//private HashSet<DateTime> fixedTSList = new HashSet<DateTime>();

	private List<string> updateTSList = new List<string>();
	private List<string> fixedTSList = new List<string>();

	public int fixedSample, updateSample = 0;




	// Use this for initialization
	void Start () {
		//InvokeRepeating("GetDataFast",0.01f,0.01f);
		//StartCoroutine("GetDataFast");
		//totSample = countdown / sampleRate; 
		U3D.Threading.Dispatcher.Initialize ();
		CreateThread();
	}
	
	// Update is called once per frame
	void Update () {
		updateSample++;
		//updateTimer = Time.deltaTime;
		//var updateTSList = new HashSet<DateTime>();
		//updateTS = DateTime.Now.ToString("hh:mm:ss.ffffff");
		//updateTimerList.Add(updateTimer);
		updateTSList.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));

		countdown -= Time.deltaTime;


		if (countdown < 0){
			saveAll();
			//GetDataFast.AbortThread();
			SceneManager.LoadScene("1.splashScreen");		
		}

			
			
	}
		
	 void GetDataFast () {
		//deltaT = Convert.ToDecimal(DateTime.UtcNow - oldtime);
		//fixedTimer = DateTime.UtcNow;
	//	var fixedTimestamp = System.DateTime;
		//fixedTS = Time.realtimeSinceStartup.ToString("hh.mm.ss.ffffff");
		//var fixedTSList = new HashSet<DateTime>();
		//fixedTimerList.Add(DateTime.UtcNow);
		//oldtime = DateTime.UtcNow;
		fixedTSList.Add(DateTime.UtcNow.ToString("hh:mm:ss.ffffff"));

	}
//		
//	IEnumerator GetDataFast () {
//
//		for (int i = 0; i < 2000; i++){
//
//			fixedTimer = Time.realtimeSinceStartup;
//			//	var fixedTimestamp = System.DateTime;
//			//fixedTS = Time.realtimeSinceStartup.ToString("hh.mm.ss.ffffff");
//
//			fixedTimerList.Add(fixedTimer);
//			fixedTSList.Add(fixedTS);
//			
//			yield return new WaitForSeconds(0.01f);
//		}
//	}

	void saveAll()
	{
//		StreamWriter ut = new StreamWriter("updateTimer.txt");
//		foreach(float t in updateTimerList)
//		{
//			ut.WriteLine(t);
//		}
//		ut.Close();
//
//		StreamWriter ft = new StreamWriter("fixedTimer.txt");
//		foreach(float t in fixedTimerList)
//		{
//			ft.WriteLine(t);
//		}
//		ft.Close();

		StreamWriter tts = new StreamWriter("updateTimeStamps.txt");
		foreach(string t in updateTSList)
		{
			tts.WriteLine(t);
		}
		tts.Close();

		StreamWriter fts = new StreamWriter("fixedTimeStamps.txt");
		foreach(string t in fixedTSList)
		{
			fts.WriteLine(t);
		}
		fts.Close();
	}

	////////////// THREADING ///////////////////
	public void CreateThread()
	{
		Task.Run (() => {
			//int totSampleint = Convert.ToInt32(totSample);
			// [...] Code to be executed in auxiliary thread
			for(int i=0; i<10000; i++)
			{
				int value = i;
				fixedSample = i;
				GetDataFast();
				MockUpSomeDelay();
			}
		});//.ContinueInMainThreadWith(Update);
	}

	void MockUpSomeDelay()
	{
		System.Threading.Thread.Sleep (8);
	}
//	void CheckTaskExecution(Task t)
//	{
//		if (t.IsFaulted) 
//		{
//			log.text+= "<color=#550000>Error executing task: " + t.Exception + "</color>\n";
//		}
//		else
//		{
//			log.text+= "<color=#005500>Tasks executed successfully</color>\n";
//		}
//		EnableButtons();
//	}
}
