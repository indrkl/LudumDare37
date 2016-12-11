using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle {
	public AudioClip deathClip;

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Player> ()) {
			other.GetComponent<Player> ().die (deathClip);
		}
	}
}
