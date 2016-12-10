using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Obstacle {
	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Player> ()) {
			GameMaster.instance.OnSuccess ();
		}
	}
}
