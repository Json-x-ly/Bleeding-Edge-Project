using UnityEngine;
using System.Collections;

public class SpotlightDetectionScript : MonoBehaviour {
	public bool playerDetected{
		get{ return checkPlayerDetection();}
	}
	public float distToPlayer{
		get{return Vector3.Distance (transform.position, PlayerLogic.main.transform.position); }
	}
	//private Light spotLight;
	public Light spotLight;
	
	void Awake () {
		spotLight = gameObject.GetComponent<Light> ();
		if (spotLight == null) {
			Debug.Log ("Light found in child and added to script");
			//spotLight = this.gameObject.AddComponent<Light>();
			spotLight = this.gameObject.GetComponentInChildren<Light>();
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	
	bool checkPlayerDetection () {
		if (distToPlayer > spotLight.range)
			return false;
		Vector3 vectToTarget = PlayerLogic.main.transform.position - transform.position;
		Vector3 forwardVect = transform.forward;
		float angleToPlayer = Vector3.Angle (forwardVect, vectToTarget);
		//Debug.Log ("distance to player:" + distToPlayer);
		//Debug.Log ("angle to player:" + angleToPlayer + " >= " + spotLight.spotAngle * 0.5f);
		if (angleToPlayer >= spotLight.spotAngle * 0.5f)
			return false;
		Ray rayToTarget = new Ray (transform.position, vectToTarget.normalized);
		
		return isPlayerHit (rayToTarget);
	}
	
	bool isPlayerHit(Ray ray){
		float dist = distToPlayer + 1;
		RaycastHit hit;
		if (Physics.Raycast (ray, out hit, dist)) {
			Debug.DrawRay(transform.position, ray.direction * Vector3.Distance(transform.position, hit.transform.position), Color.red);
			Debug.Log(hit.transform.name);
			if (hit.transform.tag == "Player")//("Player"))
			{
				Debug.Log ("Player tag found");
				return true;
			}
			return false;
		}
		return false;
	}
}