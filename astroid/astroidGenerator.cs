using UnityEngine;
using System.Collections;

public class astroidGenerator : MonoBehaviour {

	[SerializeField]
	public GameObject AstroidPrefab;

	private float _timeSinceLastAstroid = 1.0f;
	private float _waitTime = 0.0f;
	private float _waitMin = 0.5f;
	private float _waitMax = 2.0f;
	GameObject _astroid;
	// Use this for initialization
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;
		_waitTime = Random.Range(_waitMin, _waitMax);
	}
	
	// Update is called once per frame
	void Update () {
		if (_timeSinceLastAstroid >= _waitTime) {
			_astroid = Instantiate(AstroidPrefab);
			_timeSinceLastAstroid = 0.0f;
			_waitTime = Random.Range(_waitMin, _waitMax);
		} else {
			_timeSinceLastAstroid += 1.0f * Time.deltaTime;
		}

		
	}
}
