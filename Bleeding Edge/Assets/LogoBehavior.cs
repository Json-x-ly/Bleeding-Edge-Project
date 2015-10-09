using UnityEngine;
using System.Collections;

public class LogoBehavior : MonoBehaviour {
	public void AnimationFinished()
	{
		Application.LoadLevel ("Level_01");
	}
	public void cowlTrigger()
	{
		CowlBehaivor.main.ToBlack ();
	}
}
