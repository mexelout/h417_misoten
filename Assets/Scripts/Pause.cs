using UnityEngine;
using System.Collections;

public class Pause : SpecialFloor {
	public static bool paused{
		get;
		private set;
	}

	public string firstLine;
	public string secondLine;

	// Use this for initialization
	void Start () {
		paused = false;
	}
	
	// Update is called once per frame
	void Update () {
	}

	void LateUpdate() {
		// 全オブジェクトのUpdateが呼ばれた後に呼ばれるUpdate関数です。
		if(Input.GetButtonUp ("Start") && paused) {
			paused = false;
			Time.timeScale = 1;
		}
	}

	public override void Execute(Player player) {
		if(!paused) {
			TutorialManager tm = FindObjectOfType<TutorialManager>();
			tm.g[0].text = firstLine;
			tm.g[1].text = secondLine;
			Time.timeScale = 0;
			paused = true;
		}
	}
}
