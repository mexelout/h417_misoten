using UnityEngine;
using System.Collections;

public class DebugManager : MonoBehaviour {


	[SerializeField]
	private int _orderNum;
	public int orderNum {
		get { return _orderNum; }
		private set { _orderNum = value; }
	}

	private int nowOrder = 0;

	private GameObject forceScene;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if(orderNum > nowOrder) {
			nowOrder++;
			Player p = FindObjectOfType<Player>();
			p.transform.position = p.roadJoint.transform.position;
			p.rigidbody.velocity = new Vector3(0, 0, 0);
		}

		var sm = GameObject.FindObjectOfType<SceneManager>();
		if(sm) {
			if(Input.GetKeyDown(KeyCode.Alpha1)) {forceScene = sm.titleScene; sm.ForceDestroyScene(); Invoke("ForceNextScene", 0.25f); return;}
			if(Input.GetKeyDown(KeyCode.Alpha2)) {forceScene = sm.tutorialScene; sm.ForceDestroyScene(); Invoke("ForceNextScene", 0.25f); return;}
			if(Input.GetKeyDown(KeyCode.Alpha3)) {forceScene = sm.gameScene; sm.ForceDestroyScene(); Invoke("ForceNextScene", 0.25f); return;}
			if(Input.GetKeyDown(KeyCode.Alpha4)) {forceScene = sm.resultScene; sm.ForceDestroyScene(); Invoke("ForceNextScene", 0.25f); return;}
		}
	}

	private void ForceNextScene() {
		var sm = GameObject.FindObjectOfType<SceneManager>();
		sm.SetScene(forceScene);
	}
}
