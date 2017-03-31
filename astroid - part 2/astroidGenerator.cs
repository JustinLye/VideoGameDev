using UnityEngine;
using System.Collections;

public class astroidGenerator : MonoBehaviour {

	[SerializeField]
	public GameObject AstroidPrefab;

	private float _timeSinceLastAstroid = 1.0f;
	private float _waitTime = 0.0f;
	private float _waitMin = 0.5f;
	private float _waitMax = 2.0f;
	private randomSpin randSpin;
	private UIScript uiCanvas;
	private bool allowSpeedUpdate;
	private bool allowWaitUpdate;
	
	void Start () {
		Random.seed = System.DateTime.Now.Millisecond;
		_waitTime = Random.Range(_waitMin, _waitMax);
		randomSpin.SPEED_MAX = randomSpin._DEFAULT_SPEED_MAX;
		randomSpin.SPEED_MIN = randomSpin._DEFAULT_SPEED_MIN;
		randomSpin.controller = this.gameObject;
		uiCanvas = GetComponent<UIScript>();
		allowSpeedUpdate = allowWaitUpdate = true; //Variables that control the volume and speed of astroids in the game will be modified as the player scores 50 points (speed increase) and 100 points (volume increase)
	}
	
	void Update () {
		if (_timeSinceLastAstroid >= _waitTime) {
			Instantiate(AstroidPrefab);
			_timeSinceLastAstroid = 0.0f;
			_waitTime = Random.Range(_waitMin, _waitMax);
		} else {
			_timeSinceLastAstroid += 1.0f * Time.deltaTime;
		}

		int _tempScore = uiCanvas.GetScore(); //Holds the players current score. The score will be evaluated a couple of times during each update, I only wanted to have to rectrieve the score once.
		//decrease the maximum wait time as the player destroys more astroids
		if (_tempScore > 0 && _tempScore % 100 == 0 && _waitMax > 1.5f && allowWaitUpdate) {
			allowWaitUpdate = false; //disallow difficultly update. The wait time will continue to decrease each frame until the player hits another astroid or _waitMax drops below threshold; therefore, the difficult does not increase withouth the player scoring another 100 points.
			_waitMax -= 0.25f; //decrease maximum time to wait before creating a new astroid
			Debug.Log(string.Format("Wait range shrank Max: {0} Min: {1}", _waitMax, _waitMin)); //print message for debugging
		} else if (!(_tempScore > 0 && _tempScore % 100 == 0 && _waitMax > 1.5)) { //allow difficultly update. Once the player beings scoring again the disabling the difficultly update is no longer needed.
			allowWaitUpdate = true; //allow difficulty update
		}
		//increase the min and max speed values as the player destorys more astroids. Each time 50 points are scored the min and max speed range of the astroids is increased.
		if (_tempScore > 0 && _tempScore % 50 == 0 && randomSpin.SPEED_MAX < 70.0F && allowSpeedUpdate) {
			allowSpeedUpdate = false; //disallow difficultly update. Difficulty should only increase as the players score increases. The player could go many frames without hitting another astroid. allowSpeedUpdate is ensure the speed is only increased as the player continues to score)
			randomSpin.SPEED_MAX += 2.0f; //increase maximum speed allowed
			randomSpin.SPEED_MIN += 2.0f; //increase minimum speed allowed
			Debug.Log(string.Format("Speed range increased Max: {0} Min: {1}", randomSpin.SPEED_MAX, randomSpin.SPEED_MIN)); //print message for debugging
		} else if(!(_tempScore > 0 && _tempScore % 50 == 0 && randomSpin.SPEED_MAX < 70.0F)) { //allow difficultly update. Once the player beings scoring again disabling the difficultly update is no longer needed.
			allowSpeedUpdate = true;
		}
	}
	//calling this function will decrease one of the players lives
	void OnAstroidMiss() {
		uiCanvas.decrementLife();
	}

}
