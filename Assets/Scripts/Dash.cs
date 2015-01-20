using UnityEngine;	// UnityEngineはUnityの機能を利用する上で必要なライブラリ
using System.Collections;//System.CollectionsはC#を利用する上で便利なライブラリ群
using CommonSound;		//サウンド用定数をまとめたスクリプト

public class Dash : SpecialFloor {

	public float	MAX_SPEED = 60.0f;
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
		try {
			player.speed = player.speed * MULTIPLY_SPEED;
			player.speed = player.speed + ADD_SPEED;

			player.CancelInvoke("UndoSpeed");
			player.Invoke("UndoSpeed", 3);
			if(player.speed > MAX_SPEED) {
				player.speed = MAX_SPEED;
			}
			FindObjectOfType<ScoreManager>().PlusNowScore(100);
			//******************** サウンド処理(担当：野村) ********************
			SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//ダッシュ床オブジェクトに内包されているSoundSpeakerスクリプトを取得する
			SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_DASH) , false);			//ダッシュ床用SEを再生する
		} catch {
			print("dash error");
		}
	}
}
