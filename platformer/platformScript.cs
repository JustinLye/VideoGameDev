using UnityEngine;
using System.Collections;

public class platformScript : MonoBehaviour {

	private Rigidbody2D rigid;
	public LayerMask whatIsPlayer;
	public Transform checkForPlayer;
	void Start() {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (new Vector2 (1.0f * transform.localScale.x, rigid.velocity.y));
	}

	void FixedUpdate() {
		rigid.AddForce (new Vector2 (1.0f * transform.localScale.x, rigid.velocity.y));
	}

	void flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "platform boundry") {
			rigid.isKinematic = true;
			flip ();
			rigid.isKinematic = false;
		} else {
			rigid.constraints.Equals (RigidbodyConstraints2D.FreezeAll);
		}

	}
	
}
