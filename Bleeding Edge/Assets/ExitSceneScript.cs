using UnityEngine;
using System.Collections;

public class ExitSceneScript : MonoBehaviour {
	[SerializeField] string levelTarget;

	void OnTriggerEnter(Collider other){
		CowlBehaivor.main.ToChangeScene (levelTarget);
	}
}
