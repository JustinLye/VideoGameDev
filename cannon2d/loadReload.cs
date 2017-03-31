using UnityEngine;
using System.Collections;

public class loadReload : MonoBehaviour {

	[SerializeField]
	private GameObject cannonBallPrefab; //Prefab for cannon ball
	[SerializeField]
	private GameObject smokeFadePrefab; //Prefab for smoke fade

	private GameObject _cannonBall; //GameObject instantiated when the cannon is fired
	private GameObject _smokeFade; //GameObject instantiated when the cannon is fired

	private const float _reloadTime = 0.5f; //Constant used to simulate the amount of time needed to reload the cannon (in seconds)
	private const float _yCordSmokeFade = 0.35f; //Constant used to move the puff of smoke just past the end of the cannon barrel
	private float _timeSinceLastShot = 0.0f; //Variable used to track the time since the cannon was last fired.

	void Start () {
		_timeSinceLastShot = _reloadTime; // Initialize the _timeSinceLastShot variable to the _reloadTime
	}
	
	void Update () {
		/*If the left mouse button is down and the time since the cannon was last fired is
		 * greater than the reload time then instantiate a cannonball & smokeFade prefab */
		if (Input.GetMouseButton(0) && _timeSinceLastShot >= _reloadTime) {
			_cannonBall = Instantiate(cannonBallPrefab) as GameObject;
			_smokeFade = Instantiate(smokeFadePrefab) as GameObject;
			_cannonBall.transform.parent = this.gameObject.transform;
			_smokeFade.transform.parent = this.gameObject.transform;
			_smokeFade.transform.rotation = this.transform.rotation;
			_smokeFade.transform.position = _smokeFade.transform.position + new Vector3(0.0f, _yCordSmokeFade, 0.0f);
			_timeSinceLastShot = 0.0f; //Reset time since last shot to zero
		} else if(_timeSinceLastShot <= _reloadTime) {
			_timeSinceLastShot += Time.deltaTime; //increment the time since last shot if the time is less than the time needed to reload 
		}
	}
}
