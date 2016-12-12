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
	public List<AudioClip> deathClip;
	List<bool> playedClip;

	public AudioClip startMaxLevel;



	public void startLevel(int level){
		if (levelStartClips.Count >= level && playedClip [level - 1] == false) {
			StartCoroutine (playCongAndMessage (levelStartClips[level - 1]));
			playedClip [level - 1] = true;
		} else if (deathClip.Count > 0){
			StartCoroutine (playCongAndMessage (deathClip[Random.Range(0, deathClip.Count)]));
		}
	}

	public void playStartMaxLevel(){
		if (startMaxLevel) {
			StartCoroutine (playCongAndMessage (startMaxLevel));
		}
	}

	public AudioClip cong;

	public IEnumerator playCongAndMessage(AudioClip msg){
		GetComponent<AudioSource> ().clip = cong;
		GetComponent<AudioSource> ().Play ();
		yield return new WaitForSeconds (2);
		GetComponent<AudioSource> ().clip = msg;
		GetComponent<AudioSource> ().Play ();
	}


}
