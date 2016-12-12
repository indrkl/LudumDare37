using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour {


	void Update(){
		float volume = Mathf.Max (0.07f, Mathf.Min (1, (GameMaster.instance.player.transform.position.y + 5) / 15) );
		GetComponent<AudioSource> ().volume = volume;
	}
}
