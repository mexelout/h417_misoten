﻿using UnityEngine;
using System.Collections;

public class EvaluationString : MonoBehaviour {

	private const float amount = 0.02f;
	private Color alpha = new Color(0, 0, 0, amount);

	void Start() {
		renderer.material.color = new Color(1, 1, 1, 0);
	}

	// Update is called once per frame
	void Update () {
		renderer.material.color += alpha;
		if(renderer.material.color.a >= 1) {
			renderer.material.color = new Color(1, 1, 1, 1);
			alpha = new Color(0, 0, 0, -amount);
		}
		if(renderer.material.color.a <= 0.0f) {
			Destroy(gameObject);
		}
	}
}
