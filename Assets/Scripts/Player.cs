﻿using UnityEngine;
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
	public float jumpPower;
	public float jumpPowerDefault = 100;

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

		transform.Translate(0, 0, vertical);

		anm.SetFloat(anmSpeedHash, vertical);

		if(Input.GetAxis("Jump") > 0 && anm.GetBool(anmJumpHash) == false) {
			//anm.SetBool(anmJumpHash, true);
			//rigidbody.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
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

		if(roadJoint.name.Contains("Jump")) {
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
			if(rj.laneNumber == roadNumber)
				roadJoint = rj.nextJoint.GetComponent<RoadJoint>();
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
		if(roadJoint.name.Contains("Road")) {
			// トランスフォームはクローンができないのでそのまま
			Transform t = roadJoint.transform;
			Vector3 lockAt = t.position;
			float y = lockAt.y;
			lockAt.y = transform.position.y;
			t.position = lockAt;
			gameObject.transform.LookAt(t);
			// 同じ座標を参照いている為、元に戻してやらないと高さが初期プレイヤー高度と同じになる
			lockAt.y = y;
			t.position = lockAt;
		} else {
			transform.LookAt(roadJoint.transform);
		}


	}
}
