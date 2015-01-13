using UnityEngine;
using System.Collections;

public class LimitDestroy : MonoBehaviour {

	[SerializeField]
	private float _limitCount;
	public float limitCount {
		get { return _limitCount; }
		private set { _limitCount = value; }
	}

	// Use this for initialization
	void Start () {
		Invoke("Bye", limitCount);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void Bye() {
		Destroy(gameObject);
	}
}
