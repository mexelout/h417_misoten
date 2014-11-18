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
			SoundDevice.PlayBGM(0 , false);
		} catch {
		}
	}
	
	// Update is called once per frame
	void Update () {
		i++;
		 /*if (Input.GetButton ("Start")) {
				SceneManager sm = GameObject.FindObjectOfType<SceneManager> ();
				if (sm != null) {
						sm.NextScene ();
				}
		}*/
	
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