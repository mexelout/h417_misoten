using UnityEngine;
using System.Collections;

public class TitleCamera : MonoBehaviour {


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		//transform.Rotate(0, 20.0f * Time.deltaTime, 0);
		Vector3 rot = transform.localRotation.eulerAngles; 
		rot.y += 20f * Time.deltaTime;
		transform.rotation = Quaternion.Euler(rot);
	}
}
