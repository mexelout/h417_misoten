using UnityEngine;
using System.Collections;

public class LocusSphereSpiralCtrl : MonoBehaviour {

	public float RADIUS			= 10.0f;
	public float RISE_TIME		= 5.0f;
	public float MAX_WAIT_TIME = 5.0f;
	public float ROUND_TIME = 5.0f;
	public float INIT_ANGLE = 0.0f;
	public float RISE_DISTANCE = 100.0f;

	private Vector3	POS_INIT = new Vector3 (0.0f, 0.0f, 0.0f);

	private bool	m_bMoveFlag	= true;
	private float	m_fWaitTime	= 0.0f;
	private float	m_fProgresTime	= 0.0f;
	private float	m_fAngle	= 0.0f;
	private float	m_fRiseValue	= 0.0f;

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		m_fAngle = INIT_ANGLE;
		m_bMoveFlag	= true;
		m_fWaitTime	= 0.0f;
		m_fProgresTime = 0.0f;
		m_fRiseValue = RISE_DISTANCE / RISE_TIME;

		POS_INIT = transform.position;
	}

	// Update is called once per frame
	void Update () {
	
		// 上昇
		if (m_bMoveFlag == true) {

			// ｙ座標更新
			Vector3 pos = transform.position;
			pos.y += Time.deltaTime * m_fRiseValue;

			// ｘｚ座標更新
			m_fProgresTime += Time.deltaTime;
			if (m_fProgresTime > ROUND_TIME) {
				m_fProgresTime -= ROUND_TIME;
			}
			
			float fRate;
			fRate = m_fProgresTime / ROUND_TIME;
			
			m_fAngle = (Mathf.PI * 2) * fRate;

			pos.x = POS_INIT.x + ( Mathf.Cos (m_fAngle) * RADIUS );
			pos.z = POS_INIT.z + ( Mathf.Sin (m_fAngle) * RADIUS );
			transform.position = pos;


			if (transform.position.y > POS_INIT.y + RISE_DISTANCE ) {
					m_bMoveFlag = false;
			}
		// 待機
		} else {
			m_fWaitTime += Time.deltaTime;

			if( m_fWaitTime > MAX_WAIT_TIME )
			{
				m_fWaitTime = 0.0f;
				m_bMoveFlag = true;
				transform.position = POS_INIT;
			}
		}
	}
}
