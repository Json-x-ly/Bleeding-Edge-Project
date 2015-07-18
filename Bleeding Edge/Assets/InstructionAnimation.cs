using UnityEngine;
using System.Collections;

public class InstructionAnimation : MonoBehaviour {
	public Texture2D[] frames;
	public float framesPerSecond = 10.0f;
	public bool toggle;
	public float frameVal;
	
	void Update () {
		if (!toggle) {
			frameVal += Time.deltaTime;
		}
		if (toggle) {
			frameVal -= Time.deltaTime;
		}
		int index = (int)(frameVal * framesPerSecond);

		if (index > frames.Length)
			return;
		GetComponent<Renderer>().material.mainTexture = frames[index];
		GetComponent<Renderer>().material.SetTexture("_EmissionMap", frames[index]);
	}

	void ToEnglish()
	{

	}
	void ToGiberish()
	{
		 
	}
}