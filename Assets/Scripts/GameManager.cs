using UnityEngine;
using System.Collections;

public class GameManager : MonoBehaviour {

	public int stageNumber = 1;
	private int startCount = 5;

	// Use this for initialization
	void Start () {
		InvokeRepeating("StartCounting", 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
	}

	private void StartCounting() {
		startCount--;
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = startCount.ToString();
		if(startCount <= 0) {
			tm.text = "Go !!";
			CancelInvoke("StartCounting");
			Invoke("StartStringHide", 2);
		}
	}

	private void StartStringHide() {
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = "";
	}

	public int GetStartCount() {
		return startCount;
	}
}
