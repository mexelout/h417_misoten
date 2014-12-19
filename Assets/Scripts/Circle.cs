using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {

	public Vector3 targetPosition;
	public GameObject player;
	private Camera playerCamera;

	private GameObject _dynamicCircle;
	public GameObject dynamicCircle {
		get { return _dynamicCircle; }
	}
	private GameObject _staticCircle;
	public GameObject staticCircle {
		get { return _staticCircle; }
	}
	private bool alreadyDown = false;

	// Use this for initialization
	void Start () {
		PlayerCamera pc = GameObject.FindObjectOfType<PlayerCamera>();
		if(pc) {
			playerCamera = pc.gameObject.GetComponent<Camera>();
		}
		_dynamicCircle = transform.FindChild("DynamicCircle").gameObject;
		_staticCircle = transform.FindChild("StaticCircle").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if(playerCamera) {

			Vector3 pos = playerCamera.WorldToViewportPoint(targetPosition);
			// UI表示をしているカメラのサイズが縦の半分のサイズであり、サイズ5であれば
			// 計算値は 9 / 5 = 1.8
			// 算出された値のマイナス値あ左下となる
			// pos.x, pos.yは0〜1のビューポート座標なので、その数値に合うように計算し直して値を設定してあげる必要がある
			pos.x = -8.88888888888889f + 17.77777777777778f * pos.x;
			pos.y = -5 + 10f * pos.y;
			transform.position = pos;
		}
		if(_dynamicCircle) {
			float dis = (player.transform.position - targetPosition).magnitude;
			dis = (dis / 60f) * 7;
			_dynamicCircle.transform.localScale = new Vector3(3 + dis, 3 + dis, 1);
		}
	}

	public bool IfProcess(Player player) {
		if(alreadyDown) return false;

		float dif = dynamicCircle.transform.localScale.magnitude - staticCircle.transform.localScale.magnitude;
		// 3以下くらいがちょうど重なったと思えるサイズ
		if(Input.GetButtonDown("Jump")) {
			ScoreManager sm = FindObjectOfType<ScoreManager>();
			if(3 < dif && dif < 6) {
				if(sm) sm.PlusNowScore(250);
				return true;
			} else if(dif <= 3) {
				player.speed *= 1.5f;
				if(player.speed > 50) player.speed = 50;

				if(sm) sm.PlusNowScore(500);
				return true;
			}
		}
		return false;
	}
}
