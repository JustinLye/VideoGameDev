using UnityEngine;
using System.Collections;

public class treasureScript : MonoBehaviour {
	private bool isTripped = false;
	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "Player" && !isTripped) {
			isTripped = true;
			audioScript.PlayCashRegisterAudio ();		
			UIScript.incrementPoints (5.0f);
			playerController.incrementTreasureCount ();
			Destroy (this.gameObject);
		}
	}

}
