using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spikes : Obstacle {

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Player> ()) {
			other.GetComponent<Player> ().die ();
		}
	}
}
