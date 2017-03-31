using UnityEngine;
using System.Collections;

public class barrelScript : MonoBehaviour {

	private Rigidbody2D rigid;
	// Use this for initialization
	void Start () {
		rigid = GetComponent<Rigidbody2D> ();
	}


	void FixedUpdate() {
		rigid.AddForce (new Vector2 (2.0f * transform.localScale.x, rigid.velocity.y));
	}

	void flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "barrel boundry") {
			rigid.isKinematic = true;
			flip ();
			rigid.isKinematic = false;
		}
	}

}
