using UnityEngine;
using System.Collections;

public class TitleManager : MonoBehaviour {

    protected static SoundManager SoundDevice;  //サウンド再生用デバイス

	// Use this for initialization
	void Start () {
		InvokeRepeating("FlashMessage", 1, 1);

        //サウンドデバイスを探索して取得
        SoundDevice = (SoundManager)GameObject.FindObjectOfType<SoundManager>();   

        //サウンドデバイスが取得されている場合
        if(SoundDevice != null)
        {
            //BGM0番を再生
            SoundDevice.PlayBGM(0);
        }
        else 
        {
            //エラーログ出力 - 不明
            Debug.Log("デバイス未取得");
        }
    }
	
	// Update is called once per frame
	void Update () {
		if(Input.GetButton("Start")) {
			SceneManager sm = GameObject.FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
                SoundDevice.NextScene();    //次シーンへインスタンスを譲渡
			}
		}
	}

	void FlashMessage() {
		GUIText[] gt = GameObject.FindObjectsOfType<GUIText>();
		if(gt[0].text == "Rabit boots") {
			if(gt[1].text == "") {
				gt[1].text = "Please Start Button";
			} else {
				gt[1].text = "";
			}
		} else {
			if(gt[0].text == "") {
				gt[0].text = "Please Start Button";
			} else {
				gt[0].text = "";
			}
		}
	}
}
