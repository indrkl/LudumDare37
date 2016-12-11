using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float crouchHeight;
	public float standHeight;

	public Animator anim;

	bool isCrounching = false;

	public AudioClip jump;
	public AudioClip drop;
	public AudioClip runningClip;

	public Transform ninja;

	public float maxRunSpeed;
	public float acceleration;
	public float accerleationOnJump;
	public float jumpExtraAcceleration;
	public float rightDirection;

	public float jumpForce;
	public float jumpDelay;
	float lastJump = 0;
	public bool running = false;

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

		if ((moving && grounded) && !running) {
			running = true;
			if(anim)
				anim.SetBool ("running", running);
			if (runningClip && GetComponent<AudioSource> ()) {
				GetComponent<AudioSource> ().clip = runningClip;
				GetComponent<AudioSource> ().loop = true;
				GetComponent<AudioSource> ().Play ();
			}
				
		}
		if ((!moving || !grounded) && running) {
			running = false;
			if (anim)
				anim.SetBool ("running", running);
			if (GetComponent<AudioSource> ()) {
				GetComponent<AudioSource> ().loop = false;
				if (GetComponent<AudioSource> ().clip == runningClip)
					GetComponent<AudioSource> ().Stop ();
			}
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
					GetComponent<AudioSource> ().loop = false;
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

	public void die(AudioClip deathClip = null){
		GameMaster.instance.OnDeath ();
		if (anim)
			anim.SetBool ("death", true);
		if (deathClip && GetComponent<AudioSource> ()) {
			GetComponent<AudioSource> ().clip = deathClip;
			GetComponent<AudioSource> ().loop = false;
			GetComponent<AudioSource> ().Play ();
		}
	}

	public void resurrectPlayer(){
		if (anim)
			anim.SetBool ("death", false);
	}
}