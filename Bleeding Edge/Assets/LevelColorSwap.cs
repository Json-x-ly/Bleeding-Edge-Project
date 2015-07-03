using UnityEngine;
using System.Collections;

public class LevelColorSwap : MonoBehaviour {
	public Renderer render;
	public bool flip=true; 

	// Use this for initialization
	void Start () {
		render = gameObject.GetComponent<Renderer>();
		if (render == null){
			Debug.Log ("This object has no Mesh Renderer");
			Destroy (gameObject);
		}
		//render.material.SetColor ("_Outline Color", Color.red);
	}

	// Update is called once per frame
	void Update() {
		DynamicGI.SetEmissive (GetComponent<Renderer> (), Color.red);
		//if (flip) {
		//	ToCyan();
		//} else {
		//	ToRed();
		//}
	}	

	public void ToRed(){
		//render.material.SetColor ("_Emission", Color.red);
		//render.material.SetColor ("_Outline Color", Color.red);
	}
		
	public void ToCyan() {
		//render.material.SetColor ("_Emission", Color.cyan);
		//render.material.SetColor ("_Outline Color", Color.cyan);
	}
}
