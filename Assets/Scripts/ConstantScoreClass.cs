//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━
//※ConstantScoreClassの使い方について
//このクラスは、スコアの加算値を定数として纏めたクラスです。
//このクラスを使用する場合は、インポート時点で「using CommonScore;」と記述をする事。
//オブジェクトに対してこのスクリプトを張り付ける必要はありません。
//
//using UnityEngine;
//using System.Collections;
//using CommonScore;
//
//のような書式になります。
//読込後は、「int i = ConstantScore.RUN」「PlusScore(ConstantScore.JUMP);」などの様に使用することが出来ます。
//
//※※※※※ 最終更新 2014/11/10 : ※※※※※
//━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━━

//==================== インポート ====================
using UnityEngine;
using System.Collections;

//==================== スコア用定数名前領域 ====================
namespace CommonScore
{
	//==================== スコア用定数クラス ====================
	public static class ConstantScore
	{
		//******************** 定数宣言 ********************
		public static readonly int		RUN					= 2;					//一定時間走った場合に加算するスコア値
		public static readonly int		JUMP				= 10;					//ジャンプ時に加算するスコア値
		public static readonly int		ACTION				= 30;					//アクション時に加算するスコア値
		public static readonly int		RANKING_MAX			= 10;					//ランキング用スコア保存数
		public static readonly int		RANKING_DIGIT_MAX	= 5;					//ランキング用スコア桁数
		public static readonly string	RANKING_DATAPASS	= "/Ranking.txt";		//ランキング保存場所
	}
}
//================================================================================ EOF ================================================================================
