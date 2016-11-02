/// <summary>
/// changeDiscSize.cs
/// 
/// Function: changes the size of the disc as a function of distance to wall
/// Affects: GameObjects found using line 36
/// 
/// Attach to: Tabletop
/// 
/// </summary>


using UnityEngine;
using System.Collections;


//As the disc moves towards the wall (y-axis?), it needs to get slightly larger 
//First: find the disc
//Second: grab the distance between the disc and the wall
//Third: grab the scale (x,y) of the disc
//Fourth: change the scale/size of the disc as it approaches the wall


public class ChangeDiscSize : MonoBehaviour {

	public GameObject disc;
	public Vector3 discScaleChange;
	public Vector3 discPosition;
	//public float discPosition;
	public GameObject wall;
	public float wallPlane=0;
	public float Xdist; // current distance
	public float Xdist_old; // previous distance
	public float xPosition;
	public bool passedCrit; // have we passed the critical line?
	float critline; // value when passed disc gets larger
	float bline;
	public float rescale;//
	public float discSizeOriginal,discSizeChanged,discSizeMax;
	public float timeDelta;


	// Use this for initialization
	void Start () {
		disc = GameObject.Find ("Object1");
		wall = GameObject.Find ("mid.Wall");
		discScaleChange = new Vector3 (0f, 0f, 0f);
		passedCrit = false;
		critline = .32f;
		bline = .4f;
		discSizeOriginal = .12f;
		discSizeChanged = discSizeOriginal;
		discSizeMax = .125f;
		disc.gameObject.transform.localScale = new Vector3(discSizeOriginal,discSizeOriginal,discSizeOriginal);


	}

	// Update is called once per frame
	void Update () {
		//timeDelta=Time.deltaTime;
		//capture position of disc: does only capturing the x-scale work?
		xPosition = disc.transform.position.x;

		//distance from disc to wall = Xdist
		Xdist = Mathf.Abs(xPosition - wallPlane);

		discSizeOriginal = discSizeChanged;
		//change the x and y scale of disc as it approaches x-axis
		//stop when critical line


		if (disc.transform.position.x < critline) {
			passedCrit = true;
		}

		if (passedCrit == true) 
		// if we are closer than we were before, then change size
		{
			discSizeChanged=Mathf.Lerp(discSizeOriginal,discSizeMax,.5f);
			rescale = discSizeChanged;

		} else{
			// else... keep size the same as on previous update
			rescale = discSizeOriginal;

		}
			
	

		disc.gameObject.transform.localScale = new Vector3 (rescale, rescale, .12f);


		if (disc.transform.position.x > bline) { 
			//disc.gameObject.transform.localScale = new Vector3 (.12f, .12f, .12f);
		}

		Xdist_old = Xdist;




	}
}