using UnityEngine;
using System.Collections;

public class ExampleToggleGameObjectState : MonoBehaviour 
{
	public void ToggleState (GameObject toggleGameObject) 
    {
        toggleGameObject.SetActive(!toggleGameObject.activeSelf);
	}
}
