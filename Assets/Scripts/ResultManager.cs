﻿using UnityEngine;
using System.Collections;

public class ResultManager : MonoBehaviour {
	// Use this for initialization
	void Start () {
		try {
			// タグ作るのめんどくなってもうんねん・・・
			GUIText[] g = FindObjectsOfType<GUIText>();
			if(g[0].text != "Result")
				g[0].text = "Score: " + FindObjectOfType<ScoreManager>().score;
			else
				g[1].text = "Score: " + FindObjectOfType<ScoreManager>().score;
		} catch {
			print("not found gui text or score manager...");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			SceneManager sm = FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
			}
		}
	}
}
