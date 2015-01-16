using UnityEngine;
using System.Collections;

public class IntroCamera : MonoBehaviour {

	[SerializeField]
	private Vector3 _startPosition;
	public Vector3 startPosition {
		get { return _startPosition; }
		private set { _startPosition = value; }
	}

	[SerializeField]
	private Vector3 _endPosition;
	public Vector3 endPosition {
		get { return _endPosition; }
		private set { _endPosition = value; }
	}

	[SerializeField]
	private Vector3 _startRotate;
	public Vector3 startRotate {
		get { return _startRotate; }
		private set { _startRotate = value; }
	}

	[SerializeField]
	private Vector3 _endRotate;
	public Vector3 endRotate {
		get { return _endRotate; }
		private set { _endRotate = value; }
	}

	[SerializeField]
	private float _time;
	public float time {
		get { return _time; }
		private set { _time = value; }
	}

	private float elapsedTime;

	public enum Type {
		Linear,
		InExp,
		OutExp,
		InOutExp,
	}
	[SerializeField]
	private Type _type;
	public Type type {
		get { return _type; }
		private set { _type = value; }
	}

	public GameObject nextIntroCamera;

	// Use this for initialization
	void Start () {
		transform.position = startPosition;
		transform.rotation = Quaternion.Euler(startRotate);
	}
	
	// Update is called once per frame
	void Update () {
		elapsedTime += Time.deltaTime;
		if(elapsedTime < time) {
			Vector3 pvec = (endPosition - startPosition);
			Vector3 rvec = (endRotate - startRotate);
			float t = (elapsedTime / time);
			float e;
			switch(type) {
				case Type.Linear: // 線形
					pvec *= t;
					rvec *= t;
					break;
				case Type.InExp: // 順指数
					e = Mathf.Exp((t - 1) * 5);
					pvec *= e;
					rvec *= e;
					break;
				case Type.OutExp: // 逆指数
					e = (-Mathf.Exp(t * -5) + 1);
					pvec *= e;
					rvec *= e;
					break;
				case Type.InOutExp:
					// 未実装. http://gizma.com/easing/ ここ参照
					break;
			}
			transform.position = startPosition + pvec;
			transform.rotation = Quaternion.Euler(startRotate + rvec);
		} else {
			Destroy(gameObject);

			if(nextIntroCamera)
				Instantiate(nextIntroCamera);
			else {
				GameManager gm = GameObject.FindObjectOfType<GameManager>();
				if(gm)
					gm.startCountState();
			}
		}
	}

	// 破壊的メソッド、使用には十分注意すること
	public void ForceSetEndPosition(Vector3 p) {
		endPosition = p;
	}

	public void ForceSetEndRotate(Vector3 r) {
		endRotate = r;
	}
}
