/// <summary>
/// inputPlayer.cs
/// 
/// run in PlayerInfo scene; sfter entered moves to calibration screen
/// 
/// </summary>


using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class inputPlayer : MonoBehaviour {

	public string playerName;
	public int participantNumber;

	public void SetPlayerName (string value){
		playerName = value;
		Debug.Log ("playerName is: " + playerName);
		participantNumber = int.Parse(playerName);

		PlayerPrefs.SetString("playerName", playerName);

		SceneManager.LoadScene("Calibration");
		//SceneManager.LoadScene("BlankTable");

	}


}
