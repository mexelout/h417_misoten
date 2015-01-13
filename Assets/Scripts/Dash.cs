using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群
using CommonSound;		//サウンド用定数をまとめたスクリプト

public class Dash : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		player.speed = player.speed * 1.5f;
		player.CancelInvoke("UndoSpeed");
		player.Invoke("UndoSpeed", 3);
		if(player.speed > 50) {
			player.speed = 50;
		}
		try {
			FindObjectOfType<ScoreManager>().PlusNowScore(100);
		} catch {
		}

		//******************** サウンド処理(担当：野村) ********************
		SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//ダッシュ床オブジェクトに内包されているSoundSpeakerスクリプトを取得する
		SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_DASH) , false);			//ダッシュ床用SEを再生する
	}
}
