//==================== インポート ====================
using UnityEngine;
using System.Collections;
using System;

//未使用変数警告を非表示に
#pragma warning disable 0168

public class ResultManager : MonoBehaviour {
	/* 変数宣言 */
	private ScoreManager ScoreManager;
	private ScoreFileOperation ScoreFileDevice;
	private ScoreManager ScoreManagerClassDevice;

	// Use this for initialization
	void Start () {
		ScoreManager = GameObject.FindObjectOfType<ScoreManager>();					//スコアマネージャゲームオブジェクト
		ScoreFileDevice = ScoreManager.GetComponent<ScoreFileOperation>();			//スコアファイル操作用デバイス
		ScoreManagerClassDevice = ScoreManager.GetComponent<ScoreManager>();		//スコア管理デバイス

		//******************** ランキング設定 ********************
		ScoreManagerClassDevice.SetRanking(ScoreFileDevice.ReadScore());
		ScoreManager.SortRanking();
		ScoreFileDevice.WriteScore(ScoreManager.GetRanking());
		
		//******************** ランキング出力 ********************
		try
		{
			// タグ作るのめんどくなってもうんねん・・・
			GUIText[] g = FindObjectsOfType<GUIText>();

			//画面上に表示されているGUIテキストの数だけループ
			for (int nLoop = 0; nLoop < 8; nLoop++)
			{
				//中身の文字によって分岐
				switch (g[nLoop].text)
				{
					//1位
					case "1st:":
						g[nLoop].text = "1st:" + ScoreManagerClassDevice.GetRankingScore(1);

						//switch文から抜ける
						break;

					//2位
					case "2nd:":
						g[nLoop].text = "2nd:" + ScoreManagerClassDevice.GetRankingScore(2);

						//switch文から抜ける
						break;

					//3位
					case "3rd:":
						g[nLoop].text = "3rd:" + ScoreManagerClassDevice.GetRankingScore(3);

						//switch文から抜ける
						break;

					//4位
					case "4th:":
						g[nLoop].text = "4th:" + ScoreManagerClassDevice.GetRankingScore(4);

						//switch文から抜ける
						break;

					//5位
					case "5th:":
						g[nLoop].text = "5th:" + ScoreManagerClassDevice.GetRankingScore(5);

						//switch文から抜ける
						break;
				}
			}
		}
		catch (IndexOutOfRangeException Index)
		{
			print("配列超えてんよ～。ただしGUIテキストの数がReleaseSceneとSceneEditSceneで違うため、一概にコーディングミスとは言えない。");
		}
		catch (ArgumentNullException GetNull)
		{
			print("not found gui text or score manager...");
		}
		catch (NullReferenceException AccessNull)
		{
			print("not found gui text or score manager...");
		}
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.anyKeyDown) {
			SceneManager sm = FindObjectOfType<SceneManager>();
			if(sm != null) {
				sm.NextScene();
			}
		}
	}
}
