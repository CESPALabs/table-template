using UnityEngine;
using System.Collections;

public class playerPosStream  {

	string timeData;
	int sensorNum;
	Vector3 positionData;

	public playerPosStream(int sensor, Vector3 PolhemusPosition, string time)
	{
		timeData = time;
		sensorNum = sensor;
		positionData = PolhemusPosition;
	}
}

