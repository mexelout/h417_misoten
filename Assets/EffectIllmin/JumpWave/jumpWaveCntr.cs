using UnityEngine;
using System.Collections;

public class jumpWaveCntr : MonoBehaviour {

	private GameObject	m_Quad;
	private float		m_fProgresTime;

	public	float	START_SIZE	= 0.0f;
	public	float	END_SIZE	= 0.0f;
	public	float	MAX_TIME	= 5.0f;

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		/*
		m_Quad			= transform.Find ("JumpWaveQuad");
		m_fProgresTime	= 0.0f;
		*/
	}

	// Update is called once per frame
	void Update () {
		/*
		Vector3 scl;
		float fRate;

		m_fProgresTime += Time.deltaTime;

		if (m_fProgresTime > MAX_TIME) {
			Destroy();
			return;
		}

		fRate = m_fProgresTime / MAX_TIME;
		scl.x = START_SIZE + ((END_SIZE - START_SIZE) * fRate);
		scl.y = START_SIZE + ((END_SIZE - START_SIZE) * fRate);
		scl.z = 1.0f;
		m_Quad.transform.localScale = scl;

*/
	}
}
