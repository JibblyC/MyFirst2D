using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour {


	public string level2Load;

	private PlayerController player;
	private CameraController camera;
	private LevelManager theLevel;

	public float waitToMove;
	public float waitToLoad;

	private bool movePlayer;

	public Sprite flagOpen;

	private SpriteRenderer theSpriteRenderer;

	// Use this for initialization
	void Start () {
		player = FindObjectOfType<PlayerController> ();
		camera = FindObjectOfType<CameraController> ();
		theLevel = FindObjectOfType<LevelManager> ();
		theSpriteRenderer = GetComponent<SpriteRenderer> ();

		
	}
	
	// Update is called once per frame
	void Update () {
		if (movePlayer) {
			player.myRigidbody.velocity = new Vector3 (player.moveSpeed, player.myRigidbody.velocity.y, 0f);
		}
		
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			theSpriteRenderer.sprite = flagOpen;
			StartCoroutine ("levelEndCo");
		}
	}

	public IEnumerator levelEndCo(){
		player.canMove = false;
		camera.followTarget = false;
		theLevel.invincible = true;
		player.myRigidbody.velocity = Vector3.zero;

		yield return new WaitForSeconds (waitToMove);

		movePlayer = true;

		yield return new WaitForSeconds (waitToLoad);

		SceneManager.LoadScene(level2Load);
	}
		
}
