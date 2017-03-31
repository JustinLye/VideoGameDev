using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


[RequireComponent(typeof(UIScript))]
public class playerController : MonoBehaviour {

	public enum AttackType {NONE, ENEMY, SPIKE, ACID, SAW};

	[SerializeField]
	private GameObject _playerPrefab;
	private static GameObject _player;

	[SerializeField]
	private Transform _CheckPoint1;
	[SerializeField]
	private Transform _CheckPoint2;
	[SerializeField]
	private Transform _CheckPoint3;
	[SerializeField]
	private Transform _CheckPoint4;
	[SerializeField]
	private Transform _CheckPoint5;
	[SerializeField]
	private Transform _CheckPoint6;
	private static Transform _CurrentCheckPoint;
	private static bool playerAttackInProgress;
	private static AttackType perpetrator;
	private static int perpsID = -1;
	private static int lifeCount;
	private static bool playerIsDead;
	private static bool playerIsWounded;
	private static bool respawnPlayer;
	private static bool destroyPlayer;
	private static bool playerIsVictorious;
	private Animator _playerAnim;

	private static int _treasureCount = 0;
	public const int totalTreasureCount = 6;

	[SerializeField]
	private UIScript _uiScript;

	// Use this for initialization
	void Start () {
		playerAttackInProgress = false;
		perpetrator = AttackType.NONE;
		lifeCount = 3;
		_treasureCount = 0;
		playerIsDead = false;
		playerIsVictorious = false;
		_CurrentCheckPoint = _CheckPoint1;
		_playerPrefab.transform.position = _CheckPoint1.position;
		_player = Instantiate (_playerPrefab);
		_playerAnim = _player.GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void Update () {
		_uiScript.life3.enabled = (lifeCount == 3);
		_uiScript.life2.enabled = (lifeCount >= 2);
		_uiScript.life1.enabled = (lifeCount >= 1);
		if (respawnPlayer) {
			StartCoroutine ("DieAnimation", true);
			respawnPlayer = false;
		} else if (destroyPlayer) {
			destroyPlayer = false;
			_playerPrefab.transform.position = _CurrentCheckPoint.transform.position;
			Destroy (_player.gameObject);
			_player = Instantiate (_playerPrefab);
			_playerAnim = _player.GetComponent<Animator> ();
			perpsID = -1;

		}
		if (playerIsDead) {
			StartCoroutine ("GameOver", null);
		} else if (playerIsVictorious) {
			playerIsVictorious = false;
			_uiScript.VictoryMenu ();
		}
	}

	private static void processPlayerAttack() {
		lifeCount--;		
		switch (perpetrator) {
		case AttackType.ACID:
			audioScript.PlayAcidBathAudio ();
			break;
		case AttackType.SPIKE:
			audioScript.PlayEnemyCollisionAudio ();
			break;
		case AttackType.ENEMY:
			audioScript.PlayEnemyCollisionAudio ();
			break;
		case AttackType.SAW:
			audioScript.PlaySawAudio ();
			break;
		}
		if (lifeCount == 0) {
			playerIsDead = true;
		} else {
			respawnPlayer = true;
		}
	}

	public static void checkpointReached(Transform _checkpoint) {
		_CurrentCheckPoint.transform.position = _checkpoint.transform.position;
	}

	public static void healPlayersWounds() {
		if (!playerIsDead)
			playerIsWounded = true;
	}

	public static bool playerAvailableForAttack() {
		return !playerAttackInProgress;
	}

	public static int lastKnownPerp() { return perpsID; }

	public static void playerAttacked(AttackType _AttackType, int _perpsID) {
		perpsID = _perpsID;
		playerAttacked (_AttackType);
	}

	public static void playerAttacked(AttackType _AttackType) {
		if (!playerAttackInProgress && !playerIsDead) {
			playerAttackInProgress = true;
			perpetrator = _AttackType;
			processPlayerAttack ();
			perpetrator = AttackType.NONE;
			playerAttackInProgress = false;
		}
	}

	public static void incrementTreasureCount() {
		_treasureCount++;
	}

	public static bool PlayerIsVictorious() {
		return (!playerIsDead && playerIsVictorious && (_treasureCount == totalTreasureCount));
	}

	public static void VictoryDoorReached() {
		if (!playerIsDead && !playerIsVictorious) {
			playerIsVictorious = true;
		}
	}



	private IEnumerator GameOver() {
		
		yield return StartCoroutine ("DieAnimation", false);
		_uiScript.GameOverMenu ();
	}

	private IEnumerator DieAnimation(bool _destroyPlayer) {
		_playerAnim.SetBool("Die", true);
		yield return new WaitForSeconds(1.4f);
		_playerAnim.SetBool("Die", false);
		destroyPlayer = _destroyPlayer;
	}

}

