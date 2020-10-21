using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Combat : MonoBehaviour
{
    [SerializeField] private float currentHealth;
    public float maxHealth;
    public float dealDamageAmnt;
    
    public GameObject[] itemList;
    public float dropPercent;

    public AudioClip hitReceivedSound;
    public AudioClip deathSound;
    new AudioSource audio;

    GameObject gameManagerObject;
    Player_Controls playerCombatScript;
    GameManagement gameManagerScript;

    private bool isDead;

	void Start ()
    {
        currentHealth = maxHealth;
        playerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controls>();
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManagement>();
        audio = GetComponent<AudioSource>();
        isDead = false;
    }

    void Update()
    {
        if (currentHealth <= 0 && !isDead)
        {        
            Death();
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            DealDamage(dealDamageAmnt);           
        }
    }

    public void TakeDamage(float dmg)
    {
        audio.PlayOneShot(hitReceivedSound);
        currentHealth -= dmg;
        if (currentHealth <= 0)
        {
            currentHealth = 0;
        }
    }

    //Destroy game object
    void Death()
    {
        isDead = true;
        audio.PlayOneShot(deathSound);
        gameManagerScript.enemyCount -= 1;
        gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;

        if (gameObject.GetComponent<Collider2D>() != null)
        {
            gameObject.GetComponent<Collider2D>().enabled = false;
        }
        DropItems();
        Destroy(gameObject, 0.5f);
    }

    void DealDamage(float damagePlayer)
    {
        playerCombatScript.TakeDamage(damagePlayer);
    }

    void DropItems()
    {
        //If drop chance gets passed
        if (Random.Range(0f, 1f) <= dropPercent)
        {
            int item = Random.Range(0, itemList.Length);
            Instantiate(itemList[item], transform.position, transform.rotation);
        }
    }
}