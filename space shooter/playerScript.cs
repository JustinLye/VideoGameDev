using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public float MaxThrust = 5.0f;
	public float MaxRotationSpeed = 250.0f;

	private const string LEFT_STICK_X = "Left Stick X";
	private const string LEFT_STICK_Y = "Left Stick Y";

	private const float LEFT_BOUND = -7.6f;
	private const float RIGHT_BOUND = 7.6f;
	private const float LOWER_BOUND = -4.5f;
	private const float UPPER_BOUND = 4.5f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		float lsY = Input.GetAxis(LEFT_STICK_Y);
		float lsX = Input.GetAxis(LEFT_STICK_X);

		float lsY_speed = MaxThrust * Mathf.Abs(lsY);
		float lsX_speed = MaxRotationSpeed * Mathf.Abs(lsX);

		if (lsY < 0)
			lsY = 0;
		else
			lsY *= lsY_speed * Time.deltaTime;
		lsX *= lsX_speed * Time.deltaTime;

		Transform buffer = this.transform;
		
		buffer.Translate(0.0f, lsY, 0.0f);
		buffer.Rotate(0.0f, 0.0f, lsX * -1);
		if (buffer.position.x < LEFT_BOUND)
			buffer.position = new Vector3(LEFT_BOUND, transform.position.y, 0.0f);
		else if (buffer.position.x > RIGHT_BOUND)
			buffer.position = new Vector3(RIGHT_BOUND, transform.position.y, 0.0f);

		if (buffer.position.y < LOWER_BOUND)
			buffer.position = new Vector3(transform.position.x, LOWER_BOUND, 0.0f);
		else if (buffer.position.y > UPPER_BOUND)
			buffer.position = new Vector3(transform.position.x, UPPER_BOUND, 0.0f);

		this.transform.rotation = buffer.rotation;
		this.transform.position = buffer.position;

	}
}



