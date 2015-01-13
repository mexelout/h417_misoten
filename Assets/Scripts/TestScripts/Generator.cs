using UnityEngine;
using System.Collections;

public class Generator : MonoBehaviour {

	public GameObject generateObject;
	public float span = 10;

	// Use this for initialization
	void Start () {
		if(generateObject) {
			Generate();
			InvokeRepeating("Generate", span, span);
		}
	}
	
	// Update is called once per frame
	void Update () {
	}


	void Generate() {
		Instantiate(generateObject);
	}
}
