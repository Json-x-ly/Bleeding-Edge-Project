using UnityEngine;
using System.Collections;

public class PauseMenuScript : MonoBehaviour {
	public static PauseMenuScript main;
	public GameObject pauseMenu;
	public GameObject deadScreen;

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
		pauseMenu.SetActive (false);
	}

	public void ShowMenu() {
		gameObject.SetActive (true);
		pauseMenu.SetActive (true);
	}

	public void IAmNowDead() {
		gameObject.SetActive (true);
		pauseMenu.SetActive (false);
		deadScreen.SetActive (true);
	}
}
