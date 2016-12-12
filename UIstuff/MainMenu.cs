﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {
	public string gameSceneName;

	public void startGame(){
		SceneManager.LoadScene (gameSceneName);
	}

	public void credits(){
		SceneManager.LoadScene ("credits");
	}

	void Update(){
		if (Input.GetKeyDown (KeyCode.Escape)) {
			Application.Quit ();
		}
	}


}
