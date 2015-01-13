using UnityEngine;
using System.Collections;

public class ParticleStar : MonoBehaviour {

	private GameObject	m_Player;
	private Vector3		m_posPlayer;

	public	Vector3		m_OffsetPlayer		= new Vector3( 0.0f , 0.0f , 0.0f );

	// Use this for initialization
	void Start () {
	
	}

	void Awake(){
		
		// プレイヤー取得
		m_Player	= GameObject.Find("Player");
		m_posPlayer = m_Player.transform.position + m_OffsetPlayer;
	}
	
	// Update is called once per frame
	void Update () {
	
		// プレイヤーの座標再取得
		m_posPlayer = m_Player.transform.position + m_OffsetPlayer;

		transform.position = m_posPlayer;
	}
}
