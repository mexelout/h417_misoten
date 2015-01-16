using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using CommonSound;		//サウンド用定数をまとめたス

public class Player : MonoBehaviour {
	public float speed;
	public float speedDefault = 30;

	public int roadNumber = 1;
	public bool isChangeRoadNumber = false;

	// 飛んでる最中かどうかのフラグなのです！
	public bool isFly;
	private bool isOffBuilding;

	private GameManager gm;
	private Animator anm;
	private int anmSpeedHash;
	private int anmJumpHash;
	private int anmRotHash;
	private int anmStumbleHash;
	private int anmLandingHash;
	public float jumpPower;
	public float jumpPowerDefault = 250;

	public Bezier myBezier;
	public float t = 0f;

	// Next RoadJoint
	[SerializeField]
	private RoadJoint _roadJoint;
	public RoadJoint roadJoint {
		get { return _roadJoint; }
		private set { _roadJoint = value; }
	}

	private LineRenderer line;

	private float offsetSide;

	// ジャンプ時の円
	[SerializeField]
	private GameObject _circle;
	public GameObject circle {
		get { return _circle; }
		private set { _circle = value; }
	}
	// 生成後にデストロイするので保持
	private Circle generateCircle;
	public GameObject jumpEffect;
	private GameObject generateJumpEffect;

	public Dictionary<string, bool> roadJointIs {
		get;
		private set;
	}

	void Start() {
		gm = GameObject.FindObjectOfType<GameManager>();
		anm = GetComponent<Animator>();
		anmSpeedHash = Animator.StringToHash("Speed");
		anmJumpHash = Animator.StringToHash("Jump");
		anmRotHash = Animator.StringToHash("Rotate");
		anmStumbleHash = Animator.StringToHash("Stumble");
		anmLandingHash = Animator.StringToHash("Landing");
		speed = speedDefault;
		jumpPower = jumpPowerDefault;
		isFly = false;

		line = gameObject.GetComponent<LineRenderer>();

		roadJointIs = new Dictionary<string, bool>() {
			{ "Road", false },
			{ "Wall", false },
			{ "Jump", false },
			{ "Parabola", false },
		};
	}

