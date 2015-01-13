using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群

public class Dash : SpecialFloor {

	public float	MAX_SPEED = 50.0f;
	public float	ADD_SPEED = 0.0f;
	public float	MULTIPLY_SPEED	= 1.5f;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		player.speed = player.speed * MULTIPLY_SPEED;
		player.speed = player.speed + ADD_SPEED;

		player.CancelInvoke("UndoSpeed");
		player.Invoke("UndoSpeed", 3);
		if(player.speed > MAX_SPEED) {
			player.speed = MAX_SPEED;
		}
		try {
			FindObjectOfType<ScoreManager>().PlusNowScore(100);
		} catch {
		}
	}

}
