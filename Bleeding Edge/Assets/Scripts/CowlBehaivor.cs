using UnityEngine;
using System.Collections;

public class CowlBehaivor : MonoBehaviour {
	public static CowlBehaivor main;
	public Material mat;
	public Color clr1;
	public Color clr2;
	public float percent;
	public float transitionSpeed=1;
	public bool test=false;
	public bool test1=false;

	void Awake(){
		if (main == null) {
			main=this;
			mat = gameObject.GetComponent<MeshRenderer>().material;
		} else {
			Debug.LogError("Cowl script is already registerd");
			Destroy(gameObject);
		}
	}
	// Update is called once per frame
	void Update () {
		if (test) {
			test=false;
			ToBlack();
		}
		if (test1) {
			test1=false;
			ToScene();
		}
		if (percent > 1)
			return;
		
		percent += Time.deltaTime*transitionSpeed;
		mat.SetColor("_Color",Color.Lerp(clr1,clr2,percent));
	}
	public void ToBlack(){
		clr1 = Color.clear;
		clr2 = Color.black;
		percent = 0;
	}
	public void ToScene(){
		
		clr1 = Color.black;
		clr2 = Color.clear;
		percent = 0;
	}void OnTriggerEnter(Collider other) {
		Destroy(other.gameObject);
	}

}
