﻿using UnityEngine;
using System.Collections;

public class StumbleObject : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		player.speed = player.speedDefault * 0.3f;
		player.Invoke("UndoSpeed", 1.35f);
		try {
			player.StartStumble();
			player.Invoke ("EndStumble",1.35f);
	//		FindObjectOfType<ScoreManager>().AddScore(100);
		} catch {
		}
	}



}
