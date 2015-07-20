using UnityEngine;
using System.Collections;

public class InstructionAnimation : MonoBehaviour {
	public Texture2D[] frames;
	public float framesPerSecond = 10.0f;
	public float frameVal;
	public float proximity = 20;
	private bool isToGiberish;
	private float frameValRoof;
	private float frameValFloor;
	private int index
	{
		get{return (int)(frameVal * framesPerSecond);}
	}
	public float distToPlayer
	{
		get{return Vector3.Distance (transform.position, PlayerLogic.main.transform.position); }
	}
	private bool playerNear
	{
		get
		{
			if (distToPlayer < proximity)
				return true;
			return false;
		}
	}

	void Start () 
	{
		frameValRoof = (frames.Length-1) / framesPerSecond;
		frameValFloor = 0;

		GetComponent<Renderer>().material.mainTexture = frames[0];
		GetComponent<Renderer>().material.SetTexture("_EmissionMap", frames[0]);
	}

	void Update () {
		if (playerNear) 
		{
			ToEnglish ();
		} else 
		{
			ToGiberish ();
		}
		// if animating to english...
		if (!isToGiberish) {
			//if frameVal over limit, limit and end
			if (frameVal >= frameValRoof) 
			{
				frameVal = frameValRoof;
				return;
			}
			frameVal += Time.deltaTime;
		}
		// if animating to giberish
		if (isToGiberish) {
			//if frameVal under limit, limit and end
			if (frameVal <= frameValFloor)
			{
				frameVal = frameValFloor;
				return;
			}
			frameVal -= Time.deltaTime;
		}
		
		if (index > frames.Length-1)
			return;
		GetComponent<Renderer>().material.mainTexture = frames[index];
		GetComponent<Renderer>().material.SetTexture("_EmissionMap", frames[index]);
	}

	//This one is probably pointlessly redundant...
	private void ToEnglish()
	{
		isToGiberish = false;
	}
	//This one also
	private void ToGiberish()
	{
		isToGiberish = true;
	}
}