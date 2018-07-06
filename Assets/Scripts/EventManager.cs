using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour {

	public static EventManager instance = null;

	public delegate void OnKnifeHitTarget();
	public OnKnifeHitTarget onKnifeHitTarget;

	public delegate void OnLevelComplete();
	public OnKnifeHitTarget onLevelComplete;

	public delegate void OnStartButtonPressed();
	public OnStartButtonPressed onStartButtonPressed;

	public delegate void OnLevelCompleteButtonPressed();
	public OnLevelCompleteButtonPressed onLevelCompleteButtonPressed;

	public delegate void OnLevelFailedButtonPressed();
	public OnLevelFailedButtonPressed onLevelFailedButtonPressed;

	void Awake(){
		if (null == instance)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

		DontDestroyOnLoad (gameObject);

		Init ();
	}

	void Init(){
		print ("init EventManager");
	}
}
