//==================== インポート ====================
using UnityEngine;
using System.Collections;
using System.IO;
using System;
using System.Text;
using CommonScore;

//==================== スコアファイル操作クラス ====================
public class ScoreFileOperation : MonoBehaviour
{
	//******************** 定数宣言 ********************

	//******************** メンバ変数宣言 ********************
	//++++++++++ プライベート ++++++++++

	//++++++++++ プロテクト ++++++++++

	//++++++++++ パブリック ++++++++++

	//====================================================================================================
	//メソッド名	：Awake
	//役割			：実行時に呼び出されるメソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Awake ()
	{

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

	}

	//====================================================================================================
	//メソッド名	：Update
	//役割			：更新メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Update ()
	{
	
	}

	//====================================================================================================
	//メソッド名	：ReadScore
	//役割			：今までのランキングを外部ファイルから取得する
	//引数			：void
	//戻り値		：(int[]型)		取得したランキングスコアの値
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public int[] ReadScore()
	{
		//******************** 変数宣言 ********************
		int			nBuffer			= 0;			//int型バッファ
		int[]		nRankingScore	= null;			//ランキング格納用
		string		sBuffer			= null;			//String型バッファ
		string[]	sArrayBuffer	= null;			//String型配列バッファ
		FileInfo	FileDevice		= new FileInfo(Application.dataPath + ConstantScore.RANKING_DATAPASS);		//ファイル入出力用デバイス
		
		//例外処理
		try
		{
			//UTF-8で読み込む
			using(StreamReader StreamReadDevice = new StreamReader(FileDevice.OpenRead() , Encoding.UTF8))
			{
				//バッファ内にファイル内の全てのデータを読み込む
				sBuffer = StreamReadDevice.ReadToEnd();
			}
		}
		catch (Exception e)
		{
			//エラー出力
			Debug.Log("何かエラー起きてるらしい。");
		}

		sArrayBuffer	= sBuffer.Split('\n');				//改行コードを基準に、数値を分割する
		nRankingScore	= new int[sArrayBuffer.Length];		//分割した数だけ、ランキング格納用int型配列を生成する

		//配列(ランキング)の数だけループ
		for(int nLoop = 0 ; nLoop < sArrayBuffer.Length ; nLoop ++)
		{
			//デバッグ用出力
			Debug.Log(sArrayBuffer[nLoop]);
			
			//配列に格納されているスコアの文字列を、数値に変換して格納する
			nRankingScore[nLoop] = int.Parse(sArrayBuffer[nLoop]);
		}

		//バックアップ用保存
		//******************** 変数宣言 ********************
		/*int			nBuffer			= 0;			//int型バッファ
		int[]		nRankingScore	= null;			//ランキング格納用
		string		sBuffer			= null;			//String型バッファ
		string[]	sArrayBuffer	= null;			//String型配列バッファ
		FileStream FileReadDevice = new FileStream(Application.dataPath + ConstantScore.RANKING_DATAPASS , FileMode.Open , FileAccess.Read);		//
		StreamReader StreamReaderDevice = new StreamReader(FileReadDevice, Encoding.GetEncoding("UTF-8"));								//

		Debug.Log("0.ここまできた");

		//******************** 初期化 ********************
		sBuffer				= StreamReaderDevice.ReadLine();		//ランキングに書き込まれている順位数を読み込む
		nBuffer				= int.Parse(sBuffer);					//読み込んだ順位数を整数型に変換する
		nRankingScore		= new int[nBuffer + 1];					//順位の数だけランキング格納用配列を生成
		nRankingScore[0]	= nBuffer;

		Debug.Log("1.ここまできた");

		//******************** 読込処理 ********************
		//読み込んだ順位の数だけループ
		for(int nLoop = 1 ; nLoop < nBuffer + 1 ; nLoop++)
		{
			try
			{
				//int型に変換したランキングの中身を格納する
				sBuffer = StreamReaderDevice.ReadLine();
				nRankingScore[nLoop] = int.Parse(sBuffer);
			}
			catch (Exception e)
			{
				//エラー出力
				Debug.Log("読込失敗");
			}
		}

		//開いたファイルを閉じる		
		StreamReaderDevice.Close();
		FileReadDevice.Close();
		*/

		//取得したランキングを返却する
		return nRankingScore;
	}

	//====================================================================================================
	//メソッド名	：WriteScore
	//役割			：現在のランキングを外部ファイルに書き込みする
	//引数			：(int[] nNowRanking)		現在のランキング
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void WriteScore(int[] nNowRanking)
	{
		//******************** 変数宣言 ********************
		FileStream		FileWriteDevice		= new FileStream(Application.dataPath + ConstantScore.RANKING_DATAPASS , FileMode.Create , FileAccess.Write);		//
		StreamWriter	StreamWriterDevice	= new StreamWriter(FileWriteDevice , Encoding.GetEncoding("UTF-8"));												//

		//******************** 書き出し処理 ********************
		//例外処理
		try
		{
			//ランキングを保存する数だけループ
			for (int nLoop = 0; nLoop < nNowRanking.Length; nLoop++)
			{
				//最後の保存のみ、改行を入れずに保存
				if (nLoop == nNowRanking.Length - 1)
				{
					//指定されているテキストにデータを書き込む
					StreamWriterDevice.Write(nNowRanking[nLoop].ToString());
				}
				else
				{
					//指定されているテキストにデータを書き込む
					StreamWriterDevice.WriteLine(nNowRanking[nLoop].ToString());
				}
			}

			//開いたファイルを閉じる
			StreamWriterDevice.Close();
			FileWriteDevice.Close();
		}
		catch (ObjectDisposedException e)
		{
			//エラー出力
			Debug.Log("解放してないっぽい");
		}
		catch (Exception e)
		{
			//エラー出力
			Debug.Log("何かエラー起きてるらしい。");
		}
		finally
		{
			//開いたファイルは必ず閉じる
			StreamWriterDevice.Close();
			FileWriteDevice.Close();
		}

		Debug.Log("7.ここまできた");
	}
}
//================================================================================ EOF ================================================================================
