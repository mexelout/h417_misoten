using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群

public class Goal : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player) {
		try {
			GameManager gm = FindObjectOfType<GameManager>();
			gm.startFinishState();
		} catch {
		}

		try {
			ScoreManager sm = FindObjectOfType<ScoreManager>();
			sm.PlusNowScore(500);
		} catch {
			print("not found score manager");
		}
		
		try {
			SceneManager sm = FindObjectOfType<SceneManager>();
			sm.NextScene();
		} catch {
			print("not found scene manager...");
		}
		print(collider.gameObject.tag);
	}
}
