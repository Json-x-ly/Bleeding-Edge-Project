using UnityEngine;
using System.Collections;

public class ScannerLogic : MonoBehaviour {
    public BezierSpline path;
    public float t;
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
        t = (t + Time.deltaTime * 0.05f) % 1;
        transform.position = path.GetPoint(t);
        transform.LookAt(transform.position + path.GetDirection(t));
	}
}
