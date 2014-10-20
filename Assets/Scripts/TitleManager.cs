﻿using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {
	// Use this for initialization
	void Start(){
		InvokeRepeating("FlashMessage", 1, 1);
		
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();
		SoundDevice.PlayBGM(0 , false);
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Start")) {
			SceneManager sm = GameObject.FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
			}
		}

		//============================== 以下、サウンドデバイスのテスト用 ==============================
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();

		//1キーで敵艦隊見ゆ
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			//通常再生
			SoundDevice.PlayBGM(0, false);
		}
		//2キーで鉄底海峡
		if(Input.GetKeyDown(KeyCode.Alpha2))
		{
			//通常再生
			SoundDevice.PlayBGM(1, false);
		}
		//3キーで敵艦隊見ゆ(フェードイン)
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			//フェードイン再生
			SoundDevice.PlayBGM(0, true);
		}
		//4キーで鉄底海峡(フェードイン)
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			//フェードイン再生
			SoundDevice.PlayBGM(1, true);
		}
		//5キーでフェードアウト
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			//再生中BGMのフェードアウト
			SoundDevice.StopBGM(true);
		}
		//============================== サウンドデバイステスト用ここまで ==============================
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