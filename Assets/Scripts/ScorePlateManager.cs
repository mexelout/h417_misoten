//==================== インポート ====================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CommonScore;

//未使用変数警告を非表示に
#pragma warning disable 0168

public class ScorePlateManager : MonoBehaviour
{
	//******************** 定数宣言 ********************

	//******************** メンバ変数宣言 ********************
	//++++++++++ プライベート ++++++++++
	private int nScorePlateCount = 0;										//生成されたスコア数
	private int nOrderPlateCount = 0;										//生成された順位数
	private int nNotNULLLoadCount = 0;										//読込制御用カウンター
	private List<GameObject> ScorePlates = new List<GameObject>();			//生成されたScorePlate動的格納用
	private List<GameObject> OrderPlates = new List<GameObject>();			//生成されたOrderPlate動的格納用

	//++++++++++ プロテクト ++++++++++

	//++++++++++ パブリック ++++++++++

	//====================================================================================================
	//メソッド名	：Awake
	//役割			：コンストラクタ的な
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Awake()
	{
		//******************** 初期化処理 ********************
		nScorePlateCount	= 0;		//スコアカウンターを0に
		nOrderPlateCount	= 0;		//順位カウンターを0に
		nNotNULLLoadCount	= 0;		//カウンターを0に
	}

	// Use this for initialization
	void Start ()
	{

	}
	
	// Update is called once per frame
	void Update ()
	{
		/* ※メモ：当スクリプトのStart関数で、スコアの表示を固定しようとすると、ScorePlate & OrderPlate内で実行するStopHorizontalExecutionFlagメソッドを、TextureScrollの2種Updateのどちらかが実行される前に実行してしまう模様。
		 *　　　　対策として、以下の様にUpdate内でカウンターによる1フレーム後(？)の読み込み制御を行っている
		 */
		//カウンターが1と同値になった場合に実行
		if(nNotNULLLoadCount == 1)
		{
			//スコア表示用プレートにスコア値を適応する
			ScorePlateControlNumber();

			//順位表示用プレートに順位を適応する
			OrderPlateControlNumber();
		}
		else
		{
			//int型の限界を超えない内に、10以下は加算＝10以上は加算停止
			if(nNotNULLLoadCount < 10)
			{
				//カウンターを加算する
				nNotNULLLoadCount ++;
			}
		}
	}

	//====================================================================================================
	//メソッド名	：ScorePlateControlNumber
	//役割			：スコア表示用プレートにスコア値を設定する
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void ScorePlateControlNumber()
	{
		//******************** 変数宣言 ********************
		ScoreManager ScoreManager = GameObject.FindObjectOfType<ScoreManager>();		//スコアマネージャ

		//順位数 + プレイヤーのスコアの数だけループ
		for(int nRankingLoop = 0 ; nRankingLoop < ConstantScore.RANKING_MAX + 1 ; nRankingLoop ++)
		{
			//******************** 変数宣言 ********************
			int nBuffer			= 0;			//バッファ
			int nDigit			= 10000;		//桁数分割用
			int nScoreBuffer	= 0;			//スコア代入用バッファ
			String sBuffer;						//オブジェクト名格納用

			//ループ数が順位数以下の場合
			if(nRankingLoop < ConstantScore.RANKING_MAX)
			{
				//ランキング一覧から、指定された順位のスコアを取得
				nScoreBuffer = ScoreManager.GetRankingScore(nRankingLoop + 1);
			}
			//ループ数が順位数を超えた場合
			else
			{
				//プレイヤーのスコアを取得
				nScoreBuffer = ScoreManager.GetNowScore();
			}

			//1つの順位の表示桁数だけループ
			for(int nDigitLoop = 0 ; nDigitLoop < ConstantScore.RANKING_DIGIT_MAX ; nDigitLoop ++)
			{
				nBuffer = nScoreBuffer / nDigit;	//指定桁数の値を抜き取る
				nScoreBuffer %= nDigit;				//抜き取った桁を削除
				nDigit /= 10;						//抜き取る対象の桁を1つ下げる

				//値を使用するPlate名を指定する
				sBuffer = "ScorePlate" + (nRankingLoop * ConstantScore.RANKING_DIGIT_MAX + (nDigitLoop + 1));

				//生成したPlateの数だけループ
				for(int nLoopS = 0 ; nLoopS < ScorePlates.Count ; nLoopS ++)
				{
					//配列に格納されているPlate名と、値使用先のPlate名を比較
					if(ScorePlates[nLoopS].name.Equals(sBuffer))
					{
						//バッファ数が0以下の場合、0を適応する
						if(nBuffer - 1 < 0)
						{
							//一致していたら、対象のPlate名に値を適応する
							ScorePlates[nLoopS].GetComponent<TextureScroll>().StopHorizontalExecutionFlag(9 , true);

							//Debug.Log("スコアループ：" + nRankingLoop + "　適応桁数：" + nDigitLoop + "　送る値：固定9");
						}
						//0以上なら通常適応
						else
						{
							//一致していたら、対象のPlate名に値を適応する
							ScorePlates[nLoopS].GetComponent<TextureScroll>().StopHorizontalExecutionFlag((nBuffer - 1), true);

							//Debug.Log("スコアループ：" + nRankingLoop + "　適応桁数：" + nDigitLoop + "　送る値" + (nBuffer - 1));
						}

						//Plate数ループから抜ける
						break;
					}
				}
			}
		}
	}

