using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
	
	public static GameManager instance;
	public int knifeCount = 10;
	public enum State {LAODING,PLAYING,PAUSED,LEVEL_COMPLETE,LEVEL_FAILED};
	public State state = State.LAODING;
	public int score = 0;
	public int level = 1;

	void Awake(){
		if (null == instance)
			instance = this;
		else if (this != instance)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		InitGame ();
	}

	void InitGame(){
		print ("GameManager singleton created");
	}
}
