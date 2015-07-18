using UnityEngine;
using System.Collections;
using UnityEngine.VR;

public class OculusStartScript : MonoBehaviour {

	// Use this for initialization
	void Awake() {
		VRSettings.loadedDevice = VRDeviceType.Oculus;
		if (VRSettings.loadedDevice != VRDeviceType.None) {
			VRSettings.enabled = true;
		}
	}
}
