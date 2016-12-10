using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

	public float maxRunSpeed;
	public float acceleration;
	public float rightDirection;

	bool grounded = true;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		float curSpeed = GetComponent<Rigidbody> ().velocity.x;
		if (Input.GetKey (KeyCode.D)) {
			if(grounded)
				GetComponent<Rigidbody> ().AddForce ((1 - (Mathf.Max(curSpeed, 0)/maxRunSpeed)) * acceleration, 0, 0);
		}
		if (Input.GetKey (KeyCode.A)) {
			if(grounded)
				GetComponent<Rigidbody> ().AddForce ((1 - Mathf.Max(-curSpeed, 0)/maxRunSpeed) * acceleration * -1, 0, 0);
		}
	}
}