	//====================================================================================================
	//メソッド名	：OrderPlateControlNumber
	//役割			：順位表示用プレートに順位を設定する
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void OrderPlateControlNumber()
	{
		//順位数だけループ
		for (int nRankingLoop = 0; nRankingLoop < ConstantScore.RANKING_MAX ; nRankingLoop++)
		{
			//******************** 変数宣言 ********************
			String sBuffer;						//オブジェクト名格納用

			//順位を使用するPlate名を指定する
			sBuffer = "OrderPlate" + (nRankingLoop + 1);

			//生成したPlateの数だけループ
			for (int nLoopO = 0; nLoopO < OrderPlates.Count; nLoopO++)
			{
				//配列に格納されているPlate名と、値使用先のPlate名を比較
				if (OrderPlates[nLoopO].name.Equals(sBuffer))
				{
					//一致していたら、対象のPlate名に値を適応する
					OrderPlates[nLoopO].GetComponent<TextureScroll>().StopHorizontalExecutionFlag(nRankingLoop , true);

					//Plate数ループから抜ける
					break;
				}
			}
		}
	}

	//********************************************************************** 以降、セッター **********************************************************************
	//====================================================================================================
	//メソッド名	：SetScorePlateAddress
	//役割			：オブジェクト名からPlateオブジェクトを探索し保存する
	//引数			：(string sName)	生成されたPlateオブジェクトの名前
	//				　(int nIndex)		配列番号を指定する【-1を指定すると、先頭から順々に追加していく】
	//戻り値		：(int型)			格納された配列番号
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public int SetScorePlateAddress(string sName, int nIndex)
	{
		//配列に登録を行うため、登録数を加算
		nScorePlateCount++;

		//配列番号に-1が指定されている場合、配列番号を考えずに生成された順に配列にオブジェクトを追加していく
		if (nIndex == -1)
		{
			//配列のインデックスと実際に格納されているオブジェクト数を比較し、配列のインデックス数の方が大きい場合、既にnullが代入されている
			if (ScorePlates.Count < nScorePlateCount - 1)
			{
				//指定された配列位置に、オブジェクトを格納する
				ScorePlates.Insert(nIndex, GameObject.Find(sName));

				//配列番号を返却
				return nIndex;
			}
			//生成数の方が大きい場合、生成数に配列数がおいついていないため、Addメソッドで配列に格納する
			else
			{
				//配列にオブジェクトを格納する
				ScorePlates.Add(GameObject.Find(sName));

				//配列番号を返却
				return (ScorePlates.Count - 1);
			}
		}
		//指定された配列番号が、まだ生成されていない場合
		else if (ScorePlates.Count < nIndex)
		{
			//指定された配列番号手前になるまでループ
			for (int nLoop = ScorePlates.Count; nLoop < nIndex; nLoop++)
			{
				//nullを追加して予め配列を生成しておく
				ScorePlates.Add(null);
			}

			//直前のループで、指定された配列番号の手前まで配列を動的確保したため、引数の名前から生成されたオブジェクトを取得し、その次(指定された配列番号)に格納する
			ScorePlates.Add(GameObject.Find(sName));

			//配列番号を返却
			return (ScorePlates.Count - 1);
		}

		//配列番号を返却
		return 0;
	}

