using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class loadNextTrial : MonoBehaviour {

	public float trialLoadtime = 5;

	// Use this for initialization
	IEnumerator Start () {

		yield return new WaitForSeconds( trialLoadtime );

		SceneManager.LoadScene( "2.TableTemplate" );
	
	}
}
