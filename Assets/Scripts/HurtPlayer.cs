﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtPlayer : MonoBehaviour {


    private LevelManager theLevelManager;

    public int damageToGive;
    
    // Use this for initialization
	void Start () {

        theLevelManager = FindObjectOfType<LevelManager>();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Player") {
            //theLevelManager.Respawn();
            theLevelManager.HurtPlayer(damageToGive);
        }   
    }

    void OnTriggerEnter(Collider collision) {
        if(collision.tag == "KillPlane") {
            Destroy(gameObject);
        }
            
    }
}
