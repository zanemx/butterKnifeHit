using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

	void Awake(){
//		GameObject.Find ("LevelComplete").SetActive (false);
//		GameObject.Find ("LevelFailed").SetActive (false);
	}

	public void OnStartButtonPressed(){

		if (null != EventManager.instance.onStartButtonPressed)
			EventManager.instance.onStartButtonPressed ();
	}

	public void OnNextLevelPressed(){
		
		if (null != EventManager.instance.onLevelCompleteButtonPressed)
			EventManager.instance.onLevelCompleteButtonPressed ();

//		GameObject.Find ("LevelComplete").SetActive (false);
	}

	public void OnLevelFailedButtonPressed(){

		if (null != EventManager.instance.onLevelFailedButtonPressed)
			EventManager.instance.onLevelFailedButtonPressed ();

//		GameObject.Find ("LevelFailed").SetActive (false);
	}
}