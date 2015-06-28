using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	public static PauseMenuScript main;
	void Awake(){
		if (main == null) {
			main = this;
		} else {
			Debug.LogError("Scene Already has a PauseMenuScript");
			Destroy(gameObject);
		}
	}

	void Start() {
		gameObject.SetActive(false);
	}

	public void ExitGame(){
		Application.Quit ();
	}

	public void HideMenu(){
		gameObject.SetActive(false);
	}

	public void ShowMenu() {
		gameObject.SetActive (true);
	}
}
