using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public bool isSpecialMode;
	private Vector3 lookPosition;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Player player = GameManager.FindObjectOfType<Player>();
		Vector3 cameraPos = gameObject.transform.position;

		// 直線ジャンプまたは放物線ジャンプの時にtrue
		bool jumpOrParabola = player.roadJoint != null && (player.roadJoint.name.Contains("Jump") || player.roadJoint.name.Contains("Parabola"));

		if(player.roadJoint == null || player.roadJoint.specialCamera == null) { 
			Vector3 back = player.transform.forward * -1;
			if(player.isFly) {
				back.y = 0;
				back = back.normalized * 3.5f;
				back.y += 1.5f;
			} else {
				back *= 2.5f;
				back += player.transform.up * 1.5f;
			}
			Vector3 purposePos = player.gameObject.transform.position + back;

			if(jumpOrParabola) {
				purposePos -= (player.roadJoint.transform.position - player.transform.position).normalized * 3;
				purposePos -= back.normalized * 3;
				purposePos += player.transform.right;
			}

			Vector3 diffVec = purposePos - cameraPos;
			gameObject.transform.position = cameraPos + diffVec / 5;
		} else {
			Vector3 diffVec = player.roadJoint.specialCamera.transform.position - cameraPos;
			cameraPos += (diffVec * 0.2f);
			gameObject.transform.position = cameraPos;
		}

		if(jumpOrParabola && player.roadJoint.specialCamera == null) {
			Vector3 purposePos = player.roadJoint.transform.position;
			Vector3 diffVec = purposePos - lookPosition;
			lookPosition = lookPosition + diffVec / 5;
		} else {
			Vector3 purposePos = player.transform.position + player.transform.up * 1.25f;
			Vector3 diffVec = purposePos - lookPosition;
			lookPosition = lookPosition + diffVec / 5;
		}
		gameObject.transform.LookAt(lookPosition);
	}
}
