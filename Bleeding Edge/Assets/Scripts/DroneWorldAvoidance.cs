using UnityEngine;
using System.Collections;

public class DroneWorldAvoidance : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Avoidance ();
	}

	void Avoidance()
	{
		//ugly code ahead!
		Ray right = new Ray(transform.position,     (transform.forward + transform.right));
		Ray left = new Ray(transform.position,      (transform.forward + -transform.right));
		Ray up = new Ray(transform.position,        (transform.forward + transform.up));
		Ray down = new Ray(transform.position,      (transform.forward + -transform.up));
		
		float hori = IsHit(left);
		hori -= IsHit(right);
		float vert=IsHit(up);
		vert -= IsHit(down);
		transform.Rotate(new Vector3(vert, hori, 0) * Time.deltaTime*20);
	}
	float IsHit(Ray ray)
	{
		float dist = 4;
		RaycastHit hit;
		if (Physics.Raycast(ray, out hit, dist))
		{
			Debug.DrawRay(ray.origin, ray.direction * dist, Color.red);
			return dist - hit.distance;
		}
		else
		{
			Debug.DrawRay(ray.origin, ray.direction * dist, Color.green);
			return 0;
		}
	}
}
