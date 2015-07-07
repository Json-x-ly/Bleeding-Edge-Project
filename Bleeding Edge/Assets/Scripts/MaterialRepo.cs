using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MaterialRepo : MonoBehaviour {
	public static IList<Material> repo = new List<Material>();
	public bool toggleColor;
	private bool currentColor;
	public static Color clr1S;
	public static Color clr2S;
	public static Color clr1E;
	public static Color clr2E;
	public static float t=1;
	public static float transitionSpeed=1;

	void Awake(){
		GameObject[] mats = GameObject.FindObjectsOfType (typeof(GameObject))as GameObject[];
		foreach (GameObject GO in mats) {
			MeshRenderer render = GO.GetComponent<MeshRenderer> ();
			if (render == null)
				continue;

			Material mat = render.material;
			if (mat == null)
				continue;

			Debug.Log (mat.name);
			if (repo.Contains (mat))
				continue;

			repo.Add (mat);
		}
	}
	void Update(){
		if (t < 1) {
			t+=Time.deltaTime*transitionSpeed;
			t=Mathf.Min (t,1);
			Color clr1 = Color.Lerp(clr1S,clr1E,t);
			Color clr2 = Color.Lerp(clr2S,clr2E,t);
			foreach (Material mat in repo) {
				mat.SetColor("_OutlineColor",clr2);
				mat.SetColor("_EmissionColor",clr1);
			}
		}
		if (toggleColor != currentColor) {
			currentColor=toggleColor;
			if(toggleColor){
				TurnRed();
			}else{
				TurnBlue();
			}
		}
	}
	public static void TurnRed(){
		clr1S = new Color (0.000f, 1.723f, 35.695f, 55.000f);
		clr1E = Color.red;
		clr2S = Color.cyan;
		clr2E = Color.red;
		t = 0;
	}
	public static void TurnBlue(){
		clr1S = Color.red;
		clr1E = new Color (0.000f, 1.723f, 35.695f, 55.000f);
		clr2S = Color.red;
		clr2E = Color.cyan;
		t = 0;
	}
	

}
