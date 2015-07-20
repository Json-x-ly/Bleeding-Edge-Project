using UnityEngine;
using System.Collections;

public class SeedLogic : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        float dist = Vector3.Distance(transform.position, PlayerLogic.main.transform.position);
        if (dist < 3)
        {
            Vector3 dir = (PlayerLogic.main.transform.position - transform.position).normalized;
            transform.position += dir * Time.deltaTime*5;
        }
        if (dist < 0.2f)
        {
            Destroy(gameObject);
        }
	}
}
