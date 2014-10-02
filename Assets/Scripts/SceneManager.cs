using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public GameObject titleScene;
	public GameObject gameScene;
	public GameObject resultScene;

	private GameObject current;
	private GameObject instScene;

	// Use this for initialization
	void Start () {
		instScene = (GameObject)Instantiate(titleScene);
		current = titleScene;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextScene() {
		Object.Destroy(instScene);
		if(current == titleScene) {
			instScene = (GameObject)Instantiate(gameScene);
			current = gameScene;
			return;
		}
		if(current == gameScene) {
			instScene = (GameObject)Instantiate(resultScene);
			current = resultScene;
			return;
		}
		if(current == resultScene) {
			instScene = (GameObject)Instantiate(titleScene);
			current = titleScene;
			return;
		}
	}
}
