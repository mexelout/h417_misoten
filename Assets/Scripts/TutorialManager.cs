using UnityEngine;
using System.Collections;

public class TutorialManager : MonoBehaviour {
	public GUIText[] g;
	public int i;
	// Use this for initialization
	void Start () {
		g = FindObjectsOfType<GUIText>(); 

		try {
			SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();
			SoundDevice.PlayBGM((int)CommonSound.BGM_NAME.BGM_TUTORIAL , true);
		} catch {
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonDown("Start")) {
			SceneManager sm = GameObject.FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
			} else {
				print("スキップボタンが押された");
			}
		}

		if (false) {
			try {
				if (g [0].text != "tutorial" || g [1].text != "test")
					g [0].text = "tutorial";
					g [1].text = "test";

			} catch {
					print ("not found...");
			}
		}
	}
}