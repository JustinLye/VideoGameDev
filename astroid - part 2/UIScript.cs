using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIScript : MonoBehaviour {

	public Text scoreText; //displays players current score on the canvas
	public Text finalScoreText; //displays players final score when the game is over
	public Image life_0, life_1, life_2, life_3, life_4; //images that will indicate a players life has been lost
	public GameObject panel; //panel that will contain the players final score and play again or quit buttons
	private static int score = 0; //players score
	private int lifes = 5; //total lives the player starts with
	private Color _lifeLossColor = new Color(1.0f, 1.0f, 1.0f, 255); //when the player loses a life the color of the cooresponding life_1 image will become 100% opaque
	private AudioSource audio; //sound played when the player loses a life.
	public AudioSource gameOverAudio; //sound played when the game is over

	void Start() {
		audio = GetComponent<AudioSource>();// get audio source of life lost sound
		score = 0; //initialize score to 0
		lifes = 5; //initialize player lives to 5
	}

	public int GetScore() { return score; } //returns current score information. purpose is to provided other classes info.
	//calling this function will increment the players score 10 points.
	public void incrementScore() {
		scoreText.text = string.Format("Score: {0}", score+=10);
	}

	/* calling this function will:
	   play a life lost sound, decrement the total player lives, and make the corresponding life image 100% opaque
	   If the player has run out of lives the game will end */
	public void decrementLife() {
		if (lifes > 0) { //check if the player has remaining lives
			if (audio != null) //check if a life lost audio sound is present
				audio.Play(); //play the life lost sound if it is found
			lifes--; //decrement the player lives
			switch (lifes) { //make the corresponding life image 100% opaque
				case 4:
					life_4.color = _lifeLossColor;
					break;
				case 3:
					life_3.color = _lifeLossColor;
					break;
				case 2:
					life_2.color = _lifeLossColor;
					break;
				case 1:
					life_1.color = _lifeLossColor;
					break;
				case 0:
					life_0.color = _lifeLossColor;
					gameOverMenu(); //end the game if all lives have been lost
					break;
			}
		}
	}

	/* This function will present the player with a game over menu
	 * The final score will be displayed and the player will be
	 * given the option to play again or quit*/
	public void gameOverMenu() {
		Time.timeScale = 0; //method used to pause the game. I got the idea to pause the game in this way from the first few minutes of this video https://www.youtube.com/watch?v=7dCtacifmU8
		finalScoreText.text = string.Format("Final Score: {0}", score); //display players final score
		if (gameOverAudio != null) //check if the game over audio is present
			gameOverAudio.Play(); //play game over audio if it was found
		panel.transform.gameObject.SetActive(true); //activate the panel representing the game over menu
	}

	//calling this function will quit the game
	public void quitGame() {
		Application.Quit();
	}
	
	//calling this function will start a new game
	public void resetGame() {
		SceneManager.LoadScene("main"); //looked around on the web to find a way to reload the scene. the method I found Application.LoadLevel(Application.loadLevel) had been deprecated and the unity docs specified using scene manager instead
		Time.timeScale = 1; //method used ot unpause the game. I got the idea to unpause the game in this way from the first few minutes of this video https://www.youtube.com/watch?v=7dCtacifmU8
	}

}
