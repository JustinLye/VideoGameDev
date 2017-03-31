using UnityEngine;
using System.Collections;

public class audioScript : MonoBehaviour {

	[SerializeField]
	private AudioSource _toneBeepAudio;
	[SerializeField]
	private AudioSource _lowTimeBeepAudio;
	[SerializeField]
	private AudioSource _bikeHornAudio;
	[SerializeField]
	private AudioSource _victoryAudio;
	[SerializeField]
	private AudioSource _gameOverAudio;
	[SerializeField]
	private AudioSource _backgroundAudio;

	private static bool _playToneBeepAudio = false;
	private static bool _playingJumpAudio = false;
	private static bool _playLowTimeBeep = false;
	private static bool _playBikeHornAudio = false;
	private static bool _playVictoryAudio = false;
	private static bool _playGameOverAudio = false;
	private static bool _playBackgroundAudio = false;
	private static bool _updateBackgroundAudio = false;

	void Update() {
		if (_playToneBeepAudio) {
			if (_toneBeepAudio != null) {
				_toneBeepAudio.Play ();
			}
			_playToneBeepAudio = false;
		}
		if (_playLowTimeBeep) {
			if (_playLowTimeBeep != null) {
				_lowTimeBeepAudio.Play ();
				_playLowTimeBeep = false;
			}
		}
		if(_playBikeHornAudio) {
			_bikeHornAudio.Play ();
			_playBikeHornAudio = false;
		}
		if (_playVictoryAudio) {
			_victoryAudio.Play ();
			_playVictoryAudio = false;
		}
		if (_playGameOverAudio) {
			_gameOverAudio.Play ();
			_playGameOverAudio = false;
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
	public static void PlayToneBeepAudio() {
		if (!_playToneBeepAudio) {
			_playToneBeepAudio = true;
		}
	}
	public static void PlayLowTimeBeepAudio() { 
		if (!_playLowTimeBeep) {
			_playLowTimeBeep = true;
		}
	}
	public static void PlayBikeHornAudio() {
		if(!_playBikeHornAudio) {
			_playBikeHornAudio = true;
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
	public static void PlayBackgroundAudio(bool _play) {
		if(!_updateBackgroundAudio) {
			_updateBackgroundAudio = true;
			_playBackgroundAudio = _play;
		}
	}


}
