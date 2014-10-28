using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {
	public float speed;
	public float speedDefault = 30;
	public int lane = 1;

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
	public int frameCount;
	private bool isPlay;

	// Next RoadJoint
	public RoadJoint roadJoint;

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

		if(horizontal < 0) {
			lane -= (lane > 0) ? 1 : 0;
		} else if(horizontal > 0) {
			lane += (lane < 2) ? 1 : 0;
		}

		gameObject.transform.LookAt(roadJoint.transform);

		if(gm != null && gm.GetStartCount() > 0 && isPlay) {
			frameCount++;
		}
	}

	private void OnCollisionEnter(Collision collision) {
		anm.SetBool(anmJumpHash, false);
		isFly = false;
		isOffBuilding = false;
		CancelInvoke("OffTheGroundLittle");
	}

	private void OnCollisionStay(Collision collision) {
		anm.SetBool(anmJumpHash, false);
	}

	private void OnCollisionExit(Collision collision) {
		Invoke("OffTheGroundLittle", 0.4f);
		isOffBuilding = true;
	}

	private void OnTriggerEnter(Collider collider) 
	{
		try {
			SpecialFloor sf = collider.gameObject.GetComponent<SpecialFloor>();
			sf.Execute(this);
			return;
		} catch {
			print("not special floor");
		}
		try {
			RoadJoint rj = collider.gameObject.GetComponent<RoadJoint>();
			roadJoint = rj.nextJoint.GetComponent<RoadJoint>();
			return;
		} catch {
			print("not road joint");
		}
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

	private void OffTheGroundLittle() {
		if(isOffBuilding) {
			anm.SetBool(anmJumpHash, true);
			isFly = true;
		}
	}

	public void UndoSpeed() {
		speed = speedDefault;
	}
}
