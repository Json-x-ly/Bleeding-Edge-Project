using UnityEngine;
using System.Collections;

public class RemoveWhenNear : MonoBehaviour {
    SphereCollider col;
    void Awake()
    {
        col = GetComponent<SphereCollider>();
        if (col == null)
        {
            Debug.LogError("No Sphere collider on "+gameObject.name);
            Destroy(gameObject);
        }
    }
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Vector3.Distance(transform.position, PlayerLogic.main.transform.position) < col.radius)
        {
            Destroy(gameObject);
        }
	}
}
