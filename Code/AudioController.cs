using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour {
	public static AudioController instance;

	void Awake(){
		instance = this;
		playedClip = new List<bool> ();
		for (int i = 0; i < levelStartClips.Count; i++) {
			playedClip.Add (false);
		}
	}

	public List<AudioClip> levelStartClips;
	public AudioClip deathClip;
	List<bool> playedClip;

	public AudioClip startMaxLevel;



	public void startLevel(int level){
		if (levelStartClips.Count >= level && playedClip [level - 1] == false) {
			GameMaster.instance.GetComponent<AudioSource> ().clip = levelStartClips [level - 1];
			GameMaster.instance.GetComponent<AudioSource> ().Play ();
			playedClip [level - 1] = true;
		} else {
			GameMaster.instance.GetComponent<AudioSource> ().clip = deathClip;
			GameMaster.instance.GetComponent<AudioSource> ().Play ();
		}
	}

	public void playStartMaxLevel(){
		if (startMaxLevel) {
			GameMaster.instance.GetComponent<AudioSource> ().clip = startMaxLevel;
			GameMaster.instance.GetComponent<AudioSource> ().Play ();
		}
	}


}
