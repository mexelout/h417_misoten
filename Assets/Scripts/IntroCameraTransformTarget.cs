using UnityEngine;
using System.Collections;

public class IntroCameraTransformTarget : MonoBehaviour {

	private IntroCamera introCamera;
	private GameObject target;

	// Use this for initialization
	void Start () {
		introCamera = GetComponent<IntroCamera>();
		target = GameObject.Find("Main Camera");
		exec();
	}
	
	// Update is called once per frame
	void Update () {
		exec();
	}

	void exec() {
		try {
			introCamera.ForceSetEndPosition(target.transform.position);
			introCamera.ForceSetEndRotate(target.transform.rotation.eulerAngles);
		} catch {
			print("error! not found intro camera from intro camera transfrom target");
		}
	}
}