	//====================================================================================================
	//メソッド名	：SetOrderPlateAddress
	//役割			：オブジェクト名からPlateオブジェクトを探索し保存する
	//引数			：(string sName)	生成されたPlateオブジェクトの名前
	//				　(int nIndex)		配列番号を指定する【-1を指定すると、先頭から順々に追加していく】
	//戻り値		：(int型)			格納された配列番号
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public int SetOrderPlateAddress(string sName, int nIndex)
	{
		//配列に登録を行うため、登録数を加算
		nOrderPlateCount++;

		//配列番号に-1が指定されている場合、配列番号を考えずに生成された順に配列にオブジェクトを追加していく
		if (nIndex == -1)
		{
			//配列のインデックスと実際に格納されているオブジェクト数を比較し、配列のインデックス数の方が大きい場合、既にnullが代入されている
			if (OrderPlates.Count < nOrderPlateCount - 1)
			{
				//指定された配列位置に、オブジェクトを格納する
				OrderPlates.Insert(nIndex, GameObject.Find(sName));

				//配列番号を返却
				return nIndex;
			}
			//生成数の方が大きい場合、生成数に配列数がおいついていないため、Addメソッドで配列に格納する
			else
			{
				//配列にオブジェクトを格納する
				OrderPlates.Add(GameObject.Find(sName));

				//配列番号を返却
				return (OrderPlates.Count - 1);
			}
		}
		//指定された配列番号が、まだ生成されていない場合
		else if (OrderPlates.Count < nIndex)
		{
			//指定された配列番号手前になるまでループ
			for (int nLoop = OrderPlates.Count; nLoop < nIndex; nLoop++)
			{
				//nullを追加して予め配列を生成しておく
				OrderPlates.Add(null);
			}

			//直前のループで、指定された配列番号の手前まで配列を動的確保したため、引数の名前から生成されたオブジェクトを取得し、その次(指定された配列番号)に格納する
			OrderPlates.Add(GameObject.Find(sName));

			//配列番号を返却
			return (OrderPlates.Count - 1);
		}

		//配列番号を返却
		return 0;
	}

	//********************************************************************** 以降、ゲッター **********************************************************************
	//====================================================================================================
	//メソッド名	：GetScorePlateAddress
	//役割			：配列に格納されているMOBオブジェクトを返却する
	//引数			：(string sName)	MOBオブジェクトの名前
	//戻り値		：(GameObject型)	配列内の指定されたMOBオブジェクトを返却する
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public GameObject GetScorePlateAddress(string sName)
	{
		//生成された配列数だけループ
		for (int nLoop = 0; nLoop < ScorePlates.Count; nLoop++)
		{
			//ループ中に、配列内に名前が一致するオブジェクトが存在した場合
			if (ScorePlates[nLoop].name == sName)
			{
				//名前が一致したオブジェクトの中身を返す
				return ScorePlates[nLoop];
			}
		}

		//指定された配列番号のオブジェクトが見つからない
		return null;
	}

	//====================================================================================================
	//メソッド名	：GetScorePlateAddress
	//役割			：配列に格納されているMOBオブジェクトを返却する
	//引数			：(int nIndex)		配列番号指定
	//戻り値		：(GameObject型)	配列内の指定されたMOBオブジェクトを返却する
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public GameObject GetScorePlateAddress(int nIndex)
	{
		//生成された配列数よりも、指定された配列番号の値が少ない場合、既に生成されている
		if (nIndex < ScorePlates.Count)
		{
			//指定された配列の中身を返す
			return ScorePlates[nIndex];
		}

		//指定された配列番号のオブジェクトが見つからない
		return null;
	}
}
//================================================================================ EOF ================================================================================