using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTimer : Obstacle {

	float levelStartTime;
	public float startTime;
	public float endTime;

	public float objectDissapearanceDelay;

	public Transform startPoint;
	public Transform endPoint;

	public float dartFrequency;
	public float fireRateMin;
	public float fireRateMax;

	List<Vector3> dartPositions;
	List<float> nextFireTime;



	public override void atStart ()
	{
		levelStartTime = Time.time;
		for (int i = 0; i < dartPositions.Count; i++) {
			nextFireTime [i] = levelStartTime + startTime + (dartPositions[i].x - startPoint.position.x) / (endPoint.position.x - startPoint.position.x) * endTime + Random.Range(fireRateMin, fireRateMax); 
		}
	}

	List<Obstacle> otherObstacles;
	List<float> dissapearanceTime;
		
	void Awake(){
		otherObstacles = new List<Obstacle> ();
		dissapearanceTime = new List<float> ();
		dartPositions = new List<Vector3> ();
		nextFireTime = new List<float> ();
		foreach(Obstacle o in GameObject.FindObjectsOfType(typeof(Obstacle))){
			if (o != this){
				otherObstacles.Add (o);
				dissapearanceTime.Add(startTime + ((o.transform.position.x - startPoint.position.x)/(endPoint.position.x - startPoint.position.x))*endTime + objectDissapearanceDelay);
			}
		}

		for (float x = startPoint.position.x; x < endPoint.position.x; x += dartFrequency) {
			dartPositions.Add(new Vector3(x, startPoint.position.y, startPoint.position.z));
			nextFireTime.Add (0);
		}
		atStart ();
	}

	// Update is called once per frame
	void Update () {
		for (int i = 0; i < otherObstacles.Count; i++) {
			if (levelStartTime + dissapearanceTime [i] < Time.time) {
				otherObstacles [i].gameObject.SetActive (false);
			}
		}

		for (int i = 0; i < dartPositions.Count; i++) {
			if (nextFireTime [i] < Time.time) {
				Dart d = Instantiate (GameMaster.instance.dartPrefab);
				d.transform.position = dartPositions [i];
				d.direction = 0;
				d.yDirection = -1;

				nextFireTime [i] = Time.time + Random.Range (fireRateMin, fireRateMax);
			}
		}


	}
}
