using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour {


    private LevelManager theLevelManager;

    public int CoinValue;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        theLevelManager = FindObjectOfType<LevelManager>();
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            //Destroy(gameObject);
			gameObject.SetActive (false);

            theLevelManager.AddCoins(CoinValue);
        }    
    }
}
