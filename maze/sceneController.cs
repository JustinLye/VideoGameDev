using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class sceneController : MonoBehaviour {


	//method for tracking time: https://www.youtube.com/watch?v=w33cOjMT0fE

	private float elapsedTime = 60;
	private float countDown = 3;
	[SerializeField]
	private Text _startText;
	[SerializeField]
	private Text _gameTimer;
	[SerializeField]
	private GameObject _gameOverMenu;
	[SerializeField]
	private GameObject _victoryMenu;
	[SerializeField]
	private Text _victoryPrompt;
	[SerializeField]
	private GameObject _startMenu;
	private Color _fadeColor;
	public bool _startGame;
	public bool _frozen;
	private float timeTrack;
	private int timeBeepCount;
	private int beepCount;
	// Use this for initialization
	void Start () {
		_frozen = true;
		elapsedTime = 60;
		countDown = 3;
		beepCount = 0;
		timeBeepCount = 10;
		_startGame = false;
		_fadeColor = _startText.color;
		_gameOverMenu.SetActive (false);
		_victoryMenu.SetActive (false);
		_startMenu.SetActive (true);
		_startText.enabled = false;
		audioScript.PlayBackgroundAudio (false);
		_frozen = true;
		timeTrack = 0;
		Time.timeScale = 1;

	}
	
	// Update is called once per frame
	void Update () {
		if (!_frozen)
		if (!_startGame) {
			if ((countDown -= Time.deltaTime) > 0) {
				_startText.text = string.Format("Game starts in: {0}", countDown.ToString ("0"));
				timeTrack += Time.deltaTime;
				if (timeTrack >= beepCount) {
					audioScript.PlayToneBeepAudio ();
					beepCount++;
				}
			} else {
				audioScript.PlayBackgroundAudio (true);
				StartCoroutine (FadeStartText ());
				_gameTimer.gameObject.SetActive (true);
				_startGame = true;
				timeTrack = 0;
			}
		} else {
			elapsedTime -= Time.deltaTime;

			if (elapsedTime <= 0) {
				GameOver ();
			} else {
				_gameTimer.text = string.Format("Time Remaining: {0}", elapsedTime.ToString("0"));
				if (elapsedTime <= timeBeepCount) {
					audioScript.PlayLowTimeBeepAudio ();
					timeBeepCount--;
				}
			}
		}
	}

	public void FinishLineReached() {
		_frozen = true;
		audioScript.PlayVictoryAudio ();
		_victoryPrompt.text = string.Format("Congratulations! Your time was: {0} seconds", (60.0f - elapsedTime).ToString("0"));
		_victoryMenu.SetActive (true);
		audioScript.PlayBackgroundAudio (false);
		Time.timeScale = 0;
	}

	void GameOver() {
		_gameOverMenu.SetActive (true);
		_frozen = true;
		audioScript.PlayBackgroundAudio (false);
		audioScript.PlayGameOverAudio ();
		Time.timeScale = 0;

	}

	public void unfreeze() { 
		_startMenu.SetActive (false);
		_startText.enabled = true;
		_frozen = false;
	}

	private IEnumerator FadeStartText() {
		audioScript.PlayBikeHornAudio ();
		_startText.text = "GO!";
		for (float i = 1.0f; i > 0.0f; i -= 0.01f) {
			_fadeColor.a = i;
			_startText.color = _fadeColor;
			yield return null;
		}
		_startText.enabled = false;
		
	}

	public void ResetGame() {
		_victoryMenu.SetActive (false);
		_gameOverMenu.SetActive (false);
		_gameTimer.enabled = false;
		_frozen = true;
		SceneManager.LoadScene ("main");
		Time.timeScale = 1;
	}

	public void QuitGame() {
		Application.Quit ();
	}

}
