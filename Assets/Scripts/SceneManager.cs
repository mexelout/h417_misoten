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
				Invoke("RateSetScene", 0.25f);
			});
		}
	}

	private void FadeIn() {
		CameraFade.Create().StartAlphaFade(Color.white, true, 2f, 0.25f);
	}

	private void RateSetScene() {
		if(current == titleScene) {
			SetScene(gameScene);
			return;
		}
		if(current == tutorialScene) {
			SetScene(gameScene);
			return;
		}
		if(current == gameScene) {
			SetScene(resultScene);
			return;
		}
		if(current == resultScene) {
			SetScene(titleScene);
			return;
		}
	}

	public void SetScene(GameObject inst) {
		instScene = (GameObject)Instantiate(inst);
		current = inst;
	}

	public void ForceDestroyScene() {
		Object.Destroy(instScene);
		FadeIn();
	}
}
