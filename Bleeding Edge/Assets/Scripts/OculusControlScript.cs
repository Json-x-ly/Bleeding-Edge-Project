using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class OculusControlScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (CrossPlatformInputManager.GetButtonDown ("Reset_Orient")) {
			UnityEngine.VR.InputTracking.Recenter();
			//UnityEngine.VR.VRSettings.showDeviceView = true;
		}
		if (CrossPlatformInputManager.GetButtonDown ("Submit")) {
			PauseMenuScript.main.ShowMenu();
			CowlBehaivor.main.ToBlack();
		}
	}
}
