using UnityEngine;
using System.Collections;

public class Circle : MonoBehaviour {

	public Vector3 targetPosition;
	public GameObject player;
	private Camera playerCamera;
	private GameObject dynamicCircle;

	// Use this for initialization
	void Start () {
		PlayerCamera pc = GameObject.FindObjectOfType<PlayerCamera>();
		if(pc) {
			playerCamera = pc.gameObject.GetComponent<Camera>();
		}
		dynamicCircle = transform.FindChild("DynamicCircle").gameObject;
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
		if(dynamicCircle) {
			float dis = (player.transform.position - targetPosition).magnitude;
			dis = (dis / 60f) * 7;
			dynamicCircle.transform.localScale = new Vector3(3 + dis, 3 + dis, 1);
		}
	}
}
