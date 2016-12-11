using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public static GameMaster instance;

	public Dart dartPrefab;

	void Awake(){
		instance = this;
	}

	public int maxLevel;
	public int curLevel;

	public float gravity;

	public Player player;
	public Transform playerSpawn;

	public List<Obstacle> obstacles;

	public void InitializeGame (){
		deathCounter = 0;
		curLevel = 1;
		StartLevel (maxLevel, true);
		obstacles.RemoveRange (0, obstacles.Count);
		foreach (Obstacle o in GameObject.FindObjectsOfType(typeof(Obstacle))) {
			obstacles.Add (o);
		}
	}

	public void StartLevel(int levelNumber, bool startMaxLevel = false){
		foreach(Obstacle o in obstacles){
			if (o.fromLevel <= levelNumber) {
				o.gameObject.SetActive (true);
				o.atStart ();
			} else {
				o.gameObject.SetActive (false);
			}
		}

		foreach (Dart d in GameObject.FindObjectsOfType(typeof(Dart))) {
			Destroy (d.gameObject);
		}

		player.transform.position = playerSpawn.position;
		player.GetComponent<Rigidbody> ().velocity = Vector3.zero;

		if (!startMaxLevel) {
			AudioController.instance.startLevel (levelNumber);
		} else {
			AudioController.instance.playStartMaxLevel ();
		}
	}

	public int deathCounter;

	public void OnDeath(){
		deathCounter++;
		StartLevel (curLevel);
	}

	public void OnSuccess(){
		curLevel++;
		if (curLevel > maxLevel) {
			WinGame ();
		} else {
			StartLevel (curLevel);
		}
	}

	public void WinGame(){

	}

	void Start(){
		InitializeGame ();
		Physics.gravity = new Vector3 (0, gravity, 0);
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}
}
