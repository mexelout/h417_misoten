using UnityEngine;
using System.Collections;

public class FirewoksCtrl : MonoBehaviour {

	private int MAX_FIREWORKS	= 4;
	private GameObject[] m_FireWorks;
	private ParticleSystem[] m_FireWorksPar;
	private float[] m_fTime;

	public float	INTERVAL_TIME	= 5.0f;
	public Vector3	RANDOM_POS	= new Vector3( 10.0f , 10.0f , 10.0f );
	public bool		DRAW_FLAG	= true;

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){

		m_FireWorks		= new GameObject[MAX_FIREWORKS];
		m_FireWorksPar	= new ParticleSystem[MAX_FIREWORKS];
		m_fTime		= new float[ MAX_FIREWORKS ];

		m_FireWorks[0]	= gameObject.transform.FindChild ("FireworksRed").gameObject;
		m_FireWorks[1]	= gameObject.transform.FindChild ("FireworksGreen").gameObject;
		m_FireWorks[2]	= gameObject.transform.FindChild ("FireworksCyan").gameObject;
		m_FireWorks[3]	= gameObject.transform.FindChild ("FireworksYellow").gameObject;

		m_FireWorksPar[0]	= gameObject.transform.FindChild ("FireworksRed").gameObject.GetComponent<ParticleSystem>();
		m_FireWorksPar[1]	= gameObject.transform.FindChild ("FireworksGreen").gameObject.GetComponent<ParticleSystem>();
		m_FireWorksPar[2]	= gameObject.transform.FindChild ("FireworksCyan").gameObject.GetComponent<ParticleSystem>();
		m_FireWorksPar[3]	= gameObject.transform.FindChild ("FireworksYellow").gameObject.GetComponent<ParticleSystem>();

		m_FireWorksPar [0].Stop ();
		m_FireWorksPar [1].Stop ();
		m_FireWorksPar [2].Stop ();
		m_FireWorksPar [3].Stop ();

		for (int i = 0; i < MAX_FIREWORKS; i++) {
			m_fTime[i] = Random.value * INTERVAL_TIME;
		}

	}
	
	// Update is called once per frame
	void Update () {
	
		if (DRAW_FLAG == false) {
			return;
		}

		for (int i = 0; i < MAX_FIREWORKS; i++) {
		
			// 時間加算
			m_fTime[i] += Time.deltaTime;

			// 花火爆発
			if (m_fTime[i] > INTERVAL_TIME) {
				m_fTime[i] -= INTERVAL_TIME;

				Vector3 pos;
				pos.x	= transform.position.x + ( Random.value * 2 - 1 ) * RANDOM_POS.x;
				pos.y	= transform.position.y + ( Random.value * 2 - 1 ) * RANDOM_POS.y;
				pos.z	= transform.position.z + ( Random.value * 2 - 1 ) * RANDOM_POS.z;

				m_FireWorks[i].transform.position = pos;
				m_FireWorksPar[i].Play();

				m_fTime[i] += Random.value;
			}

		}

	}
}
