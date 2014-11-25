using UnityEngine;
using System.Collections;

public class Bonus : SpecialFloor {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public override void Execute(Player player)
	{
		FindObjectOfType<ScoreManager>().PlusNowScore(1000);
	}

}
