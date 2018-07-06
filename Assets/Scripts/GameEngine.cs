using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameEngine : MonoBehaviour {

	public GameObject levelFailedView;
	public GameObject levelCompleteView;
	public GameObject menuView;
	public GameObject knifePrefab;
	public float knifeSpeed = 1f;
	public Transform knifeStartTransform;
	public float knifeDistanceThreshold = 2.0f;
	public int throws = 8;

	private List<GameObject> knives;
	private GameObject target;
	private GameObject currentKnife = null;

	void Awake(){
		levelFailedView.SetActive (false);
		levelCompleteView.SetActive (false);
	}

	// Use this for initialization
	void Start () {
		knives = new List<GameObject> ();
		target = GameObject.Find ("Target");

		GameManager.instance.state = GameManager.State.LAODING;

		EventManager.instance.onStartButtonPressed += onStartButtonPressed;
		EventManager.instance.onLevelCompleteButtonPressed += onLevelCompleteButtonPressed;
		EventManager.instance.onLevelFailedButtonPressed += OnPlayAgainButtonPressed;

		throws = GameManager.instance.knifeCount;
	}

	void onStartButtonPressed(){
		EventManager.instance.onStartButtonPressed -= onStartButtonPressed;
		GameManager.instance.state = GameManager.State.PLAYING;

		menuView.SetActive (false);

		SpawnKnife ();
	}

	void SpawnKnife(){
		// get start 
		Vector3 start = knifeStartTransform.position + new Vector3(0,-4f,0);
		GameObject knife = Instantiate (knifePrefab, start, Quaternion.identity , transform);
//		knife.transform.Rotate (new Vector3 (0, 0, 45f));

		knives.Add (knife);

		currentKnife = knife;
	}

	void OnKnifeHit(GameObject knife){

		// set distance from target 
		Vector3 pos = new Vector3(knife.transform.position.x, 0.5f, knife.transform.position.z);
		knife.transform.position = pos;

		knife.transform.parent = target.transform;

		// remove from knives list 
		knives.Remove(knife);

		SpawnKnife ();

		if (null != EventManager.instance.onKnifeHitTarget)
			EventManager.instance.onKnifeHitTarget(); 
	}

	void Update () {

		if (GameManager.instance.state != GameManager.State.PLAYING)
			return;

		if (Input.GetMouseButtonDown (0)) {
			
			currentKnife.GetComponent<Knife> ().waiting = false;

			throws--;


			// TODO - move this to collision code 
			GameManager.instance.score += 100;
			GameObject.Find ("score").GetComponent<Text> ().text = "Score " + GameManager.instance.score.ToString();

			CheckLevelComplete ();
		}

		for (int i = 0; i < knives.Count; i++) {

			GameObject knife = knives [i];

			// removed from knife script if this knife hits another 
			if (knife.GetComponent<Knife>().alive == false){
				knife.transform.parent = null;
				knives.RemoveAt (i);
				Destroy (knife);
				SpawnKnife ();
				print ("removing knife");
				continue;
			}
			
			bool waiting = knife.GetComponent<Knife> ().waiting;
			if (!waiting) {
				
				float dist = Vector3.Distance (knife.transform.position, target.transform.position);
				if (dist >= knifeDistanceThreshold) {

					// move knife to target 
					float step = Time.deltaTime * knifeSpeed;
					knife.transform.position = Vector3.MoveTowards (knife.transform.position, target.transform.position, step);

				} else if (knife.transform.parent != target.transform) {
					OnKnifeHit (knife);
				}

			} else{
				
				// check dist to knife start trans
				float dist = Vector3.Distance(knife.transform.position,knifeStartTransform.position);
				if (dist > 0) {
					
					float step = Time.deltaTime * knifeSpeed;
					// move knife to initial position
					knife.transform.position = Vector3.MoveTowards (knife.transform.position, knifeStartTransform.position, step);
				}
			}
		}
	}

	// check game over 
	void CheckLevelComplete(){
		print (throws);
		if (throws == 0) {
			levelCompleteView.SetActive (true);
			GameManager.instance.state = GameManager.State.LEVEL_COMPLETE;
		}
	}
	void OnLevelFailed(){
		GameManager.instance.level = 0;
		levelFailedView.SetActive (true);
	}
	void OnPlayAgainButtonPressed(){
		print ("play again");

		GameManager.instance.level = 1;
		GameObject.Find ("level").GetComponent<Text> ().text = "Level " + GameManager.instance.level.ToString ();

		GameManager.instance.score = 0;
		GameObject.Find ("score").GetComponent<Text> ().text = "Score " + GameManager.instance.score.ToString();


		menuView.SetActive (true);
	}

	void onLevelCompleteButtonPressed(){
		// reset / increment level 

		// reset knives 
		KnifeBar bar = GameObject.Find("KnifePanel").GetComponent<KnifeBar>();
		bar.doInit ();

		throws = GameManager.instance.knifeCount;

		GameManager.instance.level++;
		GameObject.Find ("level").GetComponent<Text> ().text = "Level " + GameManager.instance.level.ToString ();

		levelCompleteView.SetActive (false);

		GameManager.instance.state = GameManager.State.PLAYING;
	}
}
