using UnityEngine;
using System.Collections;

public class RoadJoint : MonoBehaviour {

	public GameObject prevJoint;
	public GameObject nextJoint;

	[SerializeField]
	private GameObject _specialCamera;
	public GameObject specialCamera {
		get { return _specialCamera; }
		private set { _specialCamera = value; }
	}

	public int orderNumber;

	[SerializeField]
	private bool _landing;
	public bool landing {
		get { return _landing; }
		private set { _landing = value; }
	}
	[SerializeField]
	private float _landingWaitTime;
	public float landingWaitTime {
		get { return _landingWaitTime; }
		private set { _landingWaitTime = value; }
	}
	[SerializeField]
	private GameObject _landingEffect;
	public GameObject landingEffect {
		get { return _landingEffect; }
		private set { _landingEffect = value; }
	}
	[SerializeField]
	private GameObject _landingCamera;
	public GameObject landingCamera {
		get { return _landingCamera; }
		private set { _landingCamera = value; }
	}
	[SerializeField]
	private bool _NotCircle;
	public bool NotCircle {
		get { return _NotCircle; }
		private set { _NotCircle = value; }
	}

	void Awake() {
		char[] n = name.ToCharArray();
		orderNumber = int.Parse(("" + n[n.Length-2] + n[n.Length - 1]).ToString());
	}

	// Use this for initialization
	void Start () {
		foreach(RoadJoint rj in GameObject.FindObjectsOfType<RoadJoint>()) {
			if(rj.orderNumber == (this.orderNumber + 1)) {
				nextJoint = rj.gameObject;
				rj.transform.LookAt(nextJoint.transform);
			} else if(rj.orderNumber == (this.orderNumber - 1)) {
				prevJoint = rj.gameObject;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public Vector3 Dir() {
		return (nextJoint.transform.position - gameObject.transform.position).normalized;
	}


}