	void Update() {
		if(!roadJoint) {
			anm.SetFloat(anmSpeedHash, 0);
			return;
		}
		UpdateRoadJointIs();

		float vertical = speed * Time.deltaTime;
		float horizontal = (roadJointIs["Jump"] || roadJointIs["Parabola"]) ? 0 : Input.GetAxis("Horizontal");

		if((gm != null && gm.gameState != GameManager.GameState.Play) || anm.GetBool(anmLandingHash))
			vertical *= 0;

		if((gm != null && gm.gameState != GameManager.GameState.Play))
			return;

		if(roadJointIs["Parabola"]) {
			Vector3 vec = myBezier.GetPointAtTime(t);
			transform.position = vec;
			t += 0.005f * (speed / speedDefault);
			if(t > 1f)
				t = 1f;
		} else {
			transform.Translate(0, 0, vertical);
		}

		if(anm.GetBool(anmStumbleHash) == false) {
			anm.SetFloat(anmSpeedHash, vertical);
		}

		//キーが押されたら通常ジャンプ
		if(Input.GetButtonDown("Jump") && generateCircle == null) {
			if(anm.GetBool(anmRotHash) == false && anm.GetBool(anmJumpHash) == false && isFly == false && roadJoint.name.Contains("Road") && anm.GetBool(anmStumbleHash) == false && anm.GetBool(anmLandingHash) == false) {
				anm.SetBool(anmRotHash, true);
				anm.Play("Rotate");
				rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
				isFly = true;

				//******************** サウンド処理(担当：野村) ********************
				SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//プレイヤーオブジェクトに内包されているSoundSpeakerスクリプトを取得する
				SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_JUMP), false);			//ジャンプ用SEを再生する
			}
		}

		speed -= (speed - speedDefault) * 0.005f;

		line.SetPosition(0, new Vector3(0, -4, 2));
		line.SetPosition(1, new Vector3(0, -4, 2) + new Vector3(5, 0, -1).normalized * (speed / speedDefault) * 5);

		if(roadJoint.prevJoint && anm.GetBool(anmStumbleHash) == false && anm.GetBool(anmLandingHash) == false) {
			if(horizontal < 0) {
				float movingSpeed = -0.25f;
				offsetSide += movingSpeed;
				if(offsetSide < -4.5f) {
					offsetSide = -4.5f;
				} else {
					transform.position += roadJoint.transform.right * movingSpeed;
				}
				isChangeRoadNumber = true;       
			} else if(horizontal > 0) {
				float movingSpeed = 0.25f;
				offsetSide += movingSpeed;
				if(offsetSide > 4.5f) {
					offsetSide = 4.5f;
				} else {
					transform.position += roadJoint.transform.right * movingSpeed;
				}
				isChangeRoadNumber = true;
			} else if(horizontal == 0) {
				isChangeRoadNumber = false;
			}
		}

		if(roadJointIs["Jump"] || roadJointIs["Parabola"]) {
			rigidbody.useGravity = false;
			anm.SetBool(anmJumpHash, true);

		} else {
			rigidbody.useGravity = true;
		}

		TargetLock();

		if(generateCircle) {
			if(generateCircle.IfProcess(this)) {
			}
		}

		if(roadJointIs["Wall"]) {
			Vector3 v = rigidbody.velocity;
			v.x = v.z = 0;
			rigidbody.velocity = v;
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
		//Invoke("OffTheGroundLittle", 0.4f);
		isOffBuilding = true;
	}

	private void OnTriggerEnter(Collider collider) {
		try {
			SpecialFloor sf = collider.gameObject.GetComponent<SpecialFloor>();
			sf.Execute(this);
			return;
		} catch {
			//print("not special floor");

		}
		try {
			RoadJoint rj = collider.gameObject.GetComponent<RoadJoint>();
			if(rj.landing) {
				ToggleLandingAnimation();
				if(rj) {
					Invoke("ToggleLandingAnimation", rj.landingWaitTime);
					if(rj.landingEffect) (Instantiate(rj.landingEffect) as GameObject).transform.position = transform.position;
					if(rj.landingCamera) Instantiate(rj.landingCamera);
				}
			}
			roadJoint = rj.nextJoint.GetComponent<RoadJoint>();
			UpdateRoadJointIs();
			if(generateJumpEffect) {
				Destroy(generateJumpEffect.gameObject);
				generateJumpEffect = null;
			}
			if(generateCircle) {
				// ステータスが良ければエフェクト生成
				if(generateCircle.state > 0) {
				}
				Destroy(generateCircle.gameObject);
				generateCircle = null;
			}
			if(roadJoint.nextJoint) {
				if(roadJoint.nextJoint.name.Contains("Parabola") || roadJoint.nextJoint.name.Contains("Jump")) {
					if(roadJoint.nextJoint.GetComponent<RoadJoint>().NotCircle == false) {
						generateCircle = (Instantiate(circle) as GameObject).GetComponent<Circle>();
						generateCircle.player = gameObject;
						generateCircle.targetPosition = roadJoint.transform.position.Clone();
					}
				}
			}

			Physics.gravity = Vector3.down * 39.2f;
			if(roadJointIs["Wall"]) {
				Physics.gravity = (Vector3.up * 1.5f + roadJoint.transform.up) / 2 * -39.2f;
				transform.LookAt(roadJoint.transform);
			} else if(roadJointIs["Parabola"]) {
				//演出Jump
				Vector3 p = new Vector3(0.0f,0.0f,0.0f);
				Vector3 p0 =new  Vector3(0.0f,0.0f,0.0f);
				Vector3 p1 =new  Vector3(0.0f,0.0f,0.0f);
				Vector3 p2 =new  Vector3(0.0f,0.0f,0.0f);
				Vector3 p3 =new  Vector3(0.0f,0.0f,0.0f);
				t = 0;
				rigidbody.velocity = new Vector3(0, 0, 0);
				//	p0 = roadJoint.prevJoint.transform.position;
				p0 = roadJoint.prevJoint.transform.position.Clone();
				p3 = roadJoint.transform.position.Clone();
				Vector3 dir = p3 - p0;
				p1 = p0 + dir * 0.2f;
				p2 = p0 + dir * 0.8f;
				p1.y = p2.y = (p0.y > p3.y) ? p0.y + 30 : p3.y + 30;
				myBezier = new Bezier(p0, p1, p2, p3);

				//******************** サウンド処理(担当：野村) ********************
				SoundSpeaker SoundDevice = GetComponent<SoundSpeaker>();				//ダッシュ床オブジェクトに内包されているSoundSpeakerスクリプトを取得する
				SoundDevice.PlaySE((int)(CommonSound.SE_NAME.SE_JUMP), false);			//ダッシュ床用SEを再生する
			}
			if(roadJointIs["Wall"] || roadJointIs["Jump"] || roadJointIs["Parabola"]) {
				rigidbody.velocity = Vector3.zero;
			}
			return;
		} catch {
			roadJoint = null;
			//print("not road joint");
		}

	}
	
	private void OnTriggerExit(Collider collider) {
		if(collider.gameObject.CompareTag("Dash")) {
			//speed = speedDefault;
		}

		if(collider.gameObject.CompareTag("Bonus")) {
			jumpPower = jumpPowerDefault;
		}

		//print(collider.tag);
	}

	private void OffTheGroundLittle() {
		if(isOffBuilding) {
			anm.SetBool(anmJumpHash, true);
			isFly = true;
		}
	}

	public void UndoSpeed() {
		//speed = speedDefault;
	}

	public void UndoJumpPower() {
		jumpPower = jumpPowerDefault;
	}

	void TargetLock() {
		if(roadJoint.name.Contains("Road") || roadJoint.name.Contains("Parabola")) {
			// トランスフォームはクローンができないのでそのまま
			Transform t = roadJoint.transform;
			Vector3 lookAt = t.position;
			Vector3 copyLookAt = new Vector3(lookAt.x, lookAt.y, lookAt.z);
			lookAt.y = transform.position.y;
			if(roadJoint.name.Contains("Road")) {
				Vector3 offset = roadJoint.transform.right * offsetSide;
				lookAt.x += offset.x;
				lookAt.z += offset.z;
			}
			Vector3 forward = transform.forward;
			Vector3 targetDir = lookAt - transform.position;
			lookAt = (targetDir - (forward * targetDir.magnitude)) * 0.5f;
			lookAt += forward * targetDir.magnitude;
			lookAt += transform.position;
			t.position = lookAt;
			gameObject.transform.LookAt(t);
			// 同じ座標を参照いている為、元に戻してやらないと高さが初期プレイヤー高度と同じになる
			t.position = copyLookAt;
		} else if(roadJoint.name.Contains("Jump")) {
			transform.LookAt(roadJoint.transform);
		}
	}

	void EndRotate() {
		anm.SetBool(anmJumpHash, true);
		anm.SetBool(anmRotHash, false);
	}
	public void StartStumble() {
		anm.SetBool(anmStumbleHash, true);
	}
	public void EndStumble() {
		anm.SetBool(anmStumbleHash, false);
	}

	private void ToggleLandingAnimation() {
		anm.SetBool(anmLandingHash, !anm.GetBool(anmLandingHash));
	}

	private void UpdateRoadJointIs() {
		if(roadJoint) {
			roadJointIs["Road"] = roadJoint.name.Contains("Road");
			roadJointIs["Wall"] = roadJoint.name.Contains("Wall");
			roadJointIs["Jump"] = roadJoint.name.Contains("Jump");
			roadJointIs["Parabola"] = roadJoint.name.Contains("Parabola");
		}
	}
}
