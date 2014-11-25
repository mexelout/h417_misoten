using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
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
		player.Invoke("UndoSpeed", 3);
		try {
			FindObjectOfType<ScoreManager>().PlusNowScore(100);
		} catch {
		}
	}

}
