using UnityEngine;
using System.Collections;
using CommonSound;		//サウンド用定数をまとめたスクリプト

public class StumbleObject : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		try {
			player.speed = player.speedDefault * 0.3f;
			player.Invoke("UndoSpeed", 1.35f);
			player.StartStumble();
			player.Invoke ("EndStumble",1.35f);
			//******************** サウンド処理(担当：野村) ********************
			SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//ダッシュ床オブジェクトに内包されているSoundSpeakerスクリプトを取得する
			SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_FALL), false);			//ダッシュ床用SEを再生する

			//		FindObjectOfType<ScoreManager>().AddScore(100);
		} catch {
			print("stumble error");
		}

	}
}
