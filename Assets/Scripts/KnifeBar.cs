using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnifeBar : MonoBehaviour {

	public GameObject KnifePrefab;
	public GameObject KnifePanel;
	public float knifeScale = 1f;
	public float knifeRotation = 45f;

	private int currentIndex = 0;

	void Start(){

		doInit ();

		EventManager.instance.onKnifeHitTarget += OnKnifeHitTarget;
	}

	public void doInit(){
		
		int childs = transform.childCount;
		for (int i = childs - 1; i > 0; i--){
			GameObject.Destroy(transform.GetChild(i).gameObject);
		}

		currentIndex = 0;
		// load with number of knives in level 
		float y = 200f;
		for (int i = 0; i < GameManager.instance.knifeCount; i++) {

			GameObject knife = Instantiate(KnifePrefab);
			knife.transform.SetParent(KnifePanel.transform,false);

			Vector2 pos = new Vector2(0, y);
			knife.GetComponent<RectTransform> ().anchoredPosition = pos;

			y -= 40f;
		}
	}

	void OnKnifeHitTarget(){

		// remove knife
		GameObject child = transform.GetChild (currentIndex).gameObject;
//		child.GetComponent<Image> ().sprite = null;
		child.SetActive(false);

		currentIndex++;

		if (currentIndex == GameManager.instance.knifeCount) {
			EventManager.instance.onKnifeHitTarget -= OnKnifeHitTarget;
			print ("level complete");
		}

	}

}
