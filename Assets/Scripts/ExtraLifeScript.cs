using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLifeScript : MonoBehaviour {

	public int livesToGive;

	private LevelManager theLevelManager;

	// Use this for initialization
	void Start () {
		theLevelManager = FindObjectOfType<LevelManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision){
		if (collision.tag == "Player") {
			theLevelManager.AddLives (livesToGive);
			gameObject.SetActive (false);
		}
	}
}
