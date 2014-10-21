using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群

public class Goal : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		try {
			ScoreManager sm = FindObjectOfType<ScoreManager>();
			int addScore = (player.frameCount > 200) ? 500 : 500 - (player.frameCount - 200) * 2;
			sm.AddScore(addScore);
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
