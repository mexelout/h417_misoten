//==================== インポート ====================
using UnityEngine;
using System.Collections;

//==================== モブクラス ====================
public class MOB : MonoBehaviour
{
	//******************** 定数宣言 ********************
	
	//******************** メンバ変数宣言 ********************
	//++++++++++ プライベート ++++++++++

	//++++++++++ プロテクト ++++++++++

	//++++++++++ パブリック ++++++++++

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
		//TextureScroll ScrollDevice = GameObject.FindObjectOfType<TextureScroll>();
		GameObject Mob1 = GameObject.Find("Ch-Mob1");
		GameObject Mob2 = GameObject.Find("Ch-Mob2");
		TextureScroll ScrollDevice = Mob1.GetComponent<TextureScroll>();
		TextureScroll ScrollDevice2 = Mob2.GetComponent<TextureScroll>();

		//F1キーで縦スクロール切り替え
		if(Input.GetKeyDown(KeyCode.F1))
		{
			//縦スクロール実行フラグを切り替える
			ScrollDevice.ReverseVerticalExecutionFlag();
		}
		//F2キーで上下スクロール方向切り替え
		if (Input.GetKeyDown(KeyCode.F2))
		{
			//縦方向スクロール制御フラグを切り替える
			ScrollDevice.ReverseVerticalScrollFlag();
		}
		//F3キーで横スクロール切り替え
		if(Input.GetKeyDown(KeyCode.F3))
		{
			//横スクロール実行フラグを切り替える
			ScrollDevice.ReverseHorizontalExecutionFlag();
		}
		//F4キーで左右スクロール方向切り替え
		if (Input.GetKeyDown(KeyCode.F4))
		{
			//横方向スクロール制御フラグを切り替える
			ScrollDevice.ReverseHorizontalScrollFlag();
		}
		//F5キーで左右スクロール方向切り替え
		if (Input.GetKeyDown(KeyCode.F5))
		{
			//縦方向
			ScrollDevice.StopVerticalExecutionFlag();
		}
		//F6キーで左右スクロール方向切り替え
		if (Input.GetKeyDown(KeyCode.F6))
		{
			//縦方向
			ScrollDevice.StopHorizontalExecutionFlag();
		}
		//F7キーでスクロール方法切り替え
		if (Input.GetKeyDown(KeyCode.F7))
		{
			//縦方向
			ScrollDevice.ReverseRepeatFlag();
		}
	}
}
//================================================================================ EOF ================================================================================
