using UnityEngine;
using System.Collections;

public class audioScript : MonoBehaviour {

	[SerializeField]
	private AudioSource _jumpAudio;
	[SerializeField]
	private AudioSource _acidBathAudio;
	[SerializeField]
	private AudioSource _shortCircuitAudio;
	[SerializeField]
	private AudioSource _enemyCollisionAudio;
	[SerializeField]
	private AudioSource _cashRegisterAudio;
	[SerializeField]
	private AudioSource _doorOpenAudio;
	[SerializeField]
	private AudioSource _switchAudio;
	[SerializeField]
	private AudioSource _sawAudio;
	[SerializeField]
	private AudioSource _victoryAudio;
	[SerializeField]
	private AudioSource _introAudio;
	[SerializeField]
	private AudioSource _gameOverAudio;
	[SerializeField]
	private AudioSource _deathAudio;
	[SerializeField]
	private AudioSource _backgroundAudio;

	private static bool _playJumpAudio = false;
	private static bool _playingJumpAudio = false;
	private static bool _playAcidBathAudio = false;
	private static bool _playShortCircuitAudio = false;
	private static bool _playEnemyCollisionAudio = false;
	private static bool _playCashRegisterAudio = false;
	private static bool _playDoorOpenAudio = false;
	private static bool _playSwitchAudio = false;
	private static bool _playSawAudio = false;
	private static bool _playVictoryAudio = false;
	private static bool _playIntroAudio = false;
	private static bool _playGameOverAudio = false;
	private static bool _playDeathAudio = false;
	private static bool _playBackgroundAudio = false;
	private static bool _updateBackgroundAudio = false;

	private static float _introPlayTime;

	void Start() {
		_introPlayTime = _introAudio.time;
	}

	public static float IntroPlayTime() { return _introPlayTime; }

	void Update() {
		if (_playJumpAudio) {
			_jumpAudio.Play ();
			_playJumpAudio = false;
		}
		if (_playAcidBathAudio) {
			_acidBathAudio.Play ();
			_playAcidBathAudio = false;
		}
		if(_playShortCircuitAudio) {
			_shortCircuitAudio.Play ();
			_playShortCircuitAudio = false;
		}
		if(_playEnemyCollisionAudio) {
			_enemyCollisionAudio.Play ();
			_playEnemyCollisionAudio = false;
		}
		if (_playCashRegisterAudio) {
			_cashRegisterAudio.Play ();
			_playCashRegisterAudio = false;
		}
		if (_playSwitchAudio) {
			_switchAudio.Play ();
			_playSwitchAudio = false;
		}
		if (_playSawAudio) {
			_sawAudio.Play ();
			_playSawAudio = false;
		}
		if (_playVictoryAudio) {
			_victoryAudio.Play ();
			_playVictoryAudio = false;
		}
		if (_playIntroAudio) {
			_introAudio.Play ();
			_playIntroAudio = false;
		}
		if (_playGameOverAudio) {
			_gameOverAudio.Play ();
			_playGameOverAudio = false;
		}
		if (_playDeathAudio) {
			_deathAudio.Play ();
			_playDeathAudio = false;
		}
		if (_updateBackgroundAudio) {
			_updateBackgroundAudio = false;
			if (_playBackgroundAudio) {
				_backgroundAudio.Play ();
			} else {
				_backgroundAudio.Pause ();
			}
		}

	}
	public static void PlayJumpAudio() {
		if (!_playJumpAudio) {
			_playJumpAudio = true;
		}
	}
	public static void PlayAcidBathAudio() { 
		if (!_playAcidBathAudio) {
			_playAcidBathAudio = true;
		}
	}
	public static void PlayShortCircuitAudio() {
		if(!_playShortCircuitAudio) {
			_playShortCircuitAudio = true;
		}
	}
	public static void PlayEnemyCollisionAudio() {
		if(!_playEnemyCollisionAudio) {
			_playEnemyCollisionAudio = true;
		}
	}
	public static void PlayCashRegisterAudio() {
		if (!_playCashRegisterAudio) {
			_playCashRegisterAudio = true;
		}
	}
	public static void PlayDoorOpenAudio() { 
		if (!_playDoorOpenAudio) {
			_playDoorOpenAudio = true;
		}
	}
	public static void PlaySwitchAudio() { 
		if (!_playSwitchAudio) {
			_playSwitchAudio = true;
		}
	}
	public static void PlaySawAudio() {
		if (!_playSawAudio) {
			_playSawAudio = true;
		}
	}
	public static void PlayGameOverAudio() {
		if (!_playGameOverAudio) {
			_playGameOverAudio = true;
		}
	}
	public static void PlayVictoryAudio() {
		if (!_playVictoryAudio) {
			_playVictoryAudio = true;
		}
	}
	public static void PlayIntroAudio() {
		if (!_playIntroAudio) {
			_playIntroAudio = true;
		}
	}
	public static void PlayDeathAudio() {
		if (_playDeathAudio) {
			_playDeathAudio = true;
		}
	}
	public static void PlayBackgroundAudio(bool _play) {
		if(!_updateBackgroundAudio) {
			_updateBackgroundAudio = true;
			_playBackgroundAudio = _play;
		}
	}


}
