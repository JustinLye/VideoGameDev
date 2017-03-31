using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class robotController : MonoBehaviour {
	public float maxRunSpeed = 10.0f;
	public float maxWalkSpeed = 5.0f;
	private float maxSpeed = 0.0f;
	private bool facingRight = false;
	private Animator anim;
	private Rigidbody2D rigid;
	private bool isWalking = false;
	private bool isRolling = false;
	private bool grounded = false;
	public Transform groundCheck;
	public Transform hitCheck;
	private float groundRadius = 0.2f;
	private float hitRadius = 0.2f;
	public float jumpForce = 700.0f;
	public LayerMask whatIsGround;
	public LayerMask whatIsEnemyAttack;
	private bool attackerHasDisengaged = true;
	void Start () {
		anim = GetComponent<Animator>();
		rigid = GetComponent<Rigidbody2D>();
	}

	void Update() {
		if (grounded && Input.GetAxis ("Jump") > 0.0f && Mathf.Abs (rigid.velocity.y) < 0.01f) {
			grounded = false;
			rigid.AddForce (new Vector2 (rigid.velocity.x, jumpForce));
			audioScript.PlayJumpAudio ();
		}
		isWalking = (Input.GetAxis ("Walk") > 0);
		anim.SetBool ("Walking", isWalking);
		isRolling = (Input.GetAxis ("Roll") > 0);
		anim.SetBool ("Roll", isRolling);
	}

	void FixedUpdate() {
		grounded = (Physics2D.OverlapCircle (groundCheck.position, groundRadius, whatIsGround));
		anim.SetFloat ("vSpeed", rigid.velocity.y);
		anim.SetBool ("Ground", grounded);	
		float move = Input.GetAxis ("Horizontal");
		if (isWalking)
			maxSpeed = move * maxWalkSpeed;
		else
			maxSpeed = move * maxRunSpeed;
		if (grounded) {
			anim.SetFloat ("speed", Mathf.Abs (maxSpeed));
			rigid.AddForce (new Vector2 (maxSpeed, rigid.velocity.y), ForceMode2D.Force);
		}
		if (move < 0 && facingRight)
			Flip ();
		else if (move > 0 && !facingRight)
			Flip ();

		bool attackerPresent = Physics2D.OverlapCircle (hitCheck.position, hitRadius, whatIsEnemyAttack);

		if (attackerHasDisengaged && attackerPresent && playerController.playerAvailableForAttack ()) {
			attackerHasDisengaged = false;
			playerController.playerAttacked (playerController.AttackType.ENEMY);			
		} else if (!attackerHasDisengaged && !attackerPresent) {
			attackerHasDisengaged = true;
		}

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "checkpoint") {
			playerController.checkpointReached (other.gameObject.transform);
		}
		if (other.tag == "victory door") {
			playerController.VictoryDoorReached ();
		}
		if (playerController.playerAvailableForAttack() && 
			other.gameObject.GetInstanceID() != playerController.lastKnownPerp()) {
			if (other.tag == "acid") {
				playerController.playerAttacked
				(playerController.AttackType.ACID, other.gameObject.GetInstanceID ());

			} else if (other.tag == "spike") {
				playerController.playerAttacked
				(playerController.AttackType.SPIKE, other.gameObject.GetInstanceID ());
			} else if (other.tag == "saw") {
				playerController.playerAttacked
				(playerController.AttackType.SAW, other.gameObject.GetInstanceID ());
			}
		}
	}

	void Flip() {
		facingRight = !facingRight;
		Vector3 theScale = this.transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;
	}


}
