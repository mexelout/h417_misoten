//==================== インポート ====================
using UnityEngine;
using System.Collections;

//==================== テクスチャスクロールクラス ====================
public class TextureScroll : MonoBehaviour
{
	//******************** 定数宣言 ********************
	private static readonly float	DEFAULT_SCROLL_SPEED	= 0.25f;		//デフォルトのスクロール速度
	
	//******************** メンバ変数宣言 ********************
	//++++++++++ プライベート ++++++++++
	private bool	fDelayStopVerticalFlag		= false;						//縦方向スクロール停止までの遅延管理フラグ【true：遅延中　false：通常処理】
	private bool	fDelayStopHorizontalFlag	= false;						//横方向スクロール停止までの遅延管理フラグ【true：遅延中　false：通常処理】
	private Vector2 Offset						= new Vector2(0.0f , 0.0f);		//現在のオフセット値を保存する
	private Vector2	OldOffset					= new Vector2(0.0f , 0.0f);		//前回のオフセット値を保存する
	private Vector2 StopPosition				= new Vector2(0.0f , 0.0f);		//スクロール停止位置を保存する

	//++++++++++ プロテクト ++++++++++

	//++++++++++ パブリック ++++++++++
	public int		nVerticalAnimationNumber	= 1;						//縦方向アニメーション枚数
	public int		nHorizontalAnimationNumber	= 10;						//横方向アニメーション枚数
	public int		nVerticalScopeNumber		= 1;						//縦方向テクスチャの使用範囲
	public int		nHorizontalScopeNumber		= 10;						//横方向テクスチャの使用範囲
	public float	fScrollSpeed				= DEFAULT_SCROLL_SPEED;		//テクスチャのスクロール速度
	public bool		bScrollTypeFlag				= false;					//スクロール種類切り替えフラグ【true：固定値毎にスクロール　false：時間毎にスクロール】
	public bool		bVerticalExecutionFlag		= true;						//縦方向スクロール実行フラグ【true：スクロール実行　false：スクロールしない】
	public bool		bHorizontalExecutionFlag	= true;						//横方向スクロール実行フラグ【true：スクロール実行　false：スクロールしない】
	public bool		bVerticalScrollFlag			= false;					//縦方向スクロール制御フラグ【true：上方向にテクスチャが移動　false：下方向にテクスチャが移動】
	public bool		bHorizontalScrollFlag		= false;					//横方向スクロール制御フラグ【true：左方向にテクスチャが移動　false：右方向にテクスチャが移動】
	//public bool		
	

