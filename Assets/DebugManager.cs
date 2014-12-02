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
	}
}
