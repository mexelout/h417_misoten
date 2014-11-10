using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群

public class Ring : SpecialFloor {

	public GameObject particleAddScore;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		Instantiate(particleAddScore);	// エフェクト（particleAddScore）を呼び出す
		FindObjectOfType<ScoreManager>().AddScore(500);
		Destroy(gameObject); // gameobject delete
	}
}
