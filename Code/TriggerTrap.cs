using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TriggerTrap : MonoBehaviour {
	public float delay;
	public float startDelay;
	public int repeats;

	public abstract void action ();

	public IEnumerator trigger(){
		yield return new WaitForSeconds (startDelay);
		for (int i = 0; i < repeats; i++) {
			action();
			yield return new WaitForSeconds(delay);

		}

	}
}
