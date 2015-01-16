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

	public Material goMaterial;
	private GameObject countDownTextureScroll;

	void Awake() {
		try {
			foreach(TextureScroll ts in GetComponentsInChildren<TextureScroll>()) {
				if(ts.gameObject.name.Contains("CountDown")) {
					countDownTextureScroll = ts.gameObject;
					ts.StopHorizontalExecutionFlag(2, true);
					break;
				}
			}
		} catch {
			print("Count Down Texture Scroll error");
		}
	}

	// Use this for initialization
	void Start () {
		SoundManager SoundDevice = GameObject.FindObjectOfType<SoundManager>();
		SoundDevice.PlayBGM((int)(CommonSound.BGM_NAME.BGM_MAIN), false);

		sm = GameObject.FindObjectOfType<ScoreManager>().GetComponent<ScoreManager>();
		
		startCount = 3;

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
		SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();

		startCount--;
		try {
			countDownTextureScroll.GetComponent<TextureScroll>().StopHorizontalExecutionFlag(startCount - 1, true);
		} catch {
			print("game manager count down scroll error");
		}
		
		//カウントダウンを終了して走り始める場合
		if (startCount <= 0)
		{
			//tm.text = "Go !!";
			countDownTextureScroll.renderer.material = goMaterial;
			countDownTextureScroll.transform.localScale = new Vector3(4, 2.5f, 1);
			Destroy(countDownTextureScroll.GetComponent<TextureScroll>());
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
		countDownTextureScroll.transform.localScale = new Vector3(0, 0, 1);
	}

	public void startCountState() {
		gameState = GameState.Count;
		InvokeRepeating("StartCounting", 1, 1);
	}

	public void startFinishState() {
		gameState = GameState.Finish;
	}
}