	//====================================================================================================
	//メソッド名	：Start
	//役割			：実行時に呼び出されるメソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Start()
	{
		//開始時にスクロール種類フラグがtrueだった場合
		if(bScrollTypeFlag)
		{
			//リピート開始(0.01秒後に1回、それ以降スクロール速度毎に呼び出し)
			InvokeRepeating("UpdateTextureScroll" , 0.01f , fScrollSpeed);
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
		//スクロールの種類が時間毎スクロールの場合
		if(!(bScrollTypeFlag))
		{
			//******************** 変数宣言 ********************
			//Vector2 Offset	= new Vector2(0.0f , 0.0f);		//テクスチャオフセット値格納用
			Vector2 Size	= new Vector2(0.0f , 0.0f);		//テクスチャサイズ値格納用

			//******************** 以下、UV座標値計算 ********************
			//縦方向スクロール実行フラグがtrueの場合、縦方向のスクロールを実行する
			if(bVerticalExecutionFlag)
			{
				//上方向にテクスチャが移動する場合
				if(bVerticalScrollFlag)
				{
					//時間によってYの値が0から-1に変化していく。-1になったら0に戻り、繰り返す。
					Offset.y = -(Mathf.Repeat(Time.time * this.fScrollSpeed , 1)) + 1.0f;

					/*
					//遅延フラグがtrue、且つ指定されたアニメーションのオフセット値を超えた場合
					if(fDelayStopVerticalFlag && Offset.y > (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y)
					{
						//y軸のオフセット値を初期位置に固定
						Offset.y = (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y;

						//フラグを初期化
						bVerticalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayStopVerticalFlag	= false;	//遅延フラグをfalseに
					}*/
					StopScrollSetPosition();
				}
				//下方向にテクスチャが移動する場合
				else
				{
					//時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
					Offset.y = Mathf.Repeat(Time.time * this.fScrollSpeed , 1);

					/*
					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が小さい場合(= 1周した場合)
					if(fDelayStopVerticalFlag && Offset.y < (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y)
					{
						//y軸のオフセット値を初期位置に固定
						Offset.y = (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y;

						//フラグを初期化
						bVerticalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayStopVerticalFlag	= false;	//遅延フラグをfalseに
					}*/
					StopScrollSetPosition();
				}
			}
			//横方向スクロール実行フラグがtrueの場合、横方向のスクロールを実行する
			if(bHorizontalExecutionFlag)
			{
				//左方向にテクスチャが移動する場合
				if(bHorizontalScrollFlag)
				{
					//時間によってYの値が0から-1に変化していく。-1になったら0に戻り、繰り返す。
					Offset.x = -(Mathf.Repeat(Time.time * this.fScrollSpeed , 1)) + 1.0f;

					/*
					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が大きい場合(= 1周した場合)
					if(fDelayStopHorizontalFlag && Offset.x > (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x)
					{
						//y軸のオフセット値を初期位置に固定
						Offset.x = (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x;

						//フラグを初期化
						bHorizontalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayStopHorizontalFlag	= false;	//遅延フラグをfalseに
					}
					*/
					StopScrollSetPosition();
				}
				//右方向にテクスチャが移動する場合
				else
				{
					//時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
					Offset.x = Mathf.Repeat(Time.time * this.fScrollSpeed , 1);

					/*
					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が小さい場合(= 1周した場合)
					if(fDelayStopHorizontalFlag && Offset.x < (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x)
					{
						//y軸のオフセット値を初期位置に固定
						Offset.x = (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x;

						//フラグを初期化
						bHorizontalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayStopHorizontalFlag	= false;	//遅延フラグをfalseに
					}*/
					StopScrollSetPosition();
				}
			}

			//サイズ値を算出
			Size.x = (float)(1.0f / nHorizontalAnimationNumber);		//x軸
			Size.y = (float)(1.0f / nVerticalAnimationNumber);			//y軸

			//マテリアルにオフセットを設定する
			this.renderer.material.SetTextureOffset("_MainTex", Offset);
			this.renderer.material.SetTextureScale("_MainTex", Size);

			//現在のオフセット値を保存する
			OldOffset.x = Offset.x;		//x軸
			OldOffset.y = Offset.y;		//y軸
		}
	}

	//====================================================================================================
	//メソッド名	：UpdateTextureScroll
	//役割			：テクスチャ更新メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void UpdateTextureScroll()
	{
		//******************** 変数宣言 ********************
		Vector2 Size = new Vector2(0, 0);		//テクスチャサイズ値格納用

		//******************** 以下、UV座標値計算 ********************
		//1を枚数分で割った値を加算
		Offset.x += (float)(1.0f / nHorizontalAnimationNumber);
		Offset.y += (float)(1.0f / nVerticalAnimationNumber);

		//座標値が1を超えた場合、0に戻す
		if(Offset.x >= 1.0f || Offset.x >= nHorizontalScopeNumber * (1.0f / nHorizontalAnimationNumber))
		{
			//初期値に戻す
			Offset.x = 0.0f;
		}
		//座標値が1を超えた場合、0に戻す
		if (Offset.y >= 1.0f || Offset.y >= nVerticalScopeNumber * (1.0f / nVerticalAnimationNumber))
		{
			//初期値に戻す
			Offset.y = 0.0f;
		}

		//スクロール停止設定をしている場合、テクスチャ座標固定
		StopScrollSetPosition();

		//サイズ値を算出
		Size.x = (float)(1.0f / nHorizontalAnimationNumber);		//x軸
		Size.y = (float)(1.0f / nVerticalAnimationNumber);			//y軸
	
		//マテリアルにオフセットを設定する
		this.renderer.material.SetTextureOffset("_MainTex" , Offset);
		this.renderer.material.SetTextureScale("_MainTex", Size);

		//現在のオフセット値を保存する
		OldOffset.x = Offset.x;		//x軸
		OldOffset.y = Offset.y;		//y軸
	}

	//====================================================================================================
	//メソッド名	：StopScrollSetPosition
	//役割			：スクロール停止後のテクスチャ座標固定メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	private void StopScrollSetPosition()
	{
		//遅延フラグがtrue、且つ指定されたアニメーションのオフセット値を超えた場合
		if (fDelayStopVerticalFlag && bVerticalScrollFlag && Offset.y >= (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y)
		{
			//y軸のオフセット値を指定アニメーションの値に固定
			Offset.y = (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y;

			//フラグを初期化
			bVerticalExecutionFlag = false;	//実行フラグをfalseに
			fDelayStopVerticalFlag = false;	//遅延フラグをfalseに

			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
		//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が小さい場合(= 1周した場合)
		if (fDelayStopVerticalFlag && !bVerticalScrollFlag && Offset.y <= (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y)
		{
			//y軸のオフセット値を指定アニメーションの値に固定
			Offset.y = (float)(1.0f / nVerticalAnimationNumber) * StopPosition.y;

			//フラグを初期化
			bVerticalExecutionFlag = false;	//実行フラグをfalseに
			fDelayStopVerticalFlag = false;	//遅延フラグをfalseに

			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
		//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が大きい場合(= 1周した場合)
		if (fDelayStopHorizontalFlag && bHorizontalScrollFlag && Offset.x <= (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x && OldOffset.x >= (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x)
		{
			//y軸のオフセット値を初期位置に固定
			Offset.x = (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x;

			//フラグを初期化
			bHorizontalExecutionFlag = false;	//実行フラグをfalseに
			fDelayStopHorizontalFlag = false;	//遅延フラグをfalseに

			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
		//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が小さい場合(= 1周した場合)
		if (fDelayStopHorizontalFlag && !bHorizontalScrollFlag && Offset.x >= (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x && OldOffset.x <= (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x)
		{
			//y軸のオフセット値を初期位置に固定
			Offset.x = (float)(1.0f / nHorizontalAnimationNumber) * StopPosition.x;

			//フラグを初期化
			bHorizontalExecutionFlag = false;	//実行フラグをfalseに
			fDelayStopHorizontalFlag = false;	//遅延フラグをfalseに

			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
	}

	//********************************************************************** 以降、セッター **********************************************************************
	//====================================================================================================
	//メソッド名	：SetScrollSpeed
	//役割			：スクロール速度設定メソッド
	//引数			：(float fSpeed)	設定するスクロールの速度
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SetScrollSpeed(float fSpeed)
	{
		//引数の速度を設定
		fScrollSpeed = fSpeed;
	}
	
	//====================================================================================================
	//メソッド名	：SetVerticalExecutionFlag
	//役割			：縦方向スクロール実行フラグ設定メソッド
	//引数			：(bool bVerticalExecution)		設定するスクロール実行フラグ【true：スクロール実行　false：スクロールしない】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SetVerticalExecutionFlag(bool bVerticalExecution)
	{
		//縦方向スクロール実行フラグを設定
		bVerticalExecutionFlag = bVerticalExecution;
	}

	//====================================================================================================
	//メソッド名	：SetHorizontalExecutionFlag
	//役割			：横方向スクロール実行フラグ設定メソッド
	//引数			：(bool bHorizontalExecution)		設定するスクロール実行フラグ【true：スクロール実行　false：スクロールしない】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SetHorizontalExecutionFlag(bool bHorizontalExecution)
	{
		//横方向スクロール実行フラグを設定
		bHorizontalExecutionFlag = bHorizontalExecution;
	}

	//====================================================================================================
	//メソッド名	：SetVerticalScrollFlag
	//役割			：縦方向スクロール制御フラグ設定メソッド
	//引数			：(bool bVerticalFlag)		設定する縦方向制御フラグ【true：上方向にテクスチャが移動　false：下方向にテクスチャが移動】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SetVerticalScrollFlag(bool bVerticalFlag)
	{
		//縦方向スクロール制御フラグを設定
		bVerticalScrollFlag = bVerticalFlag;
	}

	//====================================================================================================
	//メソッド名	：SetHorizontalScrollFlag
	//役割			：横方向スクロール制御フラグ設定メソッド
	//引数			：(bool bHorizontalFlag)	設定する横方向制御フラグ【true：左方向にテクスチャが移動　false：右方向にテクスチャが移動】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SetHorizontalScrollFlag(bool bHorizontalFlag)
	{
		//横方向スクロール制御フラグを設定
		bHorizontalScrollFlag = bHorizontalFlag;
	}

	//********************************************************************** 以降、ゲッター **********************************************************************
	//====================================================================================================
	//メソッド名	：GetVerticalExecutionFlag
	//役割			：縦方向スクロール実行フラグ取得メソッド
	//引数			：void
	//戻り値		：(bool型)		縦方向スクロール実行フラグ
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public bool GetVerticalExecutionFlag()
	{
		//縦方向スクロール実行フラグを返却
		return bVerticalExecutionFlag;
	}

	//====================================================================================================
	//メソッド名	：GetHorizontalExecutionFlag
	//役割			：横方向スクロール実行フラグ取得メソッド
	//引数			：void
	//戻り値		：(bool型)		横方向スクロール実行フラグ
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public bool GetHorizontalExecutionFlag()
	{
		//縦方向スクロール実行フラグを返却
		return bHorizontalExecutionFlag;
	}

	//====================================================================================================
	//メソッド名	：GetVerticalScrollFlag
	//役割			：縦方向スクロール制御フラグ取得メソッド
	//引数			：void
	//戻り値		：(bool型)		縦方向スクロール制御フラグ
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public bool SetVerticalScrollFlag()
	{
		//縦方向スクロール制御フラグを返却
		return bVerticalScrollFlag;
	}

	//====================================================================================================
	//メソッド名	：GetHorizontalScrollFlag
	//役割			：横方向スクロール制御フラグ取得メソッド
	//引数			：void
	//戻り値		：(bool型)		横方向スクロール制御フラグ
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public bool SetHorizontalScrollFlag()
	{
		//横方向スクロール制御フラグを返却
		return bHorizontalScrollFlag;
	}

	//********************************************************************** 以降、スクロール停止専用 **********************************************************************
	//====================================================================================================
	//メソッド名	：StopVerticalExecutionFlag
	//役割			：縦方向スクロール停止メソッド
	//引数			：(int nStopAnimationNumber)	停止位置の指定
	//				　(bool bInstantFlag)			瞬間停止フラグ【true：即時停止　false：遅延停止】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopVerticalExecutionFlag(int nStopAnimationNumber , bool bInstantFlag)
	{
		//瞬間的に停止させるかどうか
		if (!(bInstantFlag))
		{
			//遅延発生用フラグがfalseである場合に処理
			if (!(fDelayStopVerticalFlag))
			{
				//テクスチャの座標が引数で指定されたアニメーションになるまでスクロールを続ける
				fDelayStopVerticalFlag = true;
				StopPosition.y = nStopAnimationNumber;
			}
		}
		//即時停止する場合
		else
		{
			//y軸のオフセット値を初期位置に固定
			Offset.y = (float)(1.0f / nVerticalAnimationNumber) * nStopAnimationNumber;

			//マテリアルにオフセットを設定する
			this.renderer.material.SetTextureOffset("_MainTex", Offset);

			//フラグを初期化
			bHorizontalExecutionFlag = false;	//実行フラグをfalseに
			fDelayStopHorizontalFlag = false;	//遅延フラグをfalseに

			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
	}

	//====================================================================================================
	//メソッド名	：StopHorizontalExecutionFlag
	//役割			：横方向スクロール停止メソッド
	//引数			：(int nStopAnimationNumber)	停止位置の指定
	//				　(bool bInstantFlag)			瞬間停止フラグ【true：即時停止　false：遅延停止】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopHorizontalExecutionFlag(int nStopAnimationNumber , bool bInstantFlag)
	{
		//瞬間的に停止させるかどうか
		if(!(bInstantFlag))
		{
			//遅延発生用フラグがfalseである場合に処理
			if(!(fDelayStopHorizontalFlag))
			{
				//テクスチャの座標が引数で指定されたアニメーションになるまでスクロールを続ける
				fDelayStopHorizontalFlag	= true;
				StopPosition.x				= nStopAnimationNumber;
			}
		}
		//即時停止する場合
		else
		{
			//x軸のオフセット値を初期位置に固定
			Offset.x = (float)(1.0f / nHorizontalAnimationNumber) * nStopAnimationNumber;

			//マテリアルにオフセットを設定する
			this.renderer.material.SetTextureOffset("_MainTex", Offset);

			//フラグを初期化
			bHorizontalExecutionFlag = false;	//実行フラグをfalseに
			fDelayStopHorizontalFlag = false;	//遅延フラグをfalseに

			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
	}

	//********************************************************************** 以降、フラグ切り替え **********************************************************************
	//====================================================================================================
	//メソッド名	：ReverseRepeatFlag
	//役割			：スクロール種類フラグ反転メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void ReverseRepeatFlag()
	{
		//スクロール種類を切り替え(反転)
		bScrollTypeFlag = !(bScrollTypeFlag);

		//反転に伴い、リピートのONOFFを行う
		//反転後のフラグがtrueである場合
		if (bScrollTypeFlag)
		{
			//リピート開始(0.01秒後に1回、それ以降秒毎に呼び出し)
			InvokeRepeating("UpdateTextureScroll" , 0.01f , fScrollSpeed);
		}
		//反転後のフラグがfalseである場合
		else
		{
			//リピート中止
			CancelInvoke("UpdateTextureScroll");
		}
	}
	
	//====================================================================================================
	//メソッド名	：ReverseVerticalExecutionFlag
	//役割			：縦方向スクロール実行フラグ反転メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void ReverseVerticalExecutionFlag()
	{
		//縦方向スクロール実行フラグを反転
		bVerticalExecutionFlag = !(bVerticalExecutionFlag);
	}

	//====================================================================================================
	//メソッド名	：ReverseHorizontalExecutionFlag
	//役割			：横方向スクロール実行フラグ反転メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void ReverseHorizontalExecutionFlag()
	{
		//横方向スクロール実行フラグを反転
		bHorizontalExecutionFlag = !(bHorizontalExecutionFlag);
	}

	//====================================================================================================
	//メソッド名	：ReverseVerticalScrollFlag
	//役割			：縦方向スクロール制御フラグ反転メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void ReverseVerticalScrollFlag()
	{
		//縦方向スクロール実行フラグを反転
		bVerticalScrollFlag = !(bVerticalScrollFlag);
	}

	//====================================================================================================
	//メソッド名	：ReverseHorizontalScrollFlag
	//役割			：横方向スクロール制御フラグ反転メソッド
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void ReverseHorizontalScrollFlag()
	{
		//横方向スクロール実行フラグを反転
		bHorizontalScrollFlag = !(bHorizontalScrollFlag);
	}
}
//================================================================================ EOF ================================================================================