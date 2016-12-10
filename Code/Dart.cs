using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour {

	public GameObject source;
	public bool collided = false;

	public float speed;
	public int direction;

	float collisionTime;
	public float deleteAfterCollision;


	void OnCollisionEnter(Collision coll){
		Debug.Log (coll.collider.name);
		if (!collided) {
			if (coll.other.GetComponent<Player> ()) {
				coll.other.GetComponent<Player> ().die ();
				Destroy (gameObject);
			} else if(coll.other.gameObject != source) {
				collided = true;
				collisionTime = Time.time;
				Destroy (gameObject);
			}
		}
	}

	void Start(){
		if (direction == 1) {
			transform.rotation = Quaternion.Euler (90, 90, 180);
		} else {
			transform.rotation = Quaternion.Euler (90, 90, 0);
		}
	}

	void Update(){
		if (!collided) {
			this.transform.position += new Vector3 (speed * direction * Time.deltaTime, 0, 0);
		} else {
			if (collisionTime + deleteAfterCollision > Time.time) {
				Destroy (gameObject);
			}
		}
	}
}
