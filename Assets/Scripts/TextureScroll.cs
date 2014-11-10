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
	private bool	fDelayVerticalFlag		= false;					//縦方向スクロール停止までの遅延管理フラグ【true：遅延中　false：通常処理】
	private bool	fDelayHorizontalFlag	= false;					//横方向スクロール停止までの遅延管理フラグ【true：遅延中　false：通常処理】
	private Vector2 Offset					= new Vector2(0, 0);		//	
	private Vector2	OldOffset				= new Vector2(0.0f, 0.0f);	//前回のオフセット値を保存する

	//++++++++++ プロテクト ++++++++++

	//++++++++++ パブリック ++++++++++
	public int		nVerticalAnimationNumber	= 1;						//縦方向アニメーション枚数
	public int		nHorizontalAnimationNumber	= 10;						//横方向アニメーション枚数
	public float	fScrollSpeed				= DEFAULT_SCROLL_SPEED;		//テクスチャのスクロール速度
	public bool		bScrollTypeFlag				= false;					//スクロール種類切り替えフラグ【true：固定値毎にスクロール　false：時間毎にスクロール】
	public bool		bVerticalExecutionFlag		= true;						//縦方向スクロール実行フラグ【true：スクロール実行　false：スクロールしない】
	public bool		bHorizontalExecutionFlag	= true;						//横方向スクロール実行フラグ【true：スクロール実行　false：スクロールしない】
	public bool		bVerticalScrollFlag			= false;					//縦方向スクロール制御フラグ【true：上方向にテクスチャが移動　false：下方向にテクスチャが移動】
	public bool		bHorizontalScrollFlag		= false;					//横方向スクロール制御フラグ【true：左方向にテクスチャが移動　false：右方向にテクスチャが移動】
	

	//====================================================================================================
	//メソッド名	：Start
	//役割			：実行時に呼び出されるメソッド
	//引数			：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Start()
	{
		
	}

	//====================================================================================================
	//メソッド名	：Update
	//役割			：更新メソッド
	//引数			：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Update()
	{
		//スクロールの種類が時間毎スクロールの場合
		if(!(bScrollTypeFlag))
		{
			//******************** 変数宣言 ********************
			Vector2 OffsetBuffer	= new Vector2(0, 0);		//テクスチャオフセット値格納用
			Vector2 Size			= new Vector2(0, 0);		//テクスチャサイズ値格納用

			//******************** 以下、UV座標値計算 ********************
			//縦方向スクロール実行フラグがtrueの場合、縦方向のスクロールを実行する
			if (bVerticalExecutionFlag)
			{
				//上方向にテクスチャが移動する場合
				if (bVerticalScrollFlag)
				{
					//時間によってYの値が0から-1に変化していく。-1になったら0に戻り、繰り返す。
					OffsetBuffer.y = -(Mathf.Repeat(Time.time * this.fScrollSpeed , 1)) + 1.0f;

					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が大きい場合(= 1周した場合)
					if (fDelayVerticalFlag && OffsetBuffer.y > OldOffset.y)
					{
						//y軸のオフセット値を初期位置に固定
						OffsetBuffer.y = 0.0f;

						//フラグを初期化
						bVerticalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayVerticalFlag		= false;	//遅延フラグをfalseに
					}
				}
				//下方向にテクスチャが移動する場合
				else
				{
					//時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
					OffsetBuffer.y = Mathf.Repeat(Time.time * this.fScrollSpeed , 1);

					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が小さい場合(= 1周した場合)
					if (fDelayVerticalFlag && OffsetBuffer.y < OldOffset.y)
					{
						//y軸のオフセット値を初期位置に固定
						OffsetBuffer.y = 0.0f;

						//フラグを初期化
						bVerticalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayVerticalFlag		= false;	//遅延フラグをfalseに
					}
				}
			}
			//横方向スクロール実行フラグがtrueの場合、横方向のスクロールを実行する
			if (bHorizontalExecutionFlag)
			{
				//左方向にテクスチャが移動する場合
				if (bHorizontalScrollFlag)
				{
					//時間によってYの値が0から-1に変化していく。-1になったら0に戻り、繰り返す。
					OffsetBuffer.x = -(Mathf.Repeat(Time.time * this.fScrollSpeed , 1)) + 1.0f;

					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が大きい場合(= 1周した場合)
					if (fDelayHorizontalFlag && OffsetBuffer.x > OldOffset.x)
					{
						//y軸のオフセット値を初期位置に固定
						OffsetBuffer.x = 0.0f;

						//フラグを初期化
						bHorizontalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayHorizontalFlag		= false;	//遅延フラグをfalseに
					}
				}
				//右方向にテクスチャが移動する場合
				else
				{
					//時間によってYの値が0から1に変化していく。1になったら0に戻り、繰り返す。
					OffsetBuffer.x = Mathf.Repeat(Time.time * this.fScrollSpeed , 1);

					//遅延フラグがtrue、且つ前回のオフセット値よりも今回のオフセット値の方が小さい場合(= 1周した場合)
					if (fDelayHorizontalFlag && OffsetBuffer.x < OldOffset.x)
					{
						//y軸のオフセット値を初期位置に固定
						OffsetBuffer.x = 0.0f;

						//フラグを初期化
						bHorizontalExecutionFlag	= false;	//実行フラグをfalseに
						fDelayHorizontalFlag		= false;	//遅延フラグをfalseに
					}
				}
			}

			//サイズ値を算出
			Size.x = (float)(1.0f / nHorizontalAnimationNumber);
			Size.y = (float)(1.0f / nVerticalAnimationNumber);

			//マテリアルにオフセットを設定する
			this.renderer.material.SetTextureOffset("_MainTex", OffsetBuffer);
			this.renderer.material.SetTextureScale("_MainTex", Size);

			//現在のオフセット値を保存する
			OldOffset.x = OffsetBuffer.x;		//x軸
			OldOffset.y = OffsetBuffer.y;		//y軸
		}
	}

	//====================================================================================================
	//メソッド名	：UpdateTextureScroll
	//役割			：テクスチャ更新メソッド
	//引数			：void
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
		if(Offset.x >= 1.0f)
		{
			//初期値に戻す
			Offset.x = 0.0f;
		}

		//サイズ値を算出
		Size.x = (float)(1.0f / nHorizontalAnimationNumber);
		Size.y = (float)(1.0f / nVerticalAnimationNumber);
	
		//マテリアルにオフセットを設定する
		this.renderer.material.SetTextureOffset("_MainTex" , Offset);
		this.renderer.material.SetTextureScale("_MainTex", Size);
	}

	//********************************************************************** 以降、セッター **********************************************************************
	//====================================================================================================
	//メソッド名	：SetScrollSpeed
	//役割			：スクロール速度設定メソッド
	//引数			：(float fSpeed)	設定するスクロールの速度
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
	//引数			：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopVerticalExecutionFlag()
	{
		//遅延発生用フラグがfalseである場合に処理
		if (!(fDelayVerticalFlag))
		{
			//テクスチャの座標が0になるまでスクロールを続ける
			fDelayVerticalFlag = true;
		}
	}

	//====================================================================================================
	//メソッド名	：StopHorizontalExecutionFlag
	//役割			：横方向スクロール停止メソッド
	//引数			：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopHorizontalExecutionFlag()
	{
		//遅延発生用フラグがfalseである場合に処理
		if(!(fDelayHorizontalFlag))
		{
			//テクスチャの座標が0になるまでスクロールを続ける
			fDelayHorizontalFlag = true;
		}
	}

	//********************************************************************** 以降、フラグ切り替え **********************************************************************
	//====================================================================================================
	//メソッド名	：ReverseRepeatFlag
	//役割			：スクロール種類フラグ反転メソッド
	//引数			：void
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
			InvokeRepeating("UpdateTextureScroll" , 0.01f , (1.0f / 10.0f));
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
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void ReverseHorizontalScrollFlag()
	{
		//横方向スクロール実行フラグを反転
		bHorizontalScrollFlag = !(bHorizontalScrollFlag);
	}
}
//================================================================================ EOF ================================================================================