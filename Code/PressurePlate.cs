using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressurePlate : Obstacle {

	public List<TriggerTrap> traps;
	public GameObject plate;

	public float triggerDelay;
	float lastTriggerTime = -10f;

	public bool isPressured;
	public bool isReady = true;

	void OnTriggerEnter(Collider other){
		if (other.GetComponent<Player> ()) {
			if (lastTriggerTime + triggerDelay < Time.time) {
				lastTriggerTime = Time.time;
				trigger ();
				isPressured = true;
				foreach (TriggerTrap trap in traps) {
					StartCoroutine (trap.trigger());
				}
			}
		}
	}

	void OnTriggerExit(Collider other){
		isPressured = false;
	}

	public void trigger(){

	}

	Vector3 startScale;

	void Start(){
		startScale = plate.transform.localScale;

	}

	void Update(){
		if (isPressured || (lastTriggerTime + triggerDelay) > Time.time) {
			plate.transform.localScale = Vector3.Lerp (plate.transform.localScale, new Vector3 (plate.transform.localScale.x, 0.01f, plate.transform.localScale.z), 0.1f);
		} else {
			plate.transform.localScale = Vector3.Lerp (plate.transform.localScale, startScale, 0.1f);
		}
	}

}
