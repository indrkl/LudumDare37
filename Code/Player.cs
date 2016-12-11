using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float crouchHeight;
	public float standHeight;

	bool isCrounching = false;

	public AudioClip jump;
	public AudioClip drop;

	public Transform ninja;

	public float maxRunSpeed;
	public float acceleration;
	public float accerleationOnJump;
	public float jumpExtraAcceleration;
	public float rightDirection;

	public float jumpForce;
	public float jumpDelay;
	float lastJump = 0;

	public float verticalSensitivity;

	public bool grounded = true;

	float lastVerticalSpeed = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float curSpeed = GetComponent<Rigidbody> ().velocity.x;
		float verticalSpeed = GetComponent<Rigidbody> ().velocity.y;
		float verticalDiff = Mathf.Abs (verticalSpeed - lastVerticalSpeed);
		bool moving = false;
		lastVerticalSpeed = verticalSpeed;
		if (verticalDiff > verticalSensitivity || Mathf.Abs(verticalSpeed) > verticalSensitivity) {
			grounded = false;
		} else if (!grounded) {
			if (drop) {
				GetComponent<AudioSource> ().clip = drop;
				GetComponent<AudioSource> ().Play ();
			}
			grounded = true;
		}
		float acc = acceleration;
		bool left = false;
		bool right = false;
		if (!grounded) {
			acc = accerleationOnJump;
		}
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			right = true;
			GetComponent<Rigidbody> ().AddForce ((1 - (Mathf.Max(curSpeed, 0)/maxRunSpeed)) * acc, 0, 0);
			moving = true;
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			left = true;
			GetComponent<Rigidbody> ().AddForce ((1 - Mathf.Max(-curSpeed, 0)/maxRunSpeed) * acc * -1, 0, 0);
			moving = true;
		}
		if (!moving) {
			GetComponent<Rigidbody> ().AddForce ((1 - (Mathf.Max(curSpeed, 0)/maxRunSpeed)) * acc, 0, 0);
			GetComponent<Rigidbody> ().AddForce ((1 - Mathf.Max(-curSpeed, 0)/maxRunSpeed) * acc * -1, 0, 0);
		}

		if (Input.GetKey (KeyCode.S) || Input.GetKey (KeyCode.DownArrow)) {
			if (!isCrounching) {
				isCrounching = true;
				GetComponent<CapsuleCollider> ().height = crouchHeight;
			}
		} else if(isCrounching) {
			GetComponent<CapsuleCollider> ().height = standHeight;
			isCrounching = false;
		}

		if (right && !left) {
			ninja.localRotation = Quaternion.Euler (0, 90, 0);
		}
		if (left && !right) {
			ninja.localRotation = Quaternion.Euler (0, 270, 0);
		}


		if (Input.GetKey (KeyCode.W) || Input.GetKey (KeyCode.UpArrow)) {
			if (grounded && (Time.time - lastJump) > jumpDelay) {
				GetComponent<Rigidbody> ().AddForce (0, jumpForce, 0);
				lastJump = Time.time;
				if (jump) {
					GetComponent<AudioSource> ().clip = jump;
					GetComponent<AudioSource> ().Play ();
				}
			} else {
				GetComponent<Rigidbody> ().AddForce (0, jumpExtraAcceleration, 0);
			}
		}

		if (transform.position.y < -100) {
			die ();
		}
	}

	public void die(){
		GameMaster.instance.OnDeath ();
	}
}