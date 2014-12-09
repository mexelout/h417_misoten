//==================== インポート ====================
using UnityEngine;
using System.Collections;
using CommonSound;

//==================== サウンドスピーカークラス ====================
public class SoundSpeaker : MonoBehaviour
{
	//******************** 定数宣言 ********************

	//******************** メンバ変数宣言 ********************
	//++++++++++ プライベート ++++++++++
	private int					nFadeCounter;		//フェード時に使用するカウンター
	private bool				bSEUseFlag;			//SEデバイス使用フラグ
	private bool				bVoiceUseFlag;		//Voiceデバイス使用フラグ
	private SoundStructure		BGM;				//BGM用サウンドまとめ
	private SoundStructure[]	SE;					//SE用サウンドまとめ
	private SoundStructure[]	Voice;				//Voice用サウンドまとめ
	private SoundManager		SoundManager;		//サウンドマネージャーデバイス

	//++++++++++ プロテクト ++++++++++

	//++++++++++ パブリック ++++++++++
	public int nSEDeviceNumber		= ConstantScore.SE_DEVICE_NUMBER;			//SE再生デバイス生成数
	public int nVoiceDeviceNumber	= ConstantScore.VOICE_DEVICE_NUMBER;		//Voice再生デバイス生成数

	//====================================================================================================
	//メソッド名	：Awake
	//役割			：コンストラクタ的な
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	void Awake()
	{
		//******************** 再生デバイス生成処理 ********************
		//SEの再生デバイス数が0以下の場合
		if(nSEDeviceNumber <= 0)
		{
			//SE使用フラグをfalseに設定
			bSEUseFlag = false;
		}
		else
		{
			//指定されたSEデバイス数だけデバイスを生成し、使用フラグをtrueに設定
			SE = new SoundStructure[nSEDeviceNumber];
			bSEUseFlag = true;
		}
		//Voiceの再生デバイス数が0以下の場合
		if(nVoiceDeviceNumber <= 0)
		{
			//Voice使用フラグをfalseに設定
			bVoiceUseFlag = false;
		}
		else
		{
			//指定されたVoiceデバイス数だけデバイスを生成、使用フラグをtrueに設定
			Voice = new SoundStructure[nVoiceDeviceNumber];
			bVoiceUseFlag = true;
		}

		//******************** 再生デバイス初期化処理 ********************
		BGM.Source				= gameObject.AddComponent<AudioSource>();		//BGM再生デバイスを取得
		BGM.Source.loop			= true;											//BGM再生デバイスにループ設定ON
		BGM.Source.clip			= null;											//再生BGM情報をnull設定
		BGM.nVolumeFadeSpeed	= ConstantScore.VOLUME_FADE_DEFALUT_SPEED;		//フェード速度初期化
		BGM.fVolume				= ConstantScore.VOLUME_MAX;						//BGM再生ボリューム初期化
		BGM.bFadeIn				= false;										//BGMフェードインフラグ初期化
		BGM.bFadeOut			= false;										//BGMフェードアウトフラグ初期化

		//SEデバイスが生成されている場合
		if(bSEUseFlag)
		{
			//SEデータの登録数だけループする
			for(int nLoop = 0 ; nLoop < SE.Length ; nLoop++)
			{
				SE[nLoop].Source			= gameObject.AddComponent<AudioSource>();			//SE登録配列毎に、SEを取得してSE再生デバイスに割り当てる
				SE[nLoop].Source.loop		= true;											//SE再生デバイスにループ設定OFF
				SE[nLoop].Source.clip		= null;												//再生SE情報をnull設定
				SE[nLoop].nVolumeFadeSpeed	= ConstantScore.VOLUME_FADE_DEFALUT_SPEED;			//フェード速度初期化
				SE[nLoop].fVolume			= ConstantScore.VOLUME_MAX;							//SE再生ボリューム初期化
				SE[nLoop].bFadeIn			= false;											//SEフェードインフラグ初期化
				SE[nLoop].bFadeOut			= false;											//SEフェードアウトフラグ初期化
			}
		}

		//Voiceデバイスが生成されている場合
		if(bVoiceUseFlag)
		{
			//Voiceデータの登録数だけループする
			for(int nLoop = 0 ; nLoop < Voice.Length ; nLoop++)
			{
				Voice[nLoop].Source				= gameObject.AddComponent<AudioSource>();		//Voice登録配列毎に、Voiceを取得してVoice再生デバイスに割り当てる
				Voice[nLoop].Source.loop		= false;										//Voice再生デバイスにループ設定OFF
				Voice[nLoop].Source.clip		= null;											//再生SE情報をnull設定
				Voice[nLoop].nVolumeFadeSpeed	= ConstantScore.VOLUME_FADE_DEFALUT_SPEED;		//フェード速度初期化
				Voice[nLoop].fVolume			= ConstantScore.VOLUME_MAX;						//Voice再生ボリューム初期化
				Voice[nLoop].bFadeIn			= false;										//Voiceフェードインフラグ初期化
				Voice[nLoop].bFadeOut			= false;										//Voiceフェードアウトフラグ初期化
			}
		}

		//******************** その他初期化処理 ********************
		nFadeCounter	= 0;																//フェード時に使用するカウンターの初期化	
		SoundManager	= GameObject.Find("SoundManager").GetComponent<SoundManager>();		//サウンドマネージャーのコンポーネントを取得(デバイスとして使用可能に)
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
	void Update()
	{
		//******************** BGMフェード処理 ********************
		//フェードインフラグがONの場合
		if(BGM.bFadeIn)
		{
			//カウンターが設定されたフェード速度以下の場合
			if(nFadeCounter < BGM.nVolumeFadeSpeed)
			{
				//カウンターを加算
				nFadeCounter++;
			}
			//設定されたフェード速度を超えた場合
			else
			{
				BGM.fVolume += ConstantScore.VOLUME_FADE;	//ボリュームを加算
				BGM.Source.volume	= BGM.fVolume;			//変更したボリュームを適応する
				nFadeCounter		= 0;					//カウンターを初期化

				//ボリュームが1.0f(最大値)を超えた場合
				if(BGM.fVolume >= ConstantScore.VOLUME_MAX)
				{
					BGM.fVolume			= ConstantScore.VOLUME_MAX;		//ボリュームを最大値固定
					BGM.Source.volume	= BGM.fVolume;					//変更したボリュームを適応する
					BGM.bFadeIn			= false;						//フェードイン終了
				}
			}
		}
		//フェードアウトフラグがONの場合
		else if(BGM.bFadeOut)
		{
			//カウンターが設定されたフェード速度以下の場合
			if(nFadeCounter < BGM.nVolumeFadeSpeed)
			{
				//カウンターを加算
				nFadeCounter++;
			}
			//設定されたフェード速度を超えた場合
			else
			{
				BGM.fVolume -= ConstantScore.VOLUME_FADE;	//ボリュームを減算
				BGM.Source.volume	= BGM.fVolume;			//変更したボリュームを適応する
				nFadeCounter		= 0;					//カウンターを初期化

				//ボリュームが0.0f(最低値)を超えた場合
				if(BGM.fVolume <= 0.0f)
				{
					BGM.fVolume			= 0.0f;				//ボリュームを最低値固定
					BGM.Source.volume	= BGM.fVolume;		//変更したボリュームを適応する
					BGM.bFadeOut		= false;			//フェードアウト終了

					//BGMを停止する
					StopBGM(false);
				}
			}
		}
	}

	//====================================================================================================
	//メソッド名	：PlayBGM
	//役割			：指定したBGMを再生する
	//引数			：(int nPlayNumber)			再生番号
	//				　(bool bFadeIn)			フェードインで開始するかしないか【true：フェードインで開始　false：フェードイン無し】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void PlayBGM(int nPlayNumber , bool bFadeIn)
	{
		//******************** 変数宣言 ********************
		AudioClip BGMBuffer = SoundManager.GetBGM(nPlayNumber);		//サウンドマネージャーからBGMデータを取得する

		//******************** BGM再生処理 ********************
		//①取得したBGMデータが格納されていて(nullではなく)、②且つ再生中BGMの再生番号と同じ再生番号ではない場合
		if((BGMBuffer) && (BGM.Source.clip != BGMBuffer))
		{
			BGM.Source.Stop();						//BGMを停止
			BGM.Source.clip		= BGMBuffer;		//指定された再生番号のBGMを、BGM再生デバイスに設定
			BGM.Source.volume	= BGM.fVolume;		//BGMボリューム設定
			
			//フェードインで開始する場合
			if(bFadeIn)
			{
				//フェードインフラグをtrueにした上で再生
				FadeInBGM();
			}

			//BGMを再生
			BGM.Source.Play();
			
			//メソッドから抜ける
			return;
		}
		//上記条件に当てはまらなかった場合(再生番号の指定が間違っていた場合)
		else
		{
			//条件毎に原因を載せたエラーログを出力する
			if(BGM.Source.clip == BGMBuffer)
			{
				//エラーログ出力 - 同じBGMが既に再生されている
				Debug.Log("(BGM)指定された再生番号は既に再生中です。 再生番号：" + nPlayNumber);
			}
			else
			{
				//エラーログ出力 - 不明
				Debug.Log("Unknown Error");
			}
			
			//メソッドから抜ける
			return;
		}
	}
	
	//====================================================================================================
	//メソッド名	：StopBGM
	//役割			：再生中のBGMを停止する
	//引数			：(bool bFadeOut)			フェードアウトで開始するかしないか【true：フェードアウトして停止　false：フェードアウト無し】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopBGM(bool bFadeOut)
	{
		//フェードアウトしない場合
		if (!(bFadeOut))
		{
			BGM.Source.Stop();			//BGMを停止
			BGM.Source.clip = null;		//BGM再生デバイスの再生BGM設定にnull代入
			BGM.fVolume = 1.0f;			//再生ボリュームを最大に設定
		}
		else
		{
			//フェードアウトフラグをfalseにする
			FadeOutBGM();
		}
	}

	//====================================================================================================
	//メソッド名	：FadeInBGM
	//役割			：BGMのフェードインを開始するフラグを立てる
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void FadeInBGM()
	{
		//フェードアウトフラグがONでは無い場合に、フェードインを可能にする
		if(!(BGM.bFadeOut))
		{
			BGM.bFadeIn = true;					//フェードインフラグをONにする
			BGM.fVolume = 0.0f;					//フェードインを行うにあたって、ボリュームを0から徐々に上げていく
			BGM.Source.volume = BGM.fVolume;	//変更したボリュームを適応する
		}
	}

	//====================================================================================================
	//メソッド名	：FadeOutBGM
	//役割			：BGMのフェードアウトを開始するフラグを立てる
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void FadeOutBGM()
	{
		//フェードインフラグがONではない場合に、フェードアウトを可能にする
		if(!(BGM.bFadeIn))
		{
			//フェードアウトフラグをONにする
			BGM.bFadeOut = true;
		}
	}

	//====================================================================================================
	//メソッド名	：SetBGMFadeSpeed
	//役割			：BGMのフェード速度を設定する
	//引数			：(int nFrame)		フェード速度([nFrame]毎にボリューム加算)
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void SetBGMFadeSpeed(int nFrame)
	{
		//フェード速度を設定する
		BGM.nVolumeFadeSpeed = nFrame;
	}
	
	//====================================================================================================
	//メソッド名	：PlaySE
	//役割			：指定したSEを再生する
	//引数			：(int nPlayNumber)		再生番号
	//				　(bool bLoopFlag)		ループ設定【true：ループする　false：ループしない】
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void PlaySE(int nPlayNumber , bool bLoopFlag)
	{
		//******************** 変数宣言 ********************
		AudioClip SEBuffer	= SoundManager.GetSE(nPlayNumber);		//サウンドマネージャーからSEデータを取得する

		//******************** SE再生処理 ********************
		//SEデバイスが生成されている場合
		if(bSEUseFlag)
		{
			//①取得したSEデータが格納されている(nullではない)場合
			if(SEBuffer)
			{
				//SE再生デバイス中から、再生中でないSEを探索する
				for(int nLoop = 0 ; nLoop < SE.Length ; nLoop ++)
				{
					//対象のSE再生デバイスが再生中ではない場合
					if(!(SE[nLoop].Source.isPlaying))
					{
						SE[nLoop].Source.loop	= bLoopFlag;				//ループ設定
						SE[nLoop].Source.clip	= SEBuffer;					//指定された再生番号のSEを、SE再生デバイスに設定
						SE[nLoop].Source.volume	= SE[nLoop].fVolume;		//再生ボリューム設定
						SE[nLoop].Source.Play();							//SEを再生

						//メソッドから抜ける
						return;
					}
				}
				//SE再生デバイスを全て探索し終えた場合
				{
					//エラーログ出力 - SE再生デバイスが全て使われている
					Debug.Log("(SE)SE再生デバイスが全て使われています。");
				}
			}
		}
	}
	
	//====================================================================================================
	//メソッド名	：StopSE
	//役割			：指定した再生中のSEを停止する
	//引数			：(int nPlayNumber)			再生番号
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopSE(int nPlayNumber)
	{
		//******************** 変数宣言 ********************
		AudioClip SEBuffer	= SoundManager.GetSE(nPlayNumber);		//サウンドマネージャーからSEデータを取得する

		//******************** SE停止処理 ********************
		//SEデバイスが生成されている場合
		if(bSEUseFlag)
		{
			//①取得したSEデータが格納されている(nullではない)場合
			if(SEBuffer)
			{
				//SE再生デバイス中から、再生中の同名SEを探索する
				for(int nLoop = 0 ; nLoop < SE.Length ; nLoop ++)
				{
					//①対象のSE再生デバイスが再生中、②且つ指定した再生番号のSEが再生されている場合
					if((SE[nLoop].Source.isPlaying) && SE[nLoop].Source.clip == SEBuffer)
					{
						SE[nLoop].Source.Stop();			//SEを停止する
						SE[nLoop].Source.clip	= null;		//SE再生デバイスの再生SE設定にnull代入
						SE[nLoop].fVolume		= 1.0f;		//再生ボリュームを最大に設定

						//メソッドから抜ける
						return;
					}
				}
				//SE再生デバイスを全て探索し終えた場合
				{
					//エラーログ出力 - SE再生デバイスが全て使われている
					Debug.Log("(SE)指定した再生番号のSEは現在再生されていません。");
				}
			}
		}
	}
	
	//====================================================================================================
	//メソッド名	：StopAllSE
	//役割			：全ての再生中のSEを停止する
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopAllSE()
	{
		//SEデバイスが生成されている場合
		if(bSEUseFlag)
		{
			//SE再生デバイス分だけループする
			for(int nLoop = 0 ; nLoop < SE.Length ; nLoop ++)
			{
				SE[nLoop].Source.Stop();			//SEを停止する
				SE[nLoop].Source.clip	= null;		//SE再生デバイスの再生SE設定にnull代入
				SE[nLoop].fVolume		= 1.0f;		//再生ボリュームを最大に設定
			}
		}
	}
	
	//====================================================================================================
	//メソッド名	：PlayVoice
	//役割			：指定したVoiceを再生する
	//引数			：(int nPlayNumber)		再生番号
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void PlayVoice(int nPlayNumber)
	{
		//******************** 変数宣言 ********************
		AudioClip VoiceBuffer	= SoundManager.GetVoice(nPlayNumber);		//サウンドマネージャーからVoiceデータを取得する

		//******************** Voice再生処理 ********************
		//Voiceデバイスが生成されている場合
		if(bVoiceUseFlag)
		{
			//①取得したVoiceデータが格納されている(nullではない)場合
			if(VoiceBuffer)
			{
				//Voice再生デバイス中から、再生中でないVoiceを探索する
				for(int nLoop = 0 ; nLoop < Voice.Length ; nLoop++)
				{
					//対象のVoice再生デバイスが再生中ではない場合
					if(!(Voice[nLoop].Source.isPlaying))
					{
						Voice[nLoop].Source.clip	= VoiceBuffer;				//指定された再生番号のVoiceを、Voice再生デバイスに設定
						Voice[nLoop].Source.volume	= Voice[nLoop].fVolume;		//再生ボリューム設定
						Voice[nLoop].Source.Play();								//Voiceを再生

						//メソッドから抜ける
						return;
					}
				}
				//Voice再生デバイスを全て探索し終えた場合
				{
					//エラーログ出力 - Voice再生デバイスが全て使われている
					Debug.Log("(Voice)Voice再生デバイスが全て使われています。");
				}
			}
		}
	}
	
	//====================================================================================================
	//メソッド名	：StopVoice
	//役割			：指定した再生中のVoiceを停止する
	//引数			：(int nPlayNumber)		再生番号
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopVoice(int nPlayNumber)
	{
		//******************** 変数宣言 ********************
		AudioClip VoiceBuffer	= SoundManager.GetVoice(nPlayNumber);		//サウンドマネージャーからVoiceデータを取得する

		//******************** Voice停止処理 ********************
		//Voiceデバイスが生成されている場合
		if(bVoiceUseFlag)
		{
			//①取得したVoiceデータが格納されている(nullではない)場合
			if(VoiceBuffer)
			{
				//Voice再生デバイス中から、再生中の同名Voiceを探索する
				for(int nLoop = 0 ; nLoop < Voice.Length ; nLoop++)
				{
					//①対象のVoice再生デバイスが再生中、②且つ指定した再生番号のVoiceが再生されている場合
					if((Voice[nLoop].Source.isPlaying) && Voice[nLoop].Source.clip == VoiceBuffer)
					{
						Voice[nLoop].Source.Stop();				//Voiceを停止する
						Voice[nLoop].Source.clip	= null;		//Voice再生デバイスの再生Voice設定にnull代入
						Voice[nLoop].fVolume		= 1.0f;		//再生ボリュームを最大に設定

						//メソッドから抜ける
						return;
					}
				}
				//Voice再生デバイスを全て探索し終えた場合
				{
					//エラーログ出力 - Voice再生デバイスが全て使われている
					Debug.Log("(Voice)指定した再生番号のVoiceは現在再生されていません。");
				}
			}
		}
	}
	
	//====================================================================================================
	//メソッド名	：StopAllVoice
	//役割			：全ての再生中のVoiceを停止する
	//引数			：void
	//戻り値		：void
	//作成者		：Nomura Syouhei
	//====================================================================================================
	public void StopAllVoice()
	{
		//Voiceデバイスが生成されている場合
		if(bVoiceUseFlag)
		{
			//Voice再生デバイス分だけループする
			for(int nLoop = 0 ; nLoop < Voice.Length ; nLoop++)
			{
				Voice[nLoop].Source.Stop();				//Voiceを停止する
				Voice[nLoop].Source.clip	= null;		//Voice再生デバイスの再生Voice設定にnull代入
				Voice[nLoop].fVolume		= 1.0f;		//再生ボリュームを最大に設定
			}
		}
	}
}
//================================================================================ EOF ================================================================================
