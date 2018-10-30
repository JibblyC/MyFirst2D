using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointController : MonoBehaviour {

    public Sprite flagClosed;
    public Sprite flagOpen;

    private SpriteRenderer theSpriteRenderer;

    public bool checkpointActive;

    // Use this for initialization
    void Start () {
        theSpriteRenderer = GetComponent<SpriteRenderer>();
        theSpriteRenderer.sprite = flagClosed;

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "Player") {
            theSpriteRenderer.sprite = flagOpen;
            checkpointActive = true;
        }    
    }
}
