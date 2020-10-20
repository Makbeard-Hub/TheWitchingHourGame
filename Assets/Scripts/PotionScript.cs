using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionScript : MonoBehaviour {

    public float plusHealthAmnt;
    public float plusMagicAmnt;
    public AudioClip pickupSound;
    new AudioSource audio;
    private GameObject player;
    Player_Controls playerCont;
	// Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        playerCont = player.GetComponent<Player_Controls>();
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other) {
        if (other.tag == "Player") {
            audio.PlayOneShot(pickupSound);
            playerCont.RestoreRestore(plusHealthAmnt, plusMagicAmnt);
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.transform.GetChild(0).gameObject.SetActive(false);           
            Destroy(gameObject, 0.5f);
        }
        
    }
}
