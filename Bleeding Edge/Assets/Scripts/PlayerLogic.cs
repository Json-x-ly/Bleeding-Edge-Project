using UnityEngine;
using System.Collections;


public class PlayerLogic : MonoBehaviour {
	public static PlayerLogic main;
	void Awake(){
		if (main == null) {
			main = this;
		} else {
			Debug.LogError("Scene Already has a player logic");
			Destroy(gameObject);
		}
		gameObject.AddComponent<MaterialRepo> ();

	}
}
