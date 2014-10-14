using UnityEngine;
using System.Collections;

public class PlayerCamera : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Player player = GameManager.FindObjectOfType<Player>();
		Vector3 camera_pos = gameObject.transform.position;
		Vector3 back = player.transform.forward * -1;
		if(player.isFly) {
			back.y = 0;
			back = back.normalized * 4.5f;
			back.y += 2;
		} else {
			back *= 2.5f;
			back += player.transform.up * 2;
		}
		Vector3 purpose_pos = player.gameObject.transform.position + back;
		Vector3 diff_vec = purpose_pos - camera_pos;
		gameObject.transform.position = camera_pos + diff_vec / 5;
		gameObject.transform.LookAt(player.transform.position + player.transform.up * 1.25f);
	}
}
