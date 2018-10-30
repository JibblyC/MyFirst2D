using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StompOnEnemy : MonoBehaviour {


	public GameObject deathSplosion;

	private Rigidbody2D playerRigidBody;
	public float bounceForce;
	// Use this for initialization
	void Start () {
		playerRigidBody = transform.parent.GetComponent<Rigidbody2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnTriggerEnter2D(Collider2D collision) {
		if (collision.tag == "Enemy") {
			//Destroy (collision.gameObject);
			collision.gameObject.SetActive (false);
			Instantiate (deathSplosion, collision.transform.position,collision.transform.rotation);

			playerRigidBody.velocity = new Vector3 (playerRigidBody.velocity.x, bounceForce, 0);
		}
	}   
}
