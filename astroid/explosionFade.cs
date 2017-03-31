using UnityEngine;
using System.Collections;

public class explosionFade : MonoBehaviour {

	/* modified smokeFade from other weekly project to play an explosion sound*/

	public float fadeSpeed = 0.0f; //Passed to the smoothdamp function
	public float fadeTime = 0.5f; //Length of time to fade opacity of the sprite

	private SpriteRenderer rend; //Variable to hold sprite component
	private AudioSource audio; //variable to hold audio clip for explosion
	private const float zeroTreshold = 0.0001f; //Once the alpha of the sprite reaches this level it will be destroyed

	void Start() {
		rend = GetComponent<SpriteRenderer>(); //Get the sprite component
		rend.color = new Color(1.0f, 1.0f, 1.0f, 1.0f);  //Ensure the alpha is full (255)
		audio = GetComponent<AudioSource>();
		if (audio != null)
			audio.Play();
	}

	void Update() {
		// If the alpha of the sprite is less than the treshold it will be destroyed, else fade the color
		if (rend.color.a > zeroTreshold) {
			float fade = Mathf.SmoothDamp(rend.color.a, 0.0f, ref fadeSpeed, fadeTime); // Get then new alpha for the sprite
			rend.color = new Color(1.0f, 1.0f, 1.0f, fade); //Set the alpha of the sprite to the new calculated value held in the fade variable
		} else {
			//If the puff of smoke is no longer visible then it can be removed from the game
			Destroy(this.gameObject);
		}

	}
}
