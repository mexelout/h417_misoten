using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

	public GameObject titleScene;
	public GameObject tutorialScene;
	public GameObject gameScene;
	public GameObject resultScene;

	private GameObject current;
	private GameObject instScene;

	// Use this for initialization
	void Start () {
		instScene = (GameObject)Instantiate(titleScene);
		current = titleScene;
		FadeIn();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextScene() {
		if(FindObjectOfType<CameraFade>() == null) {
			CameraFade.Create().StartAlphaFade(Color.white, false, 2f, 0f, () => {
				Object.Destroy(instScene);
				FadeIn();
				if(current == titleScene) {
					//instScene = (GameObject)Instantiate(tutorialScene);
					//current = tutorialScene;
					instScene = Instantiate(gameScene) as GameObject;
					current = gameScene;
					return;
				}
				if(current == tutorialScene) {
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
			});
		}
	}

	private void FadeIn() {
		CameraFade.Create().StartAlphaFade(Color.white, true, 2f);
	}
}
