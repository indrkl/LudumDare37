using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle {
	public AudioClip deathClip;

	static Vector3 rotationSpeed = new Vector3(0, 0, 360);

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Player> ()) {
			other.GetComponent<Player> ().die (deathClip);
		}
	}

	void Update(){
		transform.Rotate (rotationSpeed * Time.deltaTime);
	}
}
