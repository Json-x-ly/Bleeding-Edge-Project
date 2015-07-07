﻿using UnityEngine;
using System.Collections;

public class ScannerLogic : MonoBehaviour {
    public BezierSpline path;
    public float t;
    public enum _state { OnRail,HardTracking,SoftTracking}
    public _state state = _state.OnRail;
    public Vector3 target;
    public float turnSpeed = 50;
    public float moveSpeed = 2;
	public float distToPlayer{
		get{return Vector3.Distance (transform.position, PlayerLogic.main.transform.position); }
	}
	// Use this for initialization
    void Awake()
    {
        if (path == null)
        {
            Debug.LogError("No path assigned to the Scanner " + name + " removing GameObject");
            Destroy(gameObject);
        }
    }
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Avoidance();
        switch(state){
            case _state.OnRail:
                //stuck to the rail
                Vector3 railPoint = path.GetPoint(t);
                if(SeekTarget(railPoint))
                    t = (t + Time.deltaTime * 0.1f) % 1;
                Debug.DrawLine(transform.position,railPoint);
        break;
            case _state.HardTracking:
                //Activly move to the player
			SeekTarget(PlayerLogic.main.transform.position);
			if(distToPlayer>30){
				state=_state.OnRail;
			}

        break;
            case _state.SoftTracking:
                //Randomly moving after player has not been seens
        break;
        }
	}
    bool SeekTarget(Vector3 target)
    {
        Vector3 dir = target - transform.position;
        float dist = dir.magnitude;
        dir.Normalize();
        float rightDotDir = Vector3.Dot(transform.right,dir);
        float forwardDotDir = Vector3.Dot(transform.forward,dir);
        float upDotDir = -Vector3.Dot(transform.up, dir);
        float spin = Vector3.Dot(-transform.right, Vector3.up);
        Vector3 rot = new Vector3(upDotDir, rightDotDir, spin);
        transform.Rotate(rot*Time.deltaTime*turnSpeed);
        transform.position+=transform.forward*Time.deltaTime*moveSpeed*Mathf.Max(0.1f,forwardDotDir);
      
        if (dist < 1f)
        {
            return true;
        }

		if (distToPlayer < 20) {
			state=_state.HardTracking;
		}
        return false;
    }
    void Avoidance()
    {
        //ugly code ahead!
        Ray right = new Ray(transform.position,     (transform.forward + transform.right));
        Ray left = new Ray(transform.position,      (transform.forward + -transform.right));
        Ray up = new Ray(transform.position,        (transform.forward + transform.up));
        Ray down = new Ray(transform.position,      (transform.forward + -transform.up));

        float hori = IsHit(left);
        hori-=IsHit(right);
        float vert=IsHit(up);
        vert-=IsHit(down);
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
