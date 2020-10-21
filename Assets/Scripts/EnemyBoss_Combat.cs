using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyBoss_Combat : MonoBehaviour
{
    public GameObject healthMeter;
    [SerializeField] private float currentHealth;
    public float maxHealth;
    public Text bossTextHealth;
    public float dealDamageAmnt;
    public bool canJump;
    GameObject gameManagerObject;
    Player_Controls playerCombatScript;
    GameManagement gameManagerScript;

    public AudioClip hitReceivedSound;
    public AudioClip deathSound;
    new AudioSource audio;

    private Scrollbar healthSlider;
    
    private bool isDead;

    void Start ()
    {
        currentHealth = maxHealth;
        playerCombatScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controls>();
        gameManagerObject = GameObject.Find("GameManager");
        gameManagerScript = gameManagerObject.GetComponent<GameManagement>();
        audio = GetComponent<AudioSource>();
        healthMeter = GameObject.Find("Boss Health");
        healthSlider = healthMeter.GetComponent<Scrollbar>();
        healthSlider.value = (currentHealth / maxHealth);
        bossTextHealth = GameObject.Find("Boss Text").GetComponent<Text>();
        bossTextHealth.text = currentHealth.ToString();
        isDead = false;
    }
	
	void Update ()
    {
        if (currentHealth <= 0 && !isDead)
            Death();

        healthSlider.value = (currentHealth / maxHealth);
        bossTextHealth.text = currentHealth.ToString();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            DealDamage(dealDamageAmnt);
        }
        canJump = true;
    }

    public void TakeDamage(float dmg)
    {
        audio.PlayOneShot(hitReceivedSound);
        currentHealth -= dmg;
        if (currentHealth < 0)
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
        gameManagerScript.bossUI.SetActive(false);

        Destroy(gameObject, 0.5f);
    }

    void DealDamage(float damagePlayer)
    {
        playerCombatScript.TakeDamage(damagePlayer);
    }
}