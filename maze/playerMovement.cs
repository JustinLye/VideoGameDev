using UnityEngine;
using System.Collections;


[RequireComponent(typeof(Rigidbody))]
public class playerMovement : MonoBehaviour {
	private Vector3 velocity = Vector3.zero;
	private Rigidbody rigid;

	void Start () {
		rigid = GetComponent<Rigidbody> ();
	}

	public void Move(Vector3 _velocity) {
		velocity = _velocity;
	}

	void FixedUpdate() {
		rigid.MovePosition (rigid.position + velocity * Time.fixedDeltaTime);
	}
	
}
