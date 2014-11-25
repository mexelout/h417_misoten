using UnityEngine;
using System.Collections;

public class Pause : SpecialFloor {
	public bool paused = false;

	public string firstLine;
	public string secondLine;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetButtonUp ("Start")) {
			paused = false;
			Time.timeScale = 1;
		}
	}

	public override void Execute(Player player)
	{
		if(!paused) {
			TutorialManager tm = FindObjectOfType<TutorialManager>();
			tm.g[0].text = firstLine;
			tm.g[1].text = secondLine;
			Time.timeScale = 0;
			paused = true;
		}
	}
}
