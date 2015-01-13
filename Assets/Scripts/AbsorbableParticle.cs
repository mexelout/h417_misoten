using UnityEngine;
using System.Collections;

public class AbsorbableParticle : MonoBehaviour {
	// パーティクルのカタチをなす元とするモデル
	public GameObject TargetPrimitive;
	
	[Range(0.0f, 1.0f)]
	public float ParticleSpeed;
	
	// 各パーティクルの目標座標
	private Vector3[] m_targetVertices;
	// 各パーティクル情報
	private ParticleSystem.Particle[] m_targetParticles;
	
	void Start () {
		m_targetVertices = TargetPrimitive.GetComponent<MeshFilter>().sharedMesh.vertices;
		m_targetParticles = new ParticleSystem.Particle[m_targetVertices.Length];
	}
	
	void Update () {
		// ターゲット座標の更新
		m_targetVertices = TargetPrimitive.GetComponent<MeshFilter>().sharedMesh.vertices;
		
		for(int i=0; i<m_targetVertices.Length; i++) {
			// ワールド座標系に変換してTargetPrimitiveの移動に追従させる
			m_targetVertices[i] = TargetPrimitive.transform.TransformPoint(m_targetVertices[i]);
			
			// ちょっと遅延させてパーティクルを移動させる
			m_targetParticles[i].position = m_targetParticles[i].position * (1f - ParticleSpeed) + m_targetVertices[i] * ParticleSpeed;
			// 座標に応じてパーティクルをカラフルに
			m_targetParticles[i].color = new Color(1f - m_targetVertices[i].x % 1f, 0.2f + m_targetVertices[i].y % 0.8f, 0.5f + m_targetVertices[i].z % 0.5f);
			m_targetParticles[i].size = 0.05f;
			
			// ライフタイムを指定しないと表示されない（すぐ死んで消えてる？）
			m_targetParticles[i].lifetime = 10f;
			m_targetParticles[i].startLifetime = 10f;
		}
		
		// パーティクル情報の更新
		GetComponent<ParticleSystem> ().SetParticles (m_targetParticles, m_targetParticles.Length);
	}
}