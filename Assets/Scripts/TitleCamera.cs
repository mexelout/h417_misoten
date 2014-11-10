using UnityEngine;
using System.Collections;

public class TitleCamera : MonoBehaviour {

	private float TitleCameraRotX = 17.05f;
	private float TitleCameraRotY = 0.0f;
	private float TitleCameraRotZ = 0.0f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(0, 0.1f, 0);
	}
}
