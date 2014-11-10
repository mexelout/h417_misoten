using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();
		SoundDevice.PlayBGM(0 , false);
	}
	
	// Update is called once per frame
	void Update () {
				if (Input.GetButton ("Start")) {
					SceneManager sm = GameObject.FindObjectOfType<SceneManager> ();
					if (sm != null) {
						sm.NextScene ();
					}
			}
	}
}