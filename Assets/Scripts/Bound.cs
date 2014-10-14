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
		player.jumpPower = player.jumpPowerDefault * 20.5f;
		player.rigidbody.AddForce(Vector3.up * player.jumpPower, ForceMode.Impulse);
		FindObjectOfType<ScoreManager>().AddScore(100);
	}

}
