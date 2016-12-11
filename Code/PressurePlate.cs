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
		if (GetComponent<AudioSource> ()) {
			GetComponent<AudioSource> ().Play ();
		}
	}

	Vector3 startPos;
	Vector3 targetPos;

	void Start(){
		startPos = plate.transform.position;
		targetPos = startPos - new Vector3 (0, 0.1f, 0);

	}

	public override void atStart ()
	{
		foreach (TriggerTrap trap in traps) {
			StopCoroutine (trap.trigger ());
		}
	}

	void Update(){
		if (isPressured || (lastTriggerTime + triggerDelay) > Time.time) {
			plate.transform.position = Vector3.Lerp (plate.transform.position,targetPos, 0.1f);
		} else {
			plate.transform.position = Vector3.Lerp (plate.transform.position, startPos, 0.1f);
		}
	}



}
