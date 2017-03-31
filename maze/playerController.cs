using UnityEngine;
using System.Collections;


[RequireComponent(typeof(playerMovement))]
public class playerController : MonoBehaviour {

	[SerializeField]
	private float speed = 5.0f;
	[SerializeField]
	private sceneController sceneContrl;
	private playerMovement movement;

	// Use this for initialization
	void Start () {
		movement = GetComponent<playerMovement> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (sceneContrl._startGame) {
			float xMov = Input.GetAxis ("Horizontal");
			float zMov = Input.GetAxis ("Vertical");
			Vector3 moveHoriz = transform.right * xMov;
			Vector3 moveVert = transform.forward * zMov;
			Vector3 velocity = (moveHoriz + moveVert).normalized * speed;
			movement.Move (velocity);
		}
	}

	void OnTriggerEnter(Collider other) {
		if (other.tag == "finish line") {
			sceneContrl.FinishLineReached ();
		}
	}
}
