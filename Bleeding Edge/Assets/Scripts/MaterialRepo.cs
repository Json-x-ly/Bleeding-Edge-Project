using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class MaterialRepo : MonoBehaviour {
	public static IList<Material> repo = new List<Material>();
	public static IList<Material> enemyRepo = new List<Material>();
	public bool toggleColor;
	private bool currentColor;
	public static Color clr1S;
	public static Color clr2S;
	public static Color clr1E;
	public static Color clr2E;
	public static Color eClrS;
	public static Color eClrE;
	public static float t=1;
	private static bool isBlue = true;
	public static float transitionSpeed=1;

	void Awake(){
		GameObject[] mats = GameObject.FindGameObjectsWithTag ("Buildings")as GameObject[];
		foreach (GameObject GO in mats) {
			//if(GO.layer==LayerMask.GetMask

			MeshRenderer render = GO.GetComponent<MeshRenderer> ();
			if (render == null)
				continue;

			Material[] matGroup = render.materials;
			foreach (var mat in matGroup) {
				
				if (mat == null)
					continue;

				//Debug.Log (mat.name);
				if (repo.Contains (mat))
					continue;

				repo.Add (mat);
			}
		}
		GameObject[] enemyMats = GameObject.FindGameObjectsWithTag ("Enemy") as GameObject[];
		foreach (GameObject GO in enemyMats) {

			MeshRenderer render = GO.GetComponent<MeshRenderer> ();
			if (render == null)
				continue;

			Material mat = render.material;
			if (mat == null)
				continue;

			Debug.Log (mat.name);
			if(enemyRepo.Contains (mat))
				continue;

			enemyRepo.Add(mat);
		}
	}
	
	void Update(){
		if (t < 1) {
			t+=Time.deltaTime*transitionSpeed;
			t=Mathf.Min (t,1);
			Color clr1 = Color.Lerp(clr1S,clr1E,t);
			Color clr2 = Color.Lerp(clr2S,clr2E,t);
			Color eClr = Color.Lerp (eClrS, eClrE, t);
			foreach (Material mat in repo) {
				mat.SetColor("_OutlineColor",clr2);
				mat.SetColor("_EmissionColor",clr1);
			}
			foreach (Material mat in enemyRepo) {
				mat.SetColor ("_OutlineColor", eClr);
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
		if (!isBlue)
			return;
		isBlue = false;
		Debug.Log ("Turning to Red");
		clr1S = new Color (0.000f, 1.723f, 35.695f, 55.000f);
		clr1E = Color.red;
		clr2S = Color.cyan;
		clr2E = Color.red;
		eClrS = Color.blue;
		eClrE = Color.magenta;
		t = 0;
	}
	public static void TurnBlue(){
		if (isBlue)
			return;
		isBlue = true;
		Debug.Log ("Turning to Blue");
		clr1S = Color.red;
		clr1E = new Color (0.000f, 1.723f, 35.695f, 55.000f);
		clr2S = Color.red;
		clr2E = Color.cyan;
		eClrS = Color.magenta;
		eClrE = Color.blue;
		t = 0;
	}
	

}
