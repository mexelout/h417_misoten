using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群
using CommonSound;		//サウンド用定数をまとめたスクリプト

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
		//******************** サウンド処理(担当：野村) ********************
		SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//リングオブジェクトに内包されているSoundSpeakerスクリプトを取得する
		SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_RING), false);			//リング用SEを再生する

		FindObjectOfType<ScoreManager>().PlusNowScore(500);
		this.renderer.enabled = false;

		if(particleAddScore) Instantiate(particleAddScore);	// エフェクト（particleAddScore）を呼び出す
	}
}
