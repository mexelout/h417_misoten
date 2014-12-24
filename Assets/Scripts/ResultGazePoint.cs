using UnityEngine;
using System.Collections;

public class ResultGazePoint : MonoBehaviour {
	
	public GameObject target;	// オブジェクト
	public float radius = 1.0f;	// オブジェクトからカメラまでの距離(円運動の半径)
	public float angle = 0.0f;	// 角度

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		Vector3 Radius = this.transform.position - GameObject.Find("MainCamera").transform.position;
		Vector3 CameraPos = GameObject.Find("MainCamera").transform.position;

		Debug.Log(Mathf.PI);
		this.transform.position = new Vector3(CameraPos.x + Radius.x * Mathf.Cos(Mathf.PI / 180 * angle), this.transform.position.y, CameraPos.z + Radius.z * Mathf.Sin(Mathf.PI / 180 * angle));
		 
		angle += 1.0f;
		
		if(angle > 360)
		{
			angle = 1;
		}
	}
}
