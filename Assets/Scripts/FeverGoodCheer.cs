using UnityEngine;
using System.Collections;

public class FeverGoodCheer : MonoBehaviour {

	private SoundSpeaker ss;

	// Use this for initialization
	void Start () {
		ss = GetComponent<SoundSpeaker>();
		try {
			ss.PlaySE((int)CommonSound.SE_NAME.SE_CHEER2, false);
		} catch {
			print("fever good cheer error");
		}
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
