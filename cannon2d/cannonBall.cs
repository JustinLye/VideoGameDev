using UnityEngine;
using System.Collections;

public class cannonBall : MonoBehaviour {

	public float shotForce = 100.0f;

	private Rigidbody2D rigid;
	private AudioSource audio;
	private float lifeSpan = 10.0f;
	private float lifeTime = 0.0f;

	void Start () {
		rigid = GetComponent<Rigidbody2D>();
		audio = GetComponent<AudioSource>();
		rigid.gravityScale = 1.0f;
		rigid.AddForce(transform.right * shotForce);
		audio.Play();
	}
	
	void Update () {
		/* To keep from accumulating cannon ball game objects,
		 * each cannon ball will have a limited life span in the 
		 * game before it is destroyed */
		if (lifeTime >= lifeSpan)
			Destroy(this.gameObject);
		else
			lifeTime += Time.deltaTime;
	}
}
