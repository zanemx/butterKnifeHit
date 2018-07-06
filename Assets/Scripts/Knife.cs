using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knife : MonoBehaviour {

	public bool waiting = true;
	public bool alive = true;

	void OnTriggerEnter(Collider col){
		alive = false;
//		print ("colider enter");
	}
}
