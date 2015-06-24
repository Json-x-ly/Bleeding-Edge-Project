using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CameraPositionScript : MonoBehaviour {

	[SerializeField] private Transform m_CharacterPosition;
	[SerializeField] private float m_rotationSpeed;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.position = m_CharacterPosition.position;
		//float horizontal = CrossPlatformInputManager.GetAxis("Look_Horizontal");

		//transform.parent.transform.Rotate( new Vector3( 0, m_rotationSpeed, 0 ) );
	}

	void GetRotationFromInput() {

	}
}
