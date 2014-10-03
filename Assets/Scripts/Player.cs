using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour {

	public float speed = 30;
	private GameManager gm;

	// Use this for initialization
	void Start () {
		gm = GameObject.FindObjectOfType<GameManager>();
	}
	
	// Update is called once per frame
	void Update () {
		float vertical = speed * Time.deltaTime;
		//Input.GetAxis("Vertical") * speed * Time.deltaTime;
		//float horizontal = Input.GetAxis("Horizontal") * speed * Time.deltaTime;

		if(gm.GetStartCount() > 0) vertical *= 0;

		transform.Translate(0, 0, vertical);

		Animator anm = GetComponent<Animator>();
		anm.SetFloat(Animator.StringToHash("Speed"), vertical);

		if(transform.position.z > 1000) {
			transform.position = new Vector3(transform.position.x, transform.position.y, 999);
			transform.Rotate(Vector3.up, 180.0f);
		}
		if(transform.position.z < -30) {
			transform.position = new Vector3(transform.position.x, transform.position.y, -29);
			transform.Rotate(Vector3.up, 0.0f);
		}
	}

	private void OnCollisionEnter(Collision collision) {
		if(collision.gameObject.tag == "jumparea") {
		}
	}
}
