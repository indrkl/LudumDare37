using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dart : MonoBehaviour {

	public GameObject source;
	public bool collided = false;

	public float speed;
	public int direction;
	public int yDirection = 0;

	float collisionTime;
	public float deleteAfterCollision;


	void OnCollisionEnter(Collision coll){
		Debug.Log (coll.collider.name);
		if (!collided) {
			if (coll.collider.GetComponent<Player> ()) {
				coll.collider.GetComponent<Player> ().die ();
				Destroy (gameObject);
			} else if(coll.collider.gameObject != source) {
				collided = true;
				collisionTime = Time.time;
				Destroy (gameObject);
			}
		}
	}

	void Start(){
		if (direction == 1) {
			transform.rotation = Quaternion.Euler (90, 90, 180);
		} else if (direction == -1) {
			transform.rotation = Quaternion.Euler (90, 90, 0);
		} else if (yDirection == 1) {
			transform.rotation = Quaternion.Euler (0, 90, 0);
		} else if (yDirection == -1) {
			transform.rotation = Quaternion.Euler (180, 90, 0);
		}
	}

	bool whoosh = false;

	void Update(){
		if (!collided) {
			this.transform.position += new Vector3 (speed * direction * Time.deltaTime, speed * yDirection * Time.deltaTime, 0);
		} else {
			if (collisionTime + deleteAfterCollision > Time.time) {
				Destroy (gameObject);
			}
		}
		float dist = Mathf.Abs (GameMaster.instance.player.transform.position.x - transform.position.x);
		if (dist < 5 && !whoosh) {
			if (GetComponent<AudioSource> ())
				GetComponent<AudioSource> ().Play ();
			whoosh = true;
		}
	}
}
