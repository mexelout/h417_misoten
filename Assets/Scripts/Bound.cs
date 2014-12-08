using UnityEngine;
using System.Collections;

public class Bound : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		player.rigidbody.AddForce(Vector3.up * player.jumpPower * 4f, ForceMode.Force);
		FindObjectOfType<ScoreManager>().PlusNowScore(100);
		player.Invoke("EndRotate", 0.1f); // ジャンプアニメーション呼んでる
	}

}
