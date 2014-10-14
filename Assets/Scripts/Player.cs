using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public float speedDefault = 30;
	public int lane;

	// 飛んでる最中かどうかのフラグなのです！
	public bool isFly;
	private bool isOffBuilding;

	private GameManager gm;
	private Animator anm;
	private int anmSpeedHash;
	private int anmJumpHash;
	public float jumpPower;
	public float jumpPowerDefault = 100;

	// フィニッシュの処理をここに書いているのでここでカウント(ゲームマネージャに後で移行)
	private int frameCount;
	private bool isPlay;

	void Start() {
		gm = GameObject.FindObjectOfType<GameManager>();
		anm = GetComponent<Animator>();
		anmSpeedHash = Animator.StringToHash("Speed");
		anmJumpHash = Animator.StringToHash("Jump");
		lane = 0;

		speed = speedDefault;
		jumpPower = jumpPowerDefault;
		frameCount = 0;
		isPlay = true;
		isFly = false;
	}

	void Update() {
		float vertical = speed * Time.deltaTime;

		float horizontal = Input.GetAxis("Horizontal");

		if(gm != null && gm.GetStartCount() > 0)
			vertical *= 0;

		transform.Translate(0, 0, vertical);

		anm.SetFloat(anmSpeedHash, vertical);

		if(Input.GetAxis("Jump") > 0 && anm.GetBool(anmJumpHash) == false) {
			anm.SetBool(anmJumpHash, true);
			rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
		}

		if(horizontal > 0) {
			if(lane == 0) {
				lane++;
				transform.position += transform.right * 3;
			}
		} else if(horizontal < 0) {
			if(lane == 1) {
				lane--;
				transform.position += transform.right * -3;
			}
		}

		if(gm != null && gm.GetStartCount() > 0 && isPlay) {
			frameCount++;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		Vector3 dir = collision.gameObject.transform.up * -1;
		transform.LookAt(dir + transform.position);
		anm.SetBool(anmJumpHash, false);
		isFly = false;
		isOffBuilding = false;
		CancelInvoke("OffTheGroundLittle");
	}

	private void OnCollisionStay(Collision collision) {
		anm.SetBool(anmJumpHash, false);
	}

	private void OnCollisionExit(Collision collision) {
		if(collision.gameObject.CompareTag("Building")) {
			isOffBuilding = true;
			Invoke("OffTheGroundLittle", 0.5f);
		}
	}

	private void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Dash")) {
			speed = speedDefault * 1.5f;
			// とりあえず3秒早い
			Invoke("undoSpeed", 3);

			FindObjectOfType<ScoreManager>().AddScore(100);
		}

		if(collider.gameObject.CompareTag("Bonus")) {
			jumpPower = jumpPowerDefault * 5.5f;
			FindObjectOfType<ScoreManager>().AddScore(100);
		}

		// なんとなくこっちに次のシーンへ行く処理書いてしまったが、、、
		// 後にGameSceneへ移行する
		if(collider.gameObject.CompareTag("Finish")) {
			try {
				ScoreManager sm = FindObjectOfType<ScoreManager>();
				int addScore = (frameCount > 200) ? 500 : 500 - (frameCount - 200) * 2;
				sm.AddScore(addScore);
			} catch {
				print("not found score manager");
			}
			try {
				SceneManager sm = FindObjectOfType<SceneManager>();
				sm.NextScene();
			} catch {
				print("not found scene manager...");
			}
		}
		print(collider.gameObject.tag);
	}

	private void OnTriggerExit(Collider collider) {
		if(collider.gameObject.CompareTag("Dash")) {
			//speed = speedDefault;
		}

		if(collider.gameObject.CompareTag("Bonus")) {
			jumpPower = jumpPowerDefault;
		}

		print(collider.tag);
	}

	private void undoSpeed() {
		speed = speedDefault;
	}

	private void OffTheGroundLittle() {
		if(isOffBuilding) {
			anm.SetBool(anmJumpHash, true);
			isFly = true;
		}
	}
}
