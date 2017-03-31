using UnityEngine;
using System.Collections;

public class switchScript : MonoBehaviour {

	public Transform actionCheck;
	private float actionRadius = 0.2f;
	public GameObject enableObject;
	public GameObject enableObject2;
	public Sprite onSprite;
	public Sprite offSprite;
	public LayerMask whatIsPlayer;
	public bool isOn;
	private SpriteRenderer srend;
	private bool actionRequested = false;
	private float actionDelayTime = 0.2f;
	private float timeSinceAction = 0.2f;

	void Start () {
		srend = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		actionRequested = (Input.GetButton ("Action") && timeSinceAction > actionDelayTime);
	}

	void FixedUpdate() {
		if (Physics2D.OverlapCircle (actionCheck.position, actionRadius, whatIsPlayer) && actionRequested) {
			timeSinceAction = 0.0f;
			isOn = !isOn;
			if (isOn) {
				srend.sprite = onSprite;
				enableObject.SetActive (true);
				if (enableObject2 != null) {
					enableObject2.SetActive (true);
				}

			} else {
				srend.sprite = offSprite;
				enableObject.SetActive (false);
				if (enableObject2 != null) {
					enableObject2.SetActive (false);
				}
			}
			audioScript.PlaySwitchAudio ();
		} else {
			timeSinceAction += Time.fixedDeltaTime;
		}
	}
}
