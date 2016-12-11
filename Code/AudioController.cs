using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public static AudioController instance;

	void Awake(){
		instance = this;
	}

	public List<AudioClip> levelStartClips;


	public void startLevel(int level){
		if (levelStartClips.Count >= level) {
			GameMaster.instance.player.GetComponent<AudioSource> ().clip = levelStartClips [level - 1];
			GameMaster.instance.player.GetComponent<AudioSource> ().Play ();
		}
	}
}
