using UnityEngine;
using System.Collections;

public class doorTransitionController : MonoBehaviour {


	public Transform Door1;
	public Transform Door2;
	public GameObject Character;
	private bool atDoor1 = true;

	public void transition() {
		if (atDoor1) {
			Character.transform.position = Door2.position;
			atDoor1 = false;
		} else {
			Character.transform.position = Door1.position;
			atDoor1 = true;
		}
	}
}
