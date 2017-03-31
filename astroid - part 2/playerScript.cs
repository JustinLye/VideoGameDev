using UnityEngine;
using System.Collections;

public class playerScript : MonoBehaviour {

	public float MaxThrust = 5.0f;
	public float MaxRotationSpeed = 250.0f;
	private UIScript uiCanvas;

	private const string LEFT_STICK_X = "Left Stick X";
	private const string LEFT_STICK_Y = "Left Stick Y";
	private const string BUTTON_X = "Horizontal";

	private const float LEFT_BOUND = -7.6f;
	private const float RIGHT_BOUND = 7.6f;
	private const float LOWER_BOUND = -4.5f;
	private const float UPPER_BOUND = 4.5f;

	// Use this for initialization
	void Start () {
		uiCanvas = GetComponent<UIScript>();
	}
	
	// Update is called once per frame
	void Update () {
		float lsX = Input.GetAxis(LEFT_STICK_X);
		if (Mathf.Abs(lsX) < 0.00000001f)
			lsX = Input.GetAxis(BUTTON_X);
		
		float lsX_speed = MaxThrust * (Mathf.Abs(lsX));

		lsX *= lsX_speed * Time.deltaTime;
		
		Transform buffer = this.transform;
		
		buffer.Translate(lsX, 0.0f, 0.0f);
		if (buffer.position.x < LEFT_BOUND)
			buffer.position = new Vector3(LEFT_BOUND, transform.position.y, 0.0f);
		else if (buffer.position.x > RIGHT_BOUND)
			buffer.position = new Vector3(RIGHT_BOUND, transform.position.y, 0.0f);

		this.transform.position = buffer.position;

	}

	void OnAstroidHit() {
		if (uiCanvas != null)
			uiCanvas.incrementScore();
	}
}



