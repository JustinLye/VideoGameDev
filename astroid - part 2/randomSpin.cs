using UnityEngine;
using System.Collections;

/*randomSpin........................................................
 
	Positions game object randomly along horizontal axis.
	Scales game object by random value
	Adds torque and force to game object ridgidbody2D
	Destroys game object once it reaches local y position coordinate
	
	I found an example of how to use the OnTriggerEnter2D method to
	instantiate an explosion in the unity documentation  installed with
	unity ../Unity/Editor/Data/Documentation/en/ScriptReference/Collision2D-contacts.html 
 
 * Updated On: 02/08/2016 - created default speed min and max as well as speed min and speed max to allow these variables to be adjusted outside of the class
 * Updated On: 02/10/2016 - comments added to OnTriggerEnter2D method

*/
//.................................................................

public class randomSpin : MonoBehaviour {

	public GameObject explosion; //...............................................declare explosion sprite
	float _torque = 0.0f, _speed = 0.0f, _scale = 0.0f; //.........................declare float variables to hold random values
	public static GameObject controller;

	//CONSTANTS FOR RANDOM VALUE RANGES.............................................................................

	private const float _TORQUE_MIN = 200.0f, /*..................................minumium torque value*/
		_TORQUE_MAX = 400.0f, /*..................................................maximum torque value*/
		_SCALE_MIN = 0.45f, /*....................................................minium scale*/
		_SCALE_MAX = 0.75f, /*....................................................maximum scale*/
		_HORIZONTAL_MIN = -7.0f, /*...............................................horizontal position minimum*/
		_HORIZONTAL_MAX = 7.0f; //................................................horizontal position maximum

	//...............................................................................................................

	//PUBLIC STATIC VALUES: allow random speed range of values to be adjusted as the players score increases
	public const float
		_DEFAULT_SPEED_MIN = 20.0f, /*............................................minimum speed*/
		_DEFAULT_SPEED_MAX = 40.0f; /*............................................maximum speed constant*/

	public static float SPEED_MIN = 0.0F, SPEED_MAX = 0.0F;



	void Start () {
		Rigidbody2D rigid;//......................................................declare ridgidbody2D variable used to manipulate game object ridgidbody2D component
		rigid = GetComponent<Rigidbody2D>();//....................................attempt to get ridgidbody2D component
		if (rigid == null)/*......................................................check if rigidbody2D component was retrieved*/
			Debug.LogError("Game object does not have Rigidbody2D component");/*..log error if attempt to get ridgidbody2D was unsuccessful*/
		else { /*.................................................................randomly manipulate the position, scale, torque, and speed of the game object if attempt to get ridgidbody2D was successful*/
			this.transform.position = new Vector3( /*.............................set random horizontal position*/
				Random.Range(_HORIZONTAL_MIN, _HORIZONTAL_MAX),
				this.transform.position.y, this.transform.position.z);			

			_torque = Random.Range( /*............................................get random torque (v)alue s.t. v is a member of { (-MAX, -MIN) U (MIN, MAX) } */
				Random.Range(_TORQUE_MAX * -1.0f, _TORQUE_MIN * -1.0f), /*........negative value range (-MAX, -MIN) */
				Random.Range(_TORQUE_MIN, _TORQUE_MAX)); //.......................positive value range (MIN, MAX)						
			
			_speed = Random.Range(SPEED_MIN, SPEED_MAX); //.......................get random speed value (used to add force)
			_scale = Random.Range(_SCALE_MIN, _SCALE_MAX); //.....................get random scale value

			this.transform.localScale = new Vector3(_scale, _scale, 1.0f);//......scale by random scale value obtained above
			rigid.AddTorque(_torque * _scale); //.................................add random torque value obtained above to the rigidbody
			rigid.AddForce(new Vector2(0.0f, -1.0f * _speed)); //.................add random force value obtained above to the rigidbody
		}
	}

	public void resetStatics() {
		SPEED_MIN = _DEFAULT_SPEED_MIN; //................................................reset value of public static speed minimum variable
		SPEED_MAX = _DEFAULT_SPEED_MAX; //................................................reset value of public static speed maximum variable
		controller = null;
	}

	void FixedUpdate () {
		if (this.transform.localPosition.y < -6.0f) {/*............................magic number used here, I have not research how to determine if the object is out of view*/
			if (controller != null)
				controller.SendMessage("OnAstroidMiss", null);
			Destroy(this.gameObject); //..........................................destroy to object if it is likely off of the game screen
		}
	}

	void OnTriggerEnter2D(Collider2D other) {
		
		if (other.tag == "ship") { //check if the object collided with is a ship
			other.gameObject.SendMessage("OnAstroidHit", null); //if the object is a ship call the OnAtroidHit method of the ship. This should tell the ship to increase the score.
			Vector3 hitCords = new Vector3(transform.localPosition.x, transform.localPosition.y, transform.localPosition.z); //record the coordinates of where the collision occurred.
			explosion.transform.localScale = 0.50f * this.transform.localScale; //adjust the scale of the explosion sprite to that of the astriod
			GameObject _tempGameObject = Instantiate(explosion, hitCords, Quaternion.identity) as GameObject; //create a new explosion sprite where the collision occured
			Rigidbody2D _tempRidgid = _tempGameObject.GetComponent<Rigidbody2D>(); //get the rigidbody of the explosion so force may be added
			_tempRidgid.AddForce(new Vector2(0.0f, -1.0f * _speed)); //add force to the explosion sprite
			Destroy(this.gameObject); //destroy the astroid
		}
	}
}
