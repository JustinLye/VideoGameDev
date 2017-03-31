using UnityEngine;
using System.Collections;

public class scroll : MonoBehaviour {

	public float speed = 0.5f;

	private float currentOffset = 0.0f;

	private Renderer render;
	private MeshRenderer mRender;
	// Use this for initialization
	void Start () {
		render = GetComponent<Renderer>();
		mRender = GetComponent<MeshRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
		Vector2 offset = new Vector2(0.0f, Time.time * speed);
		render.material.mainTextureOffset = offset;

		currentOffset += Time.deltaTime * speed;
		if (currentOffset > 1.0f)
			currentOffset -= 1.0f;
		mRender.material.mainTextureOffset = new Vector2(0, currentOffset);

	}
}
