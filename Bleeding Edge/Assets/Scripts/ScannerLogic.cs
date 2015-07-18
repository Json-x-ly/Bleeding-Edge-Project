using UnityEngine;
using System.Collections;

public class ScannerLogic : MonoBehaviour {
    public BezierSpline path;
	public SpotlightDetectionScript detector;
    public float t;
    public enum _state { OnRail,HardTracking,SoftTracking}
    public _state state = _state.OnRail;
    public Vector3 target;
    public float turnSpeed = 50;
    public float moveSpeed = 2;
	public float lineOfSightCoolDown = 0.0f;
	const float MAX_LOS_CoolDown = 10.0f;
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

		if (detector == null) {
			Debug.Log ("Added Spotlight Detection Script");
			detector = this.gameObject.AddComponent<SpotlightDetectionScript>();
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
				
				IsTargetVisible();

				/*if(distToPlayer>30){
					state=_state.OnRail;
					MaterialRepo.TurnBlue();
				}*/
				if (distToPlayer< 5){
					PlayerLogic.main.Die();
					Debug.Log ("I am now dead. fucker");
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
		dir.Normalize ();
		float rightDotDir = Vector3.Dot (transform.right, dir);
		float forwardDotDir = Vector3.Dot (transform.forward, dir);
		float upDotDir = -Vector3.Dot (transform.up, dir);
		float spin = Vector3.Dot (-transform.right, Vector3.up);
		Vector3 rot = new Vector3 (upDotDir, rightDotDir, spin);
		transform.Rotate (rot * Time.deltaTime * turnSpeed);
		transform.position += transform.forward * Time.deltaTime * moveSpeed * Mathf.Max (0.1f, forwardDotDir);
      
		if (dist < 1.0f) {
			return true;
		}

		if (detector.playerDetected == true) {
			Debug.Log ("Detected Player!! " + detector.playerDetected);
			MaterialRepo.TurnRed ();
			state = _state.HardTracking;
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

	private bool IsTargetVisible() {
		// Can we see the target?
		Vector3 vectToTarget = PlayerLogic.main.transform.position - this.transform.position;
		Ray rayToTarget = new Ray (transform.position, vectToTarget.normalized);
		float dist = Vector3.Distance (this.transform.position, PlayerLogic.main.transform.position);
		RaycastHit hitInfo;

		if (Physics.Raycast (rayToTarget, out hitInfo, dist) == true) {
			if (hitInfo.transform.tag == "Player") {
				lineOfSightCoolDown = MAX_LOS_CoolDown;
				Debug.Log("Player visible and actively tracking");
				return true;
			}
			else  {
				return LOSCoolDown();
			}
		} else {
			return LOSCoolDown();
		}
	}

	private bool LOSCoolDown() {
		if(DecreaseLOSCoolDown() == false) {
			Debug.Log("Lost track of player. Returning to rail.");
			state = _state.OnRail;
			MaterialRepo.TurnBlue();
			return false;
		}
		return true;
	}

	private bool DecreaseLOSCoolDown() {

		Debug.Log ("Can't see the player... Actively searching");
		lineOfSightCoolDown -= Time.deltaTime;
		
		// How long have we not been able to see the player?
		if(lineOfSightCoolDown <= 0.0f) {
			lineOfSightCoolDown = 0.0f;
			// Yes -> Go back to rails
			return false;
		}
		else {
			// No -> Continue tracking
			return true;
		}
	}
}
