using UnityEngine;
using System.Collections;

public class randomrotation : MonoBehaviour {

    Vector3 rotationAxis;

    float speed;

    Transform mineTransform;

	// Use this for initialization
	void OnEnable () 
    {
        this.speed = Random.Range(1.0f, 3.0f);
        this.mineTransform = transform;
        this.rotationAxis = Random.insideUnitSphere;
	}
	
	// Update is called once per frame
	void Update () 
    {
        this.mineTransform.Rotate(this.rotationAxis, this.speed * Time.deltaTime * Mathf.Rad2Deg);
	}
}
