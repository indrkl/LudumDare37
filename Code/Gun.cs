using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Obstacle {
	public float interval;
	public float startDelay;
	public bool triggered = false;
	public int direction;
	public int yDirection = 0;

	float startTime;
	int counter = 1;

	void Start(){
		startTime = Time.time + startDelay;
	}

	public override void atStart ()
	{
		startTime = Time.time + startDelay;
		counter = 0;
	}

	void Update(){
		if (startTime + interval * counter <= Time.time) {
			counter++;
			Dart dart = Instantiate (GameMaster.instance.dartPrefab);
			dart.transform.position = transform.position;
			dart.transform.position += new Vector3 (direction * 0.5f, yDirection * 0.5f, 0);
			dart.direction = direction;
			dart.yDirection = yDirection;
			dart.source = this.gameObject;
			triggered = false;
		}
		if (startTime + interval * counter - 0.2f <= Time.time && !triggered) {
			if (GetComponent<AudioSource> ())
				GetComponent<AudioSource> ().Play ();
			triggered = true;
		}
	}
}
