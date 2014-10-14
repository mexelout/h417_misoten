using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {
	// Use this for initialization
	void Start(){
		InvokeRepeating("FlashMessage", 1, 1);
		SoundManager sm = GameObject.FindObjectOfType<SoundManager>();
		sm.PlayBGM(0);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Start")) {
			SceneManager sm = GameObject.FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
			}
		}
	}

	void FlashMessage() {
		GUIText[] gt = GameObject.FindObjectsOfType<GUIText>();
		if(gt[0].text == "Rabit boots") {
			if(gt[1].text == "") {
				gt[1].text = "Please Start Button";
			} else {
				gt[1].text = "";
			}
		} else {
			if(gt[0].text == "") {
				gt[0].text = "Please Start Button";
			} else {
				gt[0].text = "";
			}
		}
	}
}
