using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrapDoor : TriggerTrap {
	public GameObject target;
	public Transform moveTarget;
	Vector3 startPos;

	float startTime;
	public float duration;


	public override void action ()
	{
		startTime = Time.time;
	}

	void Start(){
		startPos = target.transform.position;
		startTime = -duration;
	}

	void Update(){
		if (startTime + duration > Time.time) {
			target.transform.position = Vector3.Lerp (target.transform.position, moveTarget.position, 0.1f);
		} else {
			target.transform.position = Vector3.Lerp (target.transform.position, startPos, 0.1f);
		}
	}
}
