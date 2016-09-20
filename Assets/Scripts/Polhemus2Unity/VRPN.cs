/// <summary>
/// VRPN. Commands that connect VRPN to Unity environment. This is what streams the data in. Should be attached to a permanent object (e.g., camera or table itself.
/// </summary>
using UnityEngine;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;


public static class VRPN {
	[DllImport ("unityVrpn")]
	private static extern double vrpnAnalogExtern(string address, int channel, int frameCount);

	[DllImport ("unityVrpn")]
	private static extern bool vrpnButtonExtern(string address, int channel, long frameCount);
	
	[DllImport ("unityVrpn")]
	private static extern double vrpnTrackerExtern(string address, int channel, int component, long frameCount);
	
	public static double vrpnAnalog(string address, int channel){
		return vrpnAnalogExtern (address, channel, Time.frameCount);
	}
	
	public static bool vrpnButton(string address, int channel){
		return vrpnButtonExtern (address, channel, Time.frameCount);	
	}
	
	public static Vector3 vrpnTrackerPos(string address, int channel){
		return new Vector3(
			(float) vrpnTrackerExtern(address, channel, 0, DateTime.Now.Ticks),
			(float) vrpnTrackerExtern(address, channel, 1, DateTime.Now.Ticks),
			(float) vrpnTrackerExtern(address, channel, 2, DateTime.Now.Ticks));
	}
	
	public static Quaternion vrpnTrackerQuat(string address, int channel){
		return new Quaternion(
			(float) vrpnTrackerExtern(address, channel, 3, Time.frameCount),
			(float) vrpnTrackerExtern(address, channel, 4, Time.frameCount),
			(float) vrpnTrackerExtern(address, channel, 5, Time.frameCount),
			(float) vrpnTrackerExtern(address, channel, 6, Time.frameCount));
	}
}
