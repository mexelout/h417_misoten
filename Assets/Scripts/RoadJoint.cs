using UnityEngine;
using System.Collections;

public class RoadJoint : MonoBehaviour {

	public GameObject prevJoint;

	public GameObject nextJoint;

	public Hashtable sideJoint;

	[SerializeField]
	private GameObject _specialCamera;
	public GameObject specialCamera {
		get { return _specialCamera; }
		private set { _specialCamera = value; }
	}

	public int orderNumber;
	public int laneNumber;

	void Awake() {
		char[] n = name.ToCharArray();
		orderNumber = int.Parse(("" + n[n.Length-3] + n[n.Length - 2]).ToString());
		laneNumber = int.Parse(n[n.Length - 1].ToString());
	}

	// Use this for initialization
	void Start () {
		sideJoint = new Hashtable();
		foreach(RoadJoint rj in GameObject.FindObjectsOfType<RoadJoint>()) {
			if(rj.laneNumber == this.laneNumber) {
				if(rj.orderNumber == (this.orderNumber + 1)) {
					nextJoint = rj.gameObject;
				} else if(rj.orderNumber == (this.orderNumber - 1)) {
					prevJoint = rj.gameObject;
				}
			}
			if(rj.orderNumber == this.orderNumber) {
				if(rj.laneNumber == this.laneNumber - 1) {
					sideJoint["left"] = rj;
				} else if(rj.laneNumber == this.laneNumber + 1) {
					sideJoint["right"] = rj;
				}
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 Dir() {
		return (nextJoint.transform.position - gameObject.transform.position).normalized;
	}

	public bool IsCross(Vector3 prev, Vector3 now) {
		//Vector2 v = new Vector2();

		return true;
	}

	public Vector3 CrossPosition(Vector3 prev, Vector3 now) {
		// 交点求めるをwww(使うとは言っていない)
		// 公式
		// 直線A 原点x1,y1 傾きm1
		// 直線B 原点x2,y2 傾きm2
		// x = ( m1 × x1 - m2 × x2 + y2 - y1 ) / (m1 - m2)
		// y = m1 × ( x - x1 ) + y1
		float x, y, x1, x2, y1, y2, m1, m2;
		x1 = nextJoint.transform.position.x - gameObject.transform.position.x;
		x2 = prev.x - now.x;
		y1 = nextJoint.transform.position.z - gameObject.transform.position.z;
		y2 = prev.z - now.z;
		m1 = y1 / x1;
		m2 = y2 / x2;
		x = (m1 * x1 - m2 * x2 + y2 - y1) / (m1 - m2);
		y = m1 * (x - x1) + y1;
		return new Vector3(x, 0, y);
	}
}
