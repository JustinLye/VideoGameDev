using UnityEngine;
using System.Collections;

public class cameraScript : MonoBehaviour {

	Camera gameCam;
	public float resizeSpeed = 1.0f;
	public float maxSize = -20.0f;
	private float originalSize;
	private float adjustSpeed = 0.0f;
	public float adjustTime = 2.0f;
	void Start () {
		gameCam = GetComponent<Camera>();
		originalSize = gameCam.transform.localPosition.z;
	}
	
	// Update is called once per frame
	void Update () {
		float resizeBy = Input.GetAxis("Vertical") * Time.deltaTime * resizeSpeed;
		if (resizeBy < 0) {
			if (gameCam.transform.localPosition.z + resizeBy < maxSize)
				gameCam.transform.localPosition = new Vector3 (gameCam.transform.localPosition.x, gameCam.transform.localPosition.y, maxSize);
			else
				gameCam.transform.localPosition += new Vector3 (0.0f, 0.0f, resizeBy);
		} else {
			float newSize = Mathf.SmoothDamp(gameCam.transform.localPosition.z,originalSize, ref adjustSpeed, adjustTime, resizeSpeed);
			if (Mathf.Abs (gameCam.transform.localPosition.z - originalSize) < 0.05)
				gameCam.transform.localPosition = new Vector3 (gameCam.transform.localPosition.x, gameCam.transform.localPosition.y, originalSize);
			else
				gameCam.transform.localPosition = new Vector3 (gameCam.transform.localPosition.x, gameCam.transform.localPosition.y, newSize);
		}

	}

	public void Resize(float sizeInput) {
		if (this.gameCam.orthographicSize + sizeInput > this.maxSize)
			this.gameCam.orthographicSize = this.maxSize;
		else {
			this.gameCam.orthographicSize += sizeInput;
		}
	}

}
