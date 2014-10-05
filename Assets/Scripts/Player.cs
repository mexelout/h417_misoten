using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public float speedDefault = 30;
	public int lane;
	private GameManager gm;
	private Animator anm;
	private int anmSpeedHash;
	private int anmJumpHash;
	public float jumpPower;
	public float jumpPowerDefault = 100;

	void Start() {
		gm = GameObject.FindObjectOfType<GameManager>();
		anm = GetComponent<Animator>();
		anmSpeedHash = Animator.StringToHash("Speed");
		anmJumpHash = Animator.StringToHash("Jump");
		lane = 0;

		speed = speedDefault;
		jumpPower = jumpPowerDefault;
	}

	void Update() {
		float vertical = speed * Time.deltaTime;

		float horizontal = Input.GetAxis("Horizontal");

		if(gm.GetStartCount() > 0)
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
	}

	private void OnCollisionEnter(Collision collision) {
		Vector3 dir = collision.gameObject.transform.up * -1;
		transform.LookAt(dir + transform.position);
		anm.SetBool(anmJumpHash, false);
	}

	private void OnCollisionStay(Collision collision) {
		anm.SetBool(anmJumpHash, false);
	}

	private void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.CompareTag("Dash")) {
			speed = speedDefault * 1.5f;
		}

		if(collider.gameObject.CompareTag("Bonus")) {
			jumpPower = jumpPowerDefault * 5.5f;
		}

		// なんとなくこっちに次のシーンへ行く処理書いてしまったが、、、
		// 後にGameSceneへ移行する
		if(collider.gameObject.CompareTag("Finish")) {
			SceneManager sm = FindObjectOfType<SceneManager>();
			print(sm);
			if(sm != null) {
				sm.NextScene();
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
		print(collider.gameObject.tag);
	}
}
