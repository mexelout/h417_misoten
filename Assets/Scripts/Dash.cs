﻿using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群

public class Dash : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		player.speed = player.speedDefault * 1.5f;
		Invoke("UndoSpeed",3);
		//		FindObjectOfType<ScoreManager>().AddScore(100);
	}

}
