using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour {

	public int score;
	public int high_score;
	//private int target_score = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

	}

	public void AddScore(int s) {
		score += s;
	}
}
