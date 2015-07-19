using UnityEngine;
using System.Collections;

public class InstructionAnimation : MonoBehaviour {
	public Texture2D[] frames;
	public float framesPerSecond = 10.0f;
	
	void Update () {
		int index = (int)(Time.time * framesPerSecond);
		index = index % frames.Length;
		GetComponent<Renderer>().material.mainTexture = frames[index];
		GetComponent<Renderer>().material.SetTexture("_EmissionMap", frames[index]);
	}
}