using UnityEngine;
using System.Collections;

public class scroll : MonoBehaviour {

	// Attempt to move/scroll the game object horizontally. hrzInput: user input, left_bound: left x-position bound (restict movement), right_bound: right x-position (restrict movement)
	public static bool horizontalMove(ref Transform objTransform, float hrzInput, float maxScrollSpeed, float left_bound, float right_bound) {
		bool allowMove = true; // variable used to return the result of the move
		if (Mathf.Abs(hrzInput) > 0.000000001f) { // Check if user has any input and process accordingly
			float hrzSpeed = maxScrollSpeed * (Mathf.Abs(hrzInput)) * Time.deltaTime; // Store speed of movement (independent of frame rate) in float variable
			hrzInput *= hrzSpeed; // Scale the input for speed
			Transform buffer = objTransform; // Copy transform into buffer. This will allow the translation to be made then checked against bounds before applying the transformation to the actual game object
			buffer.Translate(hrzInput, 0.0f, 0.0f); // Apply the translation to the buffer
			if (buffer.transform.position.x < left_bound) {
				buffer.transform.position = new Vector3(left_bound, objTransform.position.y, objTransform.transform.position.z); // If the movement is out of bounds to the left, set the camera position to the left bound and return false
				allowMove = false; // set outcome to false
			} else if (buffer.transform.position.x > right_bound) {
				buffer.transform.position = new Vector3(right_bound, objTransform.position.y, objTransform.position.z); // If the movement is out of bounds to the right, set the camera position to the right bound and return false
				allowMove = false; // set outcome to false
			}
			objTransform.position = buffer.transform.position; // Assign the new position vector to the game object. 
		}
		return allowMove; // return outcome
	}

}
