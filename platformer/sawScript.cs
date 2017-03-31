using UnityEngine;
using System.Collections;

public class sawScript : MonoBehaviour {
	private Rigidbody2D rigid;

	void Start() {
		rigid = GetComponent<Rigidbody2D> ();
		rigid.AddForce (new Vector2 (-2.0f * transform.localScale.x, rigid.velocity.y));

	}

	void FixedUpdate() {
		rigid.AddForce (new Vector2 (-1.5f * transform.localScale.x, rigid.velocity.y));
		transform.Rotate(new Vector3(0.0f, 0.0f, transform.localScale.x * 10.0f));
	}

	void flip() {
		Vector3 theScale = transform.localScale;
		theScale.x *= -1;
		transform.localScale = theScale;

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.tag == "saw boundry") {
			Debug.Log ("Boundry Entered");
			rigid.isKinematic = true;
			flip ();
			rigid.isKinematic = false;
		}
	}

}
