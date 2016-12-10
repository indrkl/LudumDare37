using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMaster : MonoBehaviour {

	public int maxLevel;
	public int curLevel;

	public Player player;
	public Transform playerSpawn;

	public List<Obstacle> obstacles;

	public void InitializeGame (){
		deathCounter = 0;
		curLevel = 1;
		StartLevel (maxLevel);
	}

	public void StartLevel(int levelNumber){
		foreach(Obstacle o in obstacles){
			if (o.fromLevel <= levelNumber) {
				o.gameObject.SetActive (true);
			} else {
				o.gameObject.SetActive (false);
			}
		}

		player.transform.position = playerSpawn.position;
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
	}
}
