using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.EventSystems;

public class MenuScript : MonoBehaviour {

	public Canvas quitMenu;
	public Button startText;
	public Button exitText;
	public EventSystem eventSystem;

	void Start (){
		quitMenu = quitMenu.GetComponent<Canvas>();
		startText = startText.GetComponent<Button>();
		exitText = exitText.GetComponent<Button>();
		quitMenu.enabled = false;
	}
	//this function will be used on our Exit button
	public void ExitPress() {
		quitMenu.enabled = true; //enable the Quit menu when we click the Exit button
		startText.enabled = false; //then disable the Play and Exit buttons so they cannot be clicked
		exitText.enabled = false;	
		eventSystem.SetSelectedGameObject(exitText.gameObject);
	}

	//this function will be used for our "NO" button in our Quit Menu
	public void NoPress() {
		quitMenu.enabled = false; //we'll disable the quit menu, meaning it won't be visible anymore
		startText.enabled = true; //enable the Play and Exit buttons again so they can be clicked
		exitText.enabled = true;
	}
	//this function will be used on our Play button
	public void StartLevel () {
		Application.LoadLevel ("Level_01"); //this will load our first level from our build settings. "2" is the second scene in our game
	}
	//This function will be used on our "Yes" button in our Quit menu
	public void ExitGame () {
		Application.Quit(); //this will quit our game. Note this will only work after building the game
	}

}
