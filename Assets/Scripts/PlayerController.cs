using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float moveSpeed;
	private float activeMoveSpeed;
    private Rigidbody2D myRigidbody;

    public float jumpSpeed;

    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;

    public bool isGrounded;

    private Animator myAnim;

    public Vector3 respawnPosition;

    public LevelManager theLevelManager;

	public GameObject stompBox;

	public float knockBackForce;
	public float knockBackLength;
	private float knockBackCounter;

	public float invincibilityLength;
	private float invincibilityCounter;

	public AudioSource jumpSound;
	public AudioSource hurtSound;

	private bool onPlatform;
	public float onPlatformSpeedModifier;

	void Start () {
        myRigidbody = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
        theLevelManager = FindObjectOfType<LevelManager>();
        respawnPosition = transform.position;

		activeMoveSpeed = moveSpeed;
		
	}
	
	// Update is called once per frame
	void Update () {

		isGrounded = Physics2D.OverlapCircle (groundCheck.position, groundCheckRadius, whatIsGround);
		myAnim = GetComponent<Animator> ();

		if (knockBackCounter <= 0) {

			if (onPlatform) {
				activeMoveSpeed = moveSpeed * onPlatformSpeedModifier;
			} else {
				activeMoveSpeed = moveSpeed;
			}

			if (Input.GetAxisRaw ("Horizontal") > 0f) {
				myRigidbody.velocity = new Vector3 (activeMoveSpeed, myRigidbody.velocity.y, 0f);
				transform.localScale = new Vector3 (1f, 1f, 1f);
			} else if (Input.GetAxisRaw ("Horizontal") < 0f) {
				myRigidbody.velocity = new Vector3 (-activeMoveSpeed, myRigidbody.velocity.y, 0f);
				transform.localScale = new Vector3 (-1f, 1f, 1f);
			} else {
				myRigidbody.velocity = new Vector3 (0f, myRigidbody.velocity.y, 0f);
			}


			if (Input.GetButtonDown ("Jump") && isGrounded) {
				myRigidbody.velocity = new Vector3 (myRigidbody.velocity.x, jumpSpeed, 0f);
				jumpSound.Play ();
			}


		}

		if (knockBackCounter > 0) {
			knockBackCounter -= Time.deltaTime;

			//Facing Right
			if (transform.localScale.x > 0) {
				myRigidbody.velocity = new Vector3 (-knockBackForce, knockBackForce, 0);
			//Facing Left
			} else {
				myRigidbody.velocity = new Vector3 (knockBackForce, knockBackForce, 0);
			}

		}

		if (invincibilityCounter > 0) {
			invincibilityCounter -= Time.deltaTime;
		}

		if (invincibilityCounter <= 0) {
			theLevelManager.invincible = false;
		}
			myAnim.SetFloat ("Speed", Mathf.Abs (myRigidbody.velocity.x));
			myAnim.SetBool ("Grounded", isGrounded);

			if (myRigidbody.velocity.y < 0) {
				stompBox.SetActive (true);
			} else {
				stompBox.SetActive (false);
			}
		
	}

	public void KnockBack(){
		knockBackCounter = knockBackLength;
		invincibilityCounter = invincibilityLength;
		theLevelManager.invincible = true;
	}

    void OnTriggerEnter2D(Collider2D collision) {
        if(collision.tag == "KillPlane") {

            theLevelManager.Respawn();

            //gameObject.SetActive(false);
            //transform.position = respawnPosition;
        }

        if(collision.tag == "Checkpoint") {
            respawnPosition = collision.transform.position;
        }

    }

    void OnCollisionEnter2D(Collision2D collision) {
        if(collision.gameObject.tag == "MovingPlatform") {
            transform.parent = collision.transform;
			onPlatform = true;
        }

}

    void OnCollisionExit2D(Collision2D collision) {
        if (collision.gameObject.tag == "MovingPlatform") {
            transform.parent = null;
			onPlatform = false;
        }

    }
}
