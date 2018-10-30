using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour {

    public float waitToRespawn;
    public PlayerController thePlayer;
    public GameObject deathSplosionParticleEffect;

    public int coinCount;
    public Text cointText;
	private int coinBonusLifeCount;
	public int bonusLifeThreshold;
	public AudioSource coinSound;

    public Image heart1;
    public Image heart2;
    public Image heart3;

    public Sprite heartFull;
    public Sprite heartHalf;
    public Sprite heartEmpty;

    public int maxHealth;
    public int healthCount;

    private bool respawning;

	private ResetOnRespawn[] objectsToReset;

	public bool invincible;

	public Text livesText;
	public int startingLives;
	public int currentLives;

	public GameObject gameOverScreen;

	public AudioSource levelMusic;
	public AudioSource gameOverMusic;




    // Use this for initialization
    void Start () {
        //FindObjectOfType will find the script (and therfor component) called PlayerController, which is currently attached to the Player object
        //and return the playerobject itself from that find. This is because they are two seperate objects, no hierarchy.
		//This object is found within the currently active scene.
        thePlayer = FindObjectOfType<PlayerController>();

        cointText.text = "Coins : " + coinCount;

        healthCount = maxHealth;

		objectsToReset = FindObjectsOfType<ResetOnRespawn> ();

		currentLives = startingLives;
		livesText.text = "Lives x " + currentLives;
	}
	
	// Update is called once per frame
	void Update () {

        if(healthCount <= 0 && !respawning) {
            respawning = true;
            Respawn();
        }

		if (coinBonusLifeCount >= bonusLifeThreshold) {
			AddLives (1);
			coinBonusLifeCount -= bonusLifeThreshold;
		}
		
	}

    //This method is invoked from within the player script, such that the player can be deactivated on death, invokes the below CoRoutine
    public void Respawn() {
        
		currentLives -= 1;
		livesText.text = "Lives x " + currentLives;

		if (currentLives > 0) {
			StartCoroutine ("RespawnCo");
		} else {
			thePlayer.gameObject.SetActive(false);
			gameOverScreen.SetActive (true);
			levelMusic.Stop ();
			gameOverMusic.Play ();

			//levelMusic.volume = levelMusic.volume / 2f;
		}


    }

    //This is a Co Routine (IEnumerator)
    public IEnumerator RespawnCo() {
        thePlayer.gameObject.SetActive(false);

        Instantiate(deathSplosionParticleEffect, thePlayer.transform.position, thePlayer.transform.rotation);


        yield return new WaitForSeconds(waitToRespawn);

        healthCount = maxHealth;
        respawning = false;
        UpdateHeartMeter();

		coinCount = 0;
		coinBonusLifeCount = 0;
		cointText.text = "Coins : " + coinCount;

        thePlayer.transform.position = thePlayer.respawnPosition;
        thePlayer.gameObject.SetActive(true);

		for (int i = 0; i < objectsToReset.Length; i++) {
			objectsToReset[i].gameObject.SetActive (true);
			objectsToReset[i].ResetObject();
		}
    }

    public void AddCoins(int coinsToAdd) {
        coinCount += coinsToAdd;
        cointText.text = "Coins : " + coinCount;
		coinBonusLifeCount += coinsToAdd;
		coinSound.Play ();
    }

    public void HurtPlayer(int damageToTake) {
		if (!invincible) {
			healthCount -= damageToTake;
			UpdateHeartMeter ();
			thePlayer.KnockBack ();
			thePlayer.hurtSound.Play ();
		}
    }

	public void AddLives(int livesToGive){
		currentLives += livesToGive;
		livesText.text = "Lives x " + currentLives;
		coinSound.Play ();
	}

	public void giveHealth(int healthToGive){

		healthCount += healthToGive;
		if (healthCount > maxHealth) {
			healthCount = maxHealth;
		}
	
		UpdateHeartMeter ();
	}

    public void UpdateHeartMeter() {
        switch (healthCount) {
            case 6: heart1.sprite = heartFull;
                    heart2.sprite = heartFull;
                    heart3.sprite = heartFull;
                return;
            case 5:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartHalf;
                return;
            case 4:
                heart1.sprite = heartFull;
                heart2.sprite = heartFull;
                heart3.sprite = heartEmpty;
                return;
            case 3:
                heart1.sprite = heartFull;
                heart2.sprite = heartHalf;
                heart3.sprite = heartEmpty;
                return;
            case 2:
                heart1.sprite = heartFull;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 1:
                heart1.sprite = heartHalf;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;
            case 0:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

            default:
                heart1.sprite = heartEmpty;
                heart2.sprite = heartEmpty;
                heart3.sprite = heartEmpty;
                return;

        }

    }
}
