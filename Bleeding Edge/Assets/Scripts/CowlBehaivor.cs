using UnityEngine;
using System.Collections;

public class CowlBehaivor : MonoBehaviour {
	public static CowlBehaivor main;
	public Material mat;
	public Color clr1;
	public Color clr2;
	public float percent;
	public float transitionSpeed=1;
	private bool isChangingScene=false;
	private string targetScene=null;

	void Awake(){
		if (main == null) {
			main=this;
			mat = gameObject.GetComponent<MeshRenderer>().material;
		} else {
			Debug.LogError("Cowl script is already registerd");
			Destroy(gameObject);
		}
	}

	void Start() {
		ToScene ();
	}

	// Update is called once per frame
	void Update () {
		if (percent > 1) {
			if(isChangingScene)
				Application.LoadLevel(targetScene);
			return;
		}
		
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
	}
	public void toDeath(){
		clr1 = Color.red;
		clr2 = Color.clear;
		percent = 0;
	}
	public void ToChangeScene(string sceneName){
		clr1 = Color.clear;
		clr2 = Color.black;
		percent = 0;
		isChangingScene = true;
		targetScene = sceneName;
	}
}
