using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : Obstacle {
	public float interval;
	public int direction;

	float startTime;
	int counter = 1;

	public override void atStart ()
	{
		startTime = Time.time;
	}

	void Update(){
		if (startTime + interval * counter <= Time.time) {
			counter++;
			Dart dart = Instantiate (GameMaster.instance.dartPrefab);
			dart.transform.position = transform.position;
			dart.transform.position += new Vector3 (direction * 0.5f, 0, 0);
			dart.direction = direction;
			dart.source = this.gameObject;
		}
	}
}
