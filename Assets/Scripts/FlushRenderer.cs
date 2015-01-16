using UnityEngine;
using System.Collections;

public class FlushRenderer : MonoBehaviour {

	[SerializeField]
	private float _fadeTime = 1;
	public float fadeTime {
		get { return _fadeTime; }
		private set { _fadeTime = value; }
	}

	private float currentTime;
	private float incOrDec = 1; // 増減

	// Use this for initialization
	void Start () {
		currentTime = 0;
	}
	
	// Update is called once per frame
	void Update () {
		try {
			Color c = renderer.material.color;
			c.a = currentTime / fadeTime;
			if(c.a > 1) {
				c.a = 1;
				incOrDec *= -1;
			} else if(c.a < 0) {
				c.a = 0;
				incOrDec *= -1;
			}
			renderer.material.color = c;
			currentTime += Time.deltaTime * incOrDec;
		} catch {
			print("error! Flush Renderer ");
		}
	}
}
