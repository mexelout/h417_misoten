﻿using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {

	public int stageNumber = 1;
	private int startCount = 5;
	private GUIText scoreBoard;
	private ScoreManager sm;
	SoundSpeaker[] SoundSpeaker = new SoundSpeaker[50];

	// Use this for initialization
	void Start () {
		InvokeRepeating("StartCounting", 1, 1);
		scoreBoard = gameObject.GetComponentsInChildren<GUIText>()[1];
		sm = GameObject.FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
		try {
			sm.SetNowScore(0);
		} catch {
			print("not found score manager");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(sm != null) {
			scoreBoard.text = "Score: " + sm.GetNowScore();
		}
	}

	private void StartCounting() {
		startCount--;
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = startCount.ToString();
		if(startCount <= 0) {
			tm.text = "Go !!";
			CancelInvoke("StartCounting");
			Invoke("StartStringHide", 2);
		}
	}

	private void StartStringHide() {
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = "";
	}

	public int GetStartCount() {
		return startCount;
	}
}
