using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIScript : MonoBehaviour {

	public enum MenuScreen {CLOSEALL, MAIN, GAMEOVER, INSTRUCTIONS, VICTORY}

	public Text timeText;
	public Text scoreText;
	public static float points = 0.0f;
	public Image life1;
	public Image life2;
	public Image life3;
	public static bool updatePoints;
	public GameObject _mainMenu;
	public GameObject _gameOverMenu;
	public GameObject _contolsMenu;
	public GameObject _victoryMenu;

	public Text _finalScoreGameOver;
	public Text _finalScoreVictory;

	void Start () {
		updatePoints = false;
		pullUpMenu (MenuScreen.CLOSEALL);
	}
	
	// Update is called once per frame
	void Update () {
		float gameTime = Time.timeSinceLevelLoad;
		if (gameTime < 60.0f) {
			timeText.text = string.Format("{0} (s)", Mathf.Round(gameTime));
		} else {
			timeText.text = string.Format("{0} : {1}", Mathf.FloorToInt(gameTime / 60.0f), Mathf.Round(gameTime % 60.0f));

		}
		if (updatePoints) {
			scoreText.text = string.Format ("{0}", points);
			updatePoints = false;
		}
	}

	public static void incrementPoints(float value) {
		points += value;
		updatePoints = true;
	}

	public void MainMenu() {
		audioScript.PlayBackgroundAudio (false);
		pullUpMenu (MenuScreen.MAIN);
		Time.timeScale = 0;
	}

	public void InstructionMenu() {
		pullUpMenu (MenuScreen.INSTRUCTIONS);
		Time.timeScale = 0;
	}
		
	public void GameOverMenu() {
		audioScript.PlayBackgroundAudio (false);
		audioScript.PlayGameOverAudio ();
		_finalScoreGameOver.text = string.Format ("{0}", points);
		pullUpMenu (MenuScreen.GAMEOVER);
		Time.timeScale = 0;

	}

	public void VictoryMenu() {
		audioScript.PlayBackgroundAudio (false);
		audioScript.PlayVictoryAudio ();		
		_finalScoreVictory.text = string.Format ("{0}", points);
		pullUpMenu (MenuScreen.VICTORY);
		Time.timeScale = 0;
	}


	public void RestartGame() {
		points = 0;
		Time.timeScale = 1;
		SceneManager.LoadScene ("main");
	}

	public void Quit() {
		Application.Quit ();
	}

	public void ResumeGame() {
		pullUpMenu (MenuScreen.CLOSEALL);
		audioScript.PlayBackgroundAudio (true);
		Time.timeScale = 1;

	}

	private void pullUpMenu(MenuScreen _menu) {
		switch (_menu) {
		case MenuScreen.CLOSEALL:
			_gameOverMenu.transform.gameObject.SetActive (false);
			_mainMenu.transform.gameObject.SetActive (false);
			_contolsMenu.transform.gameObject.SetActive (false);
			_victoryMenu.transform.gameObject.SetActive (false);
			break;
		case MenuScreen.GAMEOVER:
			_gameOverMenu.transform.gameObject.SetActive (true);
			_mainMenu.transform.gameObject.SetActive (false);
			_contolsMenu.transform.gameObject.SetActive (false);
			_victoryMenu.transform.gameObject.SetActive (false);
			break;
		case MenuScreen.INSTRUCTIONS:
			_gameOverMenu.transform.gameObject.SetActive (false);
			_mainMenu.transform.gameObject.SetActive (false);
			_contolsMenu.transform.gameObject.SetActive (true);
			_victoryMenu.transform.gameObject.SetActive (false);
			break;
		case MenuScreen.MAIN:
			_gameOverMenu.transform.gameObject.SetActive (false);
			_mainMenu.transform.gameObject.SetActive (true);
			_contolsMenu.transform.gameObject.SetActive (false);
			_victoryMenu.transform.gameObject.SetActive (false);
			break;
		case MenuScreen.VICTORY:
			_gameOverMenu.transform.gameObject.SetActive (false);
			_mainMenu.transform.gameObject.SetActive (false);
			_contolsMenu.transform.gameObject.SetActive (false);
			_victoryMenu.transform.gameObject.SetActive (true);
			break;
		}
	}

}
