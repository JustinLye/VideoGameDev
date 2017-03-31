using UnityEngine;
using System.Collections;

public class doorController : MonoBehaviour {

	private Animator anim;
	public Transform entryPoint;
	public LayerMask whatIsCharacter;
	private bool isOpening;
	private float entryPointRadius = 0.4f;

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		isOpening = false;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void FixedUpdate() {
		if (Physics2D.OverlapCircle (entryPoint.position, entryPointRadius, whatIsCharacter) && !isOpening) {
			if (whatIsCharacter.Equals ("Player")) {
				if (playerController.PlayerIsVictorious ()) {
					isOpening = true;
					StartCoroutine (OpenDoor ());
				}
			} else {
				isOpening = true;
				StartCoroutine (OpenDoor ());
			}
		}

	}

	private IEnumerator OpenDoor() {
		anim.SetBool ("CharacterApproaching", true);
		audioScript.PlayDoorOpenAudio ();
		yield return new WaitForSeconds (1.5f);
		anim.SetBool ("CharacterApproaching", false);
		isOpening = false;
	}


}
