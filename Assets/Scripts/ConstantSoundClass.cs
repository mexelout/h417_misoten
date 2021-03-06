﻿//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
//※ConstantSoundClassの使い方について
//このクラスは、サウンド関連の定数を纏めたクラスです。
//このクラスを使用する場合は、インポート時点で「using CommonSound;」と記述をする事。
//オブジェクトに対してこのスクリプトを張り付ける必要はありません。
//
//using UnityEngine;
//using System.Collections;
//using CommonSound;
//
//のような書式になります。
//読込後は、「int JumpSENumber = (int)(CommonSound.JUMP);」「int Volume = ConstantSound.VOLUME_MAX;」などの様に使用することが出来ます。
//
//※※※※※ 最終更新 2014/12/08 11:20 ※※※※※
//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

//==================== インポート ====================
using UnityEngine;
using System.Collections;

//==================== スコア用定数名前領域 ====================
namespace CommonSound
{
	//******************** 構造体型宣言 ********************
	//==================== サウンド構造体 ====================
	struct SoundStructure
	{
		public AudioSource	Source;				//再生デバイス(リソース)
		public int			nVolumeFadeSpeed;	//ボリュームフェードの速さ
		public float		fVolume;			//再生ボリューム
		public bool			bFadeIn;			//フェードインフラグ【true：フェードインする　false：フェードインしない】
		public bool			bFadeOut;			//フェードアウトフラグ【true：フェードアウトする　false：フェードアウトしない】
	}

	//******************** 列挙型宣言 ********************
	//BGMの名前を宣言(int型を基準に設定)
	enum BGM_NAME : int
	{
		BGM_TEST1 = 0	,		//敵艦隊見ゆ
		BGM_TEST2		,		//決戦！鉄底海峡を抜けて！
		BGM_TITLE		,		//タイトル画面用BGM
		BGM_TUTORIAL	,		//チュートリアル画面用BGM
		BGM_MAIN		,		//ゲーム画面用BGM
		BGM_RESULT				//リザルト画面用BGM
	};

	//SEの名前を宣言(int型を基準に設定)
	enum SE_NAME : int
	{
		SE_CHEER1 = 0		,		//歓声1
		SE_CHEER2			,		//歓声2
		SE_CHEER3			,		//歓声3
		SE_JUMP				,		//ジャンプ時
		SE_FALL				,		//転倒時
		SE_DASH				,		//ダッシュ床
		SE_RING				,		//リング通過用
		SE_COUNTDOWN_FRONT	,		//カウントダウン前奏
		SE_COUNTDOWN_BACK	,		//カウントダウン終了
	};

	//Voiceの名前を宣言(int型を基準に設定)
	enum VOICE_NAME : int
	{

	};

	//==================== スコア用定数クラス ====================
	public static class ConstantSound
	{
		//******************** 定数宣言 ********************
		public static readonly int		SE_DEVICE_NUMBER			= 2;			//SE再生デバイス数
		public static readonly int		VOICE_DEVICE_NUMBER			= 0;			//Voice再生デバイス数
		public static readonly int		VOLUME_FADE_DEFALUT_SPEED	= 5;			//ボリュームフェードの標準速度
		public static readonly float	VOLUME_MAX					= 1.0f;			//最大ボリューム値
		public static readonly float	VOLUME_FADE					= 0.02f;		//ボリュームフェードの減少値
	}
}
//================================================================================ EOF ================================================================================