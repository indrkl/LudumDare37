using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

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
		lastVerticalSpeed = verticalSpeed;
		if (verticalDiff > verticalSensitivity || Mathf.Abs(verticalSpeed) > verticalSensitivity) {
			grounded = false;
		} else {
			grounded = true;
		}
		float acc = acceleration;
		if (!grounded) {
			acc = accerleationOnJump;
		}
		if (Input.GetKey (KeyCode.D)) {
			GetComponent<Rigidbody> ().AddForce ((1 - (Mathf.Max(curSpeed, 0)/maxRunSpeed)) * acc, 0, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			GetComponent<Rigidbody> ().AddForce ((1 - Mathf.Max(-curSpeed, 0)/maxRunSpeed) * acc * -1, 0, 0);
		}
		if (Input.GetKey (KeyCode.W)) {
			if (grounded && (Time.time - lastJump) > jumpDelay) {
				GetComponent<Rigidbody> ().AddForce (0, jumpForce, 0);
				lastJump = Time.time;
			} else {
				GetComponent<Rigidbody> ().AddForce (0, jumpExtraAcceleration, 0);
			}
		}
	}

	public void die(){
		GameMaster.instance.OnDeath ();
	}
}