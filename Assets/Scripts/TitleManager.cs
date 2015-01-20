using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

	// Use this for initialization
	void Start(){
		InvokeRepeating("FlashMessage", 1, 1);
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();
		SoundDevice.PlayBGM((int)CommonSound.BGM_NAME.BGM_TITLE, true);
	}
	
	// Update is called once per frame
	void Update () {
		// 一応両方取れるようにした
		if(Input.GetButtonDown("Start") || Input.GetButtonDown("Jump")) {
			SceneManager sm = GameObject.FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
			} else {
				print("push startされた");
			}
		}

		//サウンドテスト
		//TitleSceneSoundTest();
	}

	void FlashMessage() {
	}

	public void TitleSceneSoundTest()
	{
		//============================== 以下、サウンドデバイスのテスト用 ==============================
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();

		//1キーで敵艦隊見ゆ
		if (Input.GetKeyDown(KeyCode.Alpha1))
		{
			//通常再生
			SoundDevice.PlayBGM(0, false);
		}
		//2キーで鉄底海峡
		if (Input.GetKeyDown(KeyCode.Alpha2))
		{
			//通常再生
			SoundDevice.PlayBGM(1, false);
		}
		//3キーで敵艦隊見ゆ(フェードイン)
		if (Input.GetKeyDown(KeyCode.Alpha3))
		{
			//フェードイン再生
			SoundDevice.PlayBGM(0, true);
		}
		//4キーで鉄底海峡(フェードイン)
		if (Input.GetKeyDown(KeyCode.Alpha4))
		{
			//フェードイン再生
			SoundDevice.PlayBGM(1, true);
		}
		//5キーでフェードアウト
		if (Input.GetKeyDown(KeyCode.Alpha5))
		{
			//再生中BGMのフェードアウト
			SoundDevice.StopBGM(true);
		}
		//============================== サウンドデバイステスト用ここまで ==============================
	}
}