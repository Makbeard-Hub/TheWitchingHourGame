using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
public class Player_Controls : MonoBehaviour
{
    public GameObject sliderGO;
    public GameObject magicMeter;
    public GameObject spellEffect1;
    public GameObject witchSprites;
    public GameObject gameManGO;
    public Transform targetStart;
    public Vector2 targetEnd;
    public AudioClip spellAttackSound;
    public AudioClip playerHitTakenSound;
    public AudioClip playerDeathSound;
    public Camera mainCamera;
    public Text myHealth;
    public Text myMagic;
    public float maxPlayerHealth;
    public float maxPlayerMagic;
    public float spellDamage;
    public float spellCost;
    public float manaRegen;
    public float attackTime;
    public bool hasWand; 

    private Animator my_anim;
    private Transform respawnLocation;
    private Vector2 targetStart2d;
    private new AudioSource audio;
    private Scrollbar playerHealthSlider;
    private Scrollbar playerMagicSlider;
    [SerializeField]  private float playerHealth;
    private float playerMagic;
    private float attackTimer;
    private float manaTimer;
    private bool doAttack;
    private bool isReviving;    

    Platformer2DUserControl playerMoveControl;
    GameManagement gManager;

	void Start ()
    {
        manaTimer = 0f;
        attackTimer = 0f;
        my_anim = GetComponent<Animator>();
        hasWand = true;
        targetStart2d = targetStart.position;
        playerHealth = maxPlayerHealth;
        playerMagic = maxPlayerMagic;
        isReviving = false;
        gManager = gameManGO.GetComponent<GameManagement>();
        playerMoveControl = GetComponent<Platformer2DUserControl>();
        respawnLocation = GameObject.FindGameObjectWithTag("Respawn").transform;
        audio = GetComponent<AudioSource>();
        myHealth.text = playerHealth.ToString();
        myMagic.text = playerMagic.ToString();
        
        playerHealthSlider = sliderGO.GetComponent<Scrollbar>();
        playerHealthSlider.value = (playerHealth / maxPlayerHealth);
        playerMagicSlider = magicMeter.GetComponent<Scrollbar>();
        playerMagicSlider.value = (playerMagic / maxPlayerMagic);
    }

    void Update()
    {
        targetStart2d = targetStart.position;
        my_anim.SetBool("Attack", false);
        if (Input.GetButtonDown("Fire1") && hasWand && attackTimer >= attackTime)
        {
            my_anim.SetTrigger("Attack");
            Attack();
        }

        if (!isReviving && playerHealth <= 0)
        {
            Death();
        }

        playerMagicSlider.value = (playerMagic / maxPlayerMagic);
        attackTimer += Time.deltaTime;

        if (manaTimer / 0.5f >= 1f)
        {
            playerMagic += manaRegen;
            manaTimer = 0f;
        }

        manaTimer += Time.deltaTime;

        if (playerMagic <= 0f)
        {
            playerMagic = 0f;
        }

        if (playerMagic >= maxPlayerMagic)
        {
            playerMagic = maxPlayerMagic;
        }

        myMagic.text = playerMagic.ToString();
        playerMagicSlider.value = (playerMagic / maxPlayerMagic);

        myHealth.text = playerHealth.ToString();
        playerHealthSlider.value = (playerHealth / maxPlayerHealth);
    }

    void Attack()
    {
        if (playerMagic < spellCost) {
            return;
        }

        //Find where to aim the shot based on mouse position
        Vector3 screenToMouseCursor = mainCamera.GetComponent<Camera>().ScreenToWorldPoint(Input.mousePosition);        
        targetEnd = new Vector2(screenToMouseCursor.x, screenToMouseCursor.y);

        //If facing right and aiming right
        if (transform.localScale.x > 0)
        {
            if (Vector2.Dot(Vector2.right, targetEnd - targetStart2d) > 0)
            {
                Instantiate(spellEffect1, targetStart.position, targetStart.localRotation);
                audio.PlayOneShot(spellAttackSound);
            }
        }

        //If facing left and aiming left
        else if (transform.localScale.x < 0) {
            if (Vector2.Dot(Vector2.right, targetEnd - targetStart2d) < 0) {
                Instantiate(spellEffect1, targetStart.position, targetStart.localRotation);
                audio.PlayOneShot(spellAttackSound);
            }
        }
        playerMagic -= spellCost;
        attackTimer = 0;                   
    }

    public void TakeDamage(float dmg)
    {
        audio.PlayOneShot(playerHitTakenSound);
        playerHealth -= dmg;
        if(playerHealth < 0)
        {
            playerHealth = 0;
        }
        myHealth.text = playerHealth.ToString();
        playerHealthSlider.value = (playerHealth/maxPlayerHealth);
    }

    public void RestoreRestore(float health, float magic)
    {
        playerHealth += health;

        if (playerHealth >= maxPlayerHealth)
        {
            playerHealth = maxPlayerHealth;
        }

        playerMagic += magic;

        if (playerMagic >= maxPlayerMagic)
        {
            playerMagic = maxPlayerMagic;
        }
    }

    //Disable/enable necessary things upon "Death"
    void Death()
    {
        isReviving = true;
        playerMoveControl.enabled = false;
        witchSprites.SetActive(false);
        audio.PlayOneShot(playerDeathSound);
        StartCoroutine(DeathPause());
    }

    //Delay while the player sees the death screen
    IEnumerator DeathPause()
    {
        yield return new WaitForSeconds(2.5f);

        ResetLevel();
        Revive();
    }

    //kill off all enemies for fresh start of current level 
    void ResetLevel()
    {
        GameObject[] gobs = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject target in gobs) {
            Destroy(target);
        }
        GameObject[] gobs2 = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject target in gobs2)
        {
            Destroy(target);
        }
        GameObject[] gobs3 = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject target in gobs3)
        {
            Destroy(target);
        }
        gManager.bigFire.SetActive(true);
    }

    //Return the player to life
    //Reactive/deactive necessary items
    //Return player to previous checkpoint
    void Revive()
    {
        playerHealth = maxPlayerHealth;
        myHealth.text =  playerHealth.ToString();
        playerMagic = maxPlayerMagic / 2f;
        myMagic.text = playerMagic.ToString();
       
        playerHealthSlider.value = (playerHealth / maxPlayerHealth);
        isReviving = false;
        gameObject.transform.position = respawnLocation.transform.position;

        witchSprites.SetActive(true);
        playerMoveControl.enabled = true;
    }
}