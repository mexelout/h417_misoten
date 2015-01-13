using UnityEngine;
using System.Collections;

public class AutoRunner : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Time.timeScale = 2.0f;
	}
	
	// Update is called once per frame
	void Update () {
		GameManager gm = GameObject.FindObjectOfType<GameManager>();
		if(gm) {
			if(gm.gameState == GameManager.GameState.Play) {
				Time.timeScale = 1;
				Animator a = GetComponent<Animator>();
				a.SetFloat("Speed", 25 * Time.deltaTime);
				transform.Translate(0, 0, 25 * Time.deltaTime);
			}
		}
	}
}
