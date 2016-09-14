// ---------------------------------------
///	<summary>
/// 
/// File: splashScreen.cs
/// 
/// Keeps splashcreen scene on for N seconds (splashLoadtime)
/// 
/// </summary>
// ---------------------------------------


using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;



public class splashScreen : MonoBehaviour {

	public float splashLoadtime = 5;

	 
	// Use this for initialization
	IEnumerator Start () {

		PlayerPrefs.DeleteAll();

		yield return new WaitForSeconds( splashLoadtime );

		// if using polhemus:
//		SceneManager.LoadScene( "Calibration" );

		// if not, no need to calibrate:
		SceneManager.LoadScene( "PlayerInfo" );


	}

	void Update () {

	}

}