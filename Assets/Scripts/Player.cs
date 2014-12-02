using UnityEngine;
using System.Collections;

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
	public float jumpPower;
	public float jumpPowerDefault = 100;

	// BoundsJoint関連
	public Vector3 p = new Vector3(0.0f,0.0f,0.0f);
	public Vector3 p0 =new  Vector3(0.0f,0.0f,0.0f);
	public Vector3 p1 =new  Vector3(0.0f,0.0f,0.0f);
	public Vector3 p2 =new  Vector3(0.0f,0.0f,0.0f);
	public Vector3 p3 =new  Vector3(0.0f,0.0f,0.0f);

	public Bezier myBezier;
	public float t = 0f;


	// フィニッシュの処理をここに書いているのでここでカウント(ゲームマネージャに後で移行)
	public int frameCount;
	private bool isPlay;

	// Next RoadJoint
	[SerializeField]
	private RoadJoint _roadJoint;
	public RoadJoint roadJoint {
		get { return _roadJoint; }
		private set { _roadJoint = value; }
	}

	void Start() {
		gm = GameObject.FindObjectOfType<GameManager>();
		anm = GetComponent<Animator>();
		anmSpeedHash = Animator.StringToHash("Speed");
		anmJumpHash = Animator.StringToHash("Jump");
		anmRotHash = Animator.StringToHash("Rotate");
		anmStumbleHash = Animator.StringToHash("Stumble");
		speed = speedDefault;
		jumpPower = jumpPowerDefault;
		frameCount = 0;
		isPlay = true;
		isFly = false;


	}

	void Update() {
		if(!roadJoint) {
			anm.SetFloat(anmSpeedHash, 0);
			return;
		}

		float vertical = speed * Time.deltaTime;
		float horizontal = Input.GetAxis("Horizontal");

		if(gm != null && gm.GetStartCount() > 0)
			vertical *= 0;

		if(roadJoint.name.Contains("Parabola")) {
			Vector3 vec = myBezier.GetPointAtTime(t);
//			Vector3 dis = roadJoint.transform.position - roadJoint.prevJoint.transform.position;
//			vec.x = roadJoint.prevJoint.transform.position.x + dis.x * t;
//			vec.z = roadJoint.prevJoint.transform.position.z + dis.z * t;
			transform.position = vec;
			t += 0.005f;
			if(t > 1f)
				t = 0f;
		} else {
			transform.Translate(0, 0, vertical);
		}

		if(anm.GetBool(anmStumbleHash) == false)
		{
			anm.SetFloat(anmSpeedHash, vertical);
		}


		if(Input.GetAxis("Jump") > 0) {
			if(anm.GetBool(anmRotHash) == false && isFly == false && roadJoint.name.Contains("Road")) {
				anm.SetBool(anmRotHash, true);
				rigidbody.AddForce(Vector3.up * jumpPower / 10, ForceMode.Impulse);
				isFly = true;
			}
		}

		if(horizontal < 0 && !isChangeRoadNumber) {
			roadNumber -= (roadNumber > 0) ? 1 : 0;
			RoadJoint rj = roadJoint.sideJoint["left"] as RoadJoint;
			if(rj) {
				roadJoint = rj.GetComponent<RoadJoint>();
			}
			isChangeRoadNumber = true;
		} else if(horizontal > 0 && !isChangeRoadNumber) {
			roadNumber += (roadNumber < 2) ? 1 : 0;
			RoadJoint rj = roadJoint.sideJoint["right"] as RoadJoint;
			if(rj != null) {
				roadJoint = rj.GetComponent<RoadJoint>();
			}
			isChangeRoadNumber = true;
		} else if(horizontal == 0) {
			isChangeRoadNumber = false;
		}

		if(roadJoint.name.Contains("Jump") || roadJoint.name.Contains("Parabola")) {
			rigidbody.useGravity = false;
			anm.SetBool(anmJumpHash, true);

		} else {
			rigidbody.useGravity = true;
		}

		TargetLock();

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
		//Invoke("OffTheGroundLittle", 0.4f);
		isOffBuilding = true;
	}

	private void OnTriggerEnter(Collider collider) 
	{
		try {
			SpecialFloor sf = collider.gameObject.GetComponent<SpecialFloor>();
			sf.Execute(this);
			return;
		} catch {
			//print("not special floor");
		}
		try {
			RoadJoint rj = collider.gameObject.GetComponent<RoadJoint>();
			if(rj.laneNumber == roadNumber){
				roadJoint = rj.nextJoint.GetComponent<RoadJoint>();
			}
			if(roadJoint.name.Contains("Parabola")) {
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
		speed = speedDefault;
	}

	void TargetLock() {
		if(roadJoint.name.Contains("Road") || roadJoint.name.Contains("Parabola")) {
			// トランスフォームはクローンができないのでそのまま
			Transform t = roadJoint.transform;
			Vector3 lookAt = t.position;
			Vector3 copyLookAt = new Vector3(lookAt.x, lookAt.y, lookAt.z);
			lookAt.y = transform.position.y;
			Vector3 forward = transform.forward;
			Vector3 targetDir = lookAt - transform.position;
			lookAt = (targetDir - (forward * targetDir.magnitude)) * 0.1f;
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
		anm.SetBool(anmRotHash, false);
	}
	public void StartStumble() {
		anm.SetBool(anmStumbleHash, true);
	}
	public void EndStumble() {
		anm.SetBool(anmStumbleHash, false);
	}
}
