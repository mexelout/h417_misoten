﻿//==================== インポート ====================
using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using CommonSound;

//未使用変数警告を非表示に
#pragma warning disable 0168

//==================== モブクラス ====================
public class MOBManager : MonoBehaviour
{
	//******************** 定数宣言 ********************
	
	//******************** メンバ変数宣言 ********************
	//++++++++++ プライベート ++++++++++
	private int					nMobCount = 0;							//生成されたMOB数
	private List<GameObject>	Mobs = new List<GameObject>();			//生成されたMOB動的格納用

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
		nMobCount = 0;		//カウンターを0に
	}

	//====================================================================================================
	//メソッド名	：Start
	//役割			：実行時に呼び出されるメソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Start()
	{
		//確保した配列数だけループ
		for (int nLoop = 0; nLoop < Mobs.Count; nLoop++)
		{
			//デバイスが登録されていない場合を考慮してtry文使用
			try
			{
				//オブジェクトに設定されているサウンドスピーカーを取得して、歓声1を再生する
				Mobs[nLoop].GetComponent<SoundSpeaker>().PlaySE((int)(SE_NAME.SE_CHEER1) , true);
			}
			catch (Exception e)
			{
				Debug.Log("オブジェクトが存在しない。");
			}
		}
	}

	//====================================================================================================
	//メソッド名	：Update
	//役割			：更新メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Update()
	{
		//サウンドテスト
		//SoundTest();
	}

	//********************************************************************** 以降、セッター **********************************************************************
	//====================================================================================================
	//メソッド名	：SetMobAddress
	//役割			：オブジェクト名からMobオブジェクトを探索し保存する
	//引数			：(string sName)	生成されたMOBオブジェクトの名前
	//				　(int nIndex)		配列番号を指定する【-1を指定すると、先頭から順々に追加していく】
	//戻り値		：(int型)			格納された配列番号
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public int SetMobAddress(string sName , int nIndex)
	{
		//配列に登録を行うため、登録数を加算
		nMobCount ++;

		//配列番号に-1が指定されている場合、配列番号を考えずに生成された順に配列にオブジェクトを追加していく
		if(nIndex == -1)
		{
			//配列のインデックスと実際に格納されているオブジェクト数を比較し、配列のインデックス数の方が大きい場合、既にnullが代入されている
			if(Mobs.Count < nMobCount - 1)
			{
				//指定された配列位置に、オブジェクトを格納する
				Mobs.Insert(nIndex , GameObject.Find(sName));

				//配列番号を返却
				return nIndex;
			}
			//生成数の方が大きい場合、生成数に配列数がおいついていないため、Addメソッドで配列に格納する
			else
			{
				//配列にオブジェクトを格納する
				Mobs.Add(GameObject.Find(sName));

				//配列番号を返却
				return (Mobs.Count - 1);
			}
		}
		//指定された配列番号が、まだ生成されていない場合
		else if(Mobs.Count < nIndex)
		{
			//指定された配列番号手前になるまでループ
			for(int nLoop = Mobs.Count ; nLoop < nIndex ; nLoop ++)
			{
				//nullを追加して予め配列を生成しておく
				Mobs.Add(null);
			}

			//直前のループで、指定された配列番号の手前まで配列を動的確保したため、引数の名前から生成されたオブジェクトを取得し、その次(指定された配列番号)に格納する
			Mobs.Add(GameObject.Find(sName));

			//配列番号を返却
			return (Mobs.Count - 1);
		}
		
		//配列番号を返却
		return 0;
	}

	//********************************************************************** 以降、ゲッター **********************************************************************
	//====================================================================================================
	//メソッド名	：GetMobAddress
	//役割			：配列に格納されているMOBオブジェクトを返却する
	//引数			：(string sName)	MOBオブジェクトの名前
	//戻り値		：(GameObject型)	配列内の指定されたMOBオブジェクトを返却する
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public GameObject GetMobAddress(string sName)
	{
		//生成された配列数だけループ
		for(int nLoop = 0 ; nLoop < Mobs.Count ; nLoop ++)
		{
			//ループ中に、配列内に名前が一致するオブジェクトが存在した場合
			if(Mobs[nLoop].name == sName)
			{
				//名前が一致したオブジェクトの中身を返す
				return Mobs[nLoop];
			}
		}

		//指定された配列番号のオブジェクトが見つからない
		return null;
	}

	//====================================================================================================
	//メソッド名	：GetMobAddress
	//役割			：配列に格納されているMOBオブジェクトを返却する
	//引数			：(int nIndex)		配列番号指定
	//戻り値		：(GameObject型)	配列内の指定されたMOBオブジェクトを返却する
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public GameObject GetMobAddress(int nIndex)
	{
		//生成された配列数よりも、指定された配列番号の値が少ない場合、既に生成されている
		if(nIndex < Mobs.Count)
		{
			//指定された配列の中身を返す
			return Mobs[nIndex];
		}

		//指定された配列番号のオブジェクトが見つからない
		return null;
	}

	//********************************************************************** 以降、処理テスト用 **********************************************************************
	//====================================================================================================
	//メソッド名	：SoundTest
	//役割			：サウンド系処理の確認用
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SoundTest()
	{
		//1キーで全デバイス再生
		if(Input.GetKeyDown(KeyCode.Alpha1))
		{
			//確保した配列数だけループ
			for(int nLoop = 0 ; nLoop < Mobs.Count ; nLoop ++)
			{
				//デバイスが登録されていない場合を考慮してtry文使用
				try
				{
					//オブジェクトに設定されているサウンドスピーカーを取得して、歓声1を再生する
					Mobs[nLoop].GetComponent<SoundSpeaker>().PlaySE((int)(SE_NAME.SE_CHEER1) , true);
				}
				catch (Exception e)
				{
					Debug.Log("オブジェクトが存在しない。");
				}
			}
		}
	}
}
//================================================================================ EOF ================================================================================
