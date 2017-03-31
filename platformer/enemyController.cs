using UnityEngine;
using System.Collections;

public class enemyController : MonoBehaviour {

	private Animator anim;
	public float maxMoveSpeed = 10.0f;
	public float minMoveSpeed = 8.0f;
	private float currentMoveSpeed = 0.0f;
	public float maxDirectionTime = 2.0f;
	public float minDirectionTime = 1.0f;
	private float currentDirectionTime = 0.0f;
	private float timeSinceLastTurn = 0.0f;
	private Rigidbody2D rigid;
	public Transform hitCheck;
	public LayerMask whatIsPlayer;
	public GameObject explosionPrefab;
	private float hitCheckRadius = 0.2f;
	public bool isDead = false;
	private bool isPassingThroughDoor = false;
	private doorTransitionController doorController;
	public Collider2D attackCollider;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
		anim = GetComponent<Animator> ();
		doorController = GetComponent<doorTransitionController> ();
		currentDirectionTime = Random.Range (minDirectionTime, maxDirectionTime);
		currentMoveSpeed = Random.Range (minMoveSpeed, maxMoveSpeed);
	}

	void Update() {
		if (!isDead) {
			if (Physics2D.OverlapCircle (hitCheck.position, hitCheckRadius, whatIsPlayer)) {
				attackCollider.isTrigger = false;
				attackCollider.gameObject.SetActive (false);
				hitCheck.gameObject.layer = 0;
				Die ();
				UIScript.incrementPoints (10.0f);
			}
		}
	}

	void FixedUpdate() {
		if (!isDead) {
			if (timeSinceLastTurn >= currentDirectionTime) {
				flip ();
				timeSinceLastTurn = 0.0f;
				currentDirectionTime = Random.Range (minDirectionTime, maxDirectionTime);
				currentMoveSpeed = Random.Range (minMoveSpeed, maxMoveSpeed);
			} else {
				timeSinceLastTurn += Time.fixedDeltaTime;
				rigid.AddForce (new Vector2 (currentMoveSpeed * transform.localScale.x, 0.0f));

			}
		}
	}

	void flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "open door" && !isPassingThroughDoor) {
			isPassingThroughDoor = true;
			rigid.isKinematic = true;
			StartCoroutine ("GoThroughDoor");
		} else if (other.tag == "enemy boundry") {
			rigid.isKinematic = true;
			flip ();
			timeSinceLastTurn = 0.0f;
			rigid.isKinematic = false;
		}
	}

	public void Die() {
		if (!isDead) {
			
			isDead = true;
			explosionPrefab.transform.position = hitCheck.transform.position;
			Instantiate (explosionPrefab);

			StartCoroutine (AnimateDeathAndDDestroy ());
		}

	}

	private IEnumerator AnimateDeathAndDDestroy() {
		anim.SetBool ("Dead", true);
		yield return new WaitForSeconds (1.5f);
		anim.speed = 0.0f;
		Destroy (this.gameObject);

	}

	private IEnumerator GoThroughDoor() {
		anim.SetBool ("EnteringDoor", true);
		yield return new WaitForSeconds (1.0f);
		anim.SetBool ("EnteringDoor", false);
		if (doorController != null) {
			StartCoroutine (AllowForDoorTransition ());
		} else {
			rigid.isKinematic = false;
			isPassingThroughDoor = false;
		}
	}

	private IEnumerator AllowForDoorTransition() {
		doorController.transition ();
		rigid.isKinematic = false;	
		yield return new WaitForSeconds (1.5f);
		isPassingThroughDoor = false;
	}
}
