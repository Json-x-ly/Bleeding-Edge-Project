using UnityEngine;
using System.Collections;

public class DroneChaseLogic : MonoBehaviour {

	public BezierSpline path;
	public float lineOfSightCoolDown = 0.0f;
	const float MAX_LOS_CoolDown = 10.0f;
	public ScannerLogic scanLogic = null;

	void Awake() {
		if (path == null)
		{
			Debug.LogError("No path assigned to the Scanner " + name + " removing GameObject");
			Destroy(gameObject);
		}

		if (scanLogic == null) {
			scanLogic = this.gameObject.GetComponent<ScannerLogic>();
			if(scanLogic == null) {
				scanLogic = this.gameObject.AddComponent<ScannerLogic>();
				scanLogic.SetPath(this.path);
			}
		}
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (scanLogic.state == ScannerLogic._state.HardTracking) {
			scanLogic.enabled = false;
			scanLogic.SeekTarget(PlayerLogic.main.transform.position);

			if (scanLogic.distToPlayer< 5){
				PlayerLogic.main.Die();
				Debug.Log ("I am now dead. fucker");
			}

			if(IsTargetVisible() == false) {
				scanLogic.enabled = true;
				MaterialRepo.TurnBlue();
				scanLogic.state = ScannerLogic._state.OnRail;
			}
		}
		else if (scanLogic.detector.playerDetected == true) {
			MaterialRepo.TurnRed ();
			scanLogic.state = ScannerLogic._state.HardTracking;
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
