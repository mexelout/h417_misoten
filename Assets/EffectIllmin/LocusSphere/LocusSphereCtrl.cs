using UnityEngine;
using System.Collections;

public class LocusSphereCtrl : MonoBehaviour {

	public Vector3	POS_INIT	= new Vector3 (0.0f, 0.0f, 0.0f);
	public Vector3	SPIN_RATE	= new Vector3 (0.0f, 1.0f, 1.0f);
	public float	RADIUS		= 10.0f;
	public float	ROUND_TIME	= 10.0f;
	public float	INIT_ANGLE	= 0.0f;

	private float	m_fAngle;
	private float	m_fProgresTime;
	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		m_fAngle 	= INIT_ANGLE;
		m_fProgresTime = 0.0f;
	}

	// Update is called once per frame
	void Update () {
	
		m_fProgresTime += Time.deltaTime;
		if (m_fProgresTime > ROUND_TIME) {
			m_fProgresTime -= ROUND_TIME;
		}

		float fRate;
		fRate = m_fProgresTime / ROUND_TIME;

		m_fAngle = (Mathf.PI * 2) * fRate;

		Vector3 pos;
		pos.x = POS_INIT.x + ( Mathf.Cos (m_fAngle) * RADIUS ) * SPIN_RATE.x;
		pos.y = POS_INIT.y + ( Mathf.Sin (m_fAngle) * RADIUS ) * SPIN_RATE.y;
		pos.z = POS_INIT.z + ( Mathf.Cos (m_fAngle) * RADIUS ) * SPIN_RATE.z;

		transform.position = pos;
	}
}
