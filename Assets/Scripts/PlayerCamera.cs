using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	public bool isSpecialMode;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Player player = GameManager.FindObjectOfType<Player>();
		Vector3 camera_pos = gameObject.transform.position;

		if(player.roadJoint == null || player.roadJoint.specialCamera == null) { 
			Vector3 back = player.transform.forward * -1;
			if(player.isFly) {
				back.y = 0;
				back = back.normalized * 4.5f;
				back.y += 1.5f;
			} else {
				back *= 3.5f;
				back += player.transform.up * 1.5f;
			}
			Vector3 purpose_pos = player.gameObject.transform.position + back;
			Vector3 diff_vec = purpose_pos - camera_pos;
			gameObject.transform.position = camera_pos + diff_vec / 5;
		} else {
			Vector3 diff_vec = player.roadJoint.specialCamera.transform.position - camera_pos;
			camera_pos += (diff_vec * 0.2f);
			gameObject.transform.position = camera_pos;
		}
		gameObject.transform.LookAt(player.transform.position + player.transform.up * 1.25f);
	}
}
