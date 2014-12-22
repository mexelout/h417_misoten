using UnityEngine;
using System.Collections;

public class LandingCameraCtrl : MonoBehaviour {

	private GameObject	m_Player;
	private Vector3		m_posPlayer;
	private float		m_fProgresTime;	// 経過時間

	public	float		m_LengthToPlayerStart	= 100.0f;
	public	float		m_LengthToPlayerEnd = 0.1f;
	public	Vector3		m_StartAngle		= new Vector3( 0.0f , 0.0f , 0.0f );
	public	Vector3		m_EndAngle			= new Vector3( 0.0f , Mathf.PI , 0.0f );
	public	float		m_fMoveTime			= 4.0f;
	public	float		m_fWaitTime 		= 1.0f;
	public	Vector3		m_OffsetPlayer		= new Vector3( 0.0f , 0.5f , 0.0f );

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){

		// プレイヤー取得
		m_Player	= GameObject.Find("Player");
		m_posPlayer = m_Player.transform.position + m_OffsetPlayer;

		m_fProgresTime = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

		// プレイヤーの座標再取得
		m_posPlayer = m_Player.transform.position + m_OffsetPlayer;

		// 時間取得
		m_fProgresTime += Time.deltaTime;

		// カメラ移動中
		if (m_fProgresTime < m_fMoveTime) {
			// レート計算
			float fRate = m_fProgresTime / m_fMoveTime;

			// 角度計算
			Vector3 fAngle = m_StartAngle + (m_EndAngle - m_StartAngle) * fRate;

			// 距離計算
			float	fLengthToPlayer	= m_LengthToPlayerStart + (m_LengthToPlayerEnd - m_LengthToPlayerStart) * fRate;

			// 座標計算
			Vector3 pos = m_posPlayer + new Vector3 (Mathf.Cos (fAngle.y) * fLengthToPlayer
			                                         , 0.0f * fLengthToPlayer, 
			                                         Mathf.Sin (fAngle.y) * fLengthToPlayer);

			transform.LookAt (m_posPlayer);
			transform.position = pos;
		}
	}
}
