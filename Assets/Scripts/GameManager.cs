using UnityEngine;
using System.Collections;
using System.IO;

public class GameManager : MonoBehaviour {

	public int stageNumber = 1;
	public int startCount {
		get;
		private set;
	}
	private GUIText scoreBoard;
	private ScoreManager sm;
	SoundSpeaker[] SoundSpeaker = new SoundSpeaker[50];

	public int frameCount;

	public enum GameState {
		Intro = 0,
		Count = 1,
		Play = 2,
		Finish = 3,
	};

	public GameState gameState {
		get;
		private set;
	}

	[SerializeField]
	private GameObject _introCamera;
	public GameObject introCamera {
		get { return _introCamera; }
		private set { _introCamera = value; }
	}

	// Use this for initialization
	void Start () {
		sm = GameObject.FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
		startCount = 5;
		try {
			sm.SetNowScore(0);
		} catch {
			print("not found score manager");
		}
		frameCount = 0;
		gameState = GameState.Intro;
		Instantiate(introCamera);
	}
	
	// Update is called once per frame
	void Update () {
		switch(gameState) {
			case GameState.Intro:
				break;
			case GameState.Count:
				break;
			case GameState.Play:
				frameCount++;
				break;
			case GameState.Finish:
				break;
			default:
				break;
		}
	}

	private void StartCounting() {
		startCount--;
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = startCount.ToString();
		if(startCount <= 0) {
			tm.text = "Go !!";
			CancelInvoke("StartCounting");
			Invoke("StartStringHide", 2);
			gameState = GameState.Play;
		}
	}

	private void StartStringHide() {
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = "";
	}

	public void startCountState() {
		gameState = GameState.Count;
		InvokeRepeating("StartCounting", 1, 1);
	}

	public void startFinishState() {
		gameState = GameState.Finish;
	}
}
