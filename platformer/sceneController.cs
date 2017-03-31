using UnityEngine;
using System.Collections;

public class sceneController : MonoBehaviour {



	void Start () {

		audioScript.PlayBackgroundAudio (true);
		Random.seed = System.DateTime.Now.Millisecond * System.DateTime.Now.Second;
		Time.timeScale = 1;

	}

	private IEnumerator Intro() {
		Time.timeScale = 0;
		yield return StartCoroutine ("StartTheMusic", null);
		Time.timeScale = 1;
	}

	private IEnumerator StartTheMusic() {
		Debug.Log (audioScript.IntroPlayTime ());
		yield return new WaitForSeconds (10.0f);
		audioScript.PlayBackgroundAudio (true);
	}






}
