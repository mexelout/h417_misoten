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
	//SoundSpeaker[] SoundSpeaker = new SoundSpeaker[50];

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
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();
		SoundDevice.PlayBGM((int)(CommonSound.BGM_NAME.BGM_MAIN), false);

		sm = GameObject.FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
		
		startCount = 3;
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = startCount.ToString();

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
				if(Input.GetAxis("Jump") == 1) {
					startCountState();
					Destroy(GameObject.FindObjectOfType<IntroCamera>().gameObject);
				}
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
		//******************** サウンド処理(担当：野村) ********************
		SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//プレイヤーオブジェクトに内包されているSoundSpeakerスクリプトを取得する

		startCount--;
		GUIText tm = gameObject.GetComponentInChildren<GUIText>();
		tm.text = startCount.ToString();
		
		//カウントダウンを終了して走り始める場合
		if (startCount <= 0)
		{
			tm.text = "Go !!";
			CancelInvoke("StartCounting");
			Invoke("StartStringHide", 2);
			gameState = GameState.Play;

			SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_COUNTDOWN_BACK), false);			//カウントダウン後半用SEを再生する
		}
		//カウントダウンの値が0以外＝スタートではない場合
		else
		{
			SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_COUNTDOWN_FRONT), false);			//カウントダウン前半用SEを再生する
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
