using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPumpkinKingMovement : MonoBehaviour
{
    Rigidbody2D rig2d;
    BoxCollider2D playerSensor;
    EnemyBoss_Combat bossCombatScript;
    bool playerInRange;
    float moveTimer;
    Vector2 moveDirection;
    GameObject player;
    bool canIJump;

    public GameObject parentGameObj;
    public float moveTimeDelay;
    public float moveXmag;
    public float moveYmag;

    void Start ()
    {
        rig2d = parentGameObj.GetComponent<Rigidbody2D>();
        playerSensor = GetComponent<BoxCollider2D>();
        moveTimer = 0;
        moveDirection = new Vector2(moveXmag, moveYmag);
        playerInRange = false;
        bossCombatScript = parentGameObj.GetComponent<EnemyBoss_Combat>();
        canIJump = bossCombatScript.canJump;
	}
	
	void Update ()
    {
        moveTimer += Time.deltaTime;
        canIJump = bossCombatScript.canJump;
        if (playerInRange && moveTimer > moveTimeDelay && canIJump)
        {
            CheckMoveDirection();
            moveDirection = new Vector2(moveXmag, moveYmag);
            rig2d.AddForce(moveDirection, ForceMode2D.Impulse);
            moveTimer = 0;            
        }                            
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = true;
            player = other.gameObject;           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            playerInRange = false;
        }
    }

    void CheckMoveDirection()
    {
        if (transform.position.x > player.transform.position.x)
        {
            moveXmag = -Mathf.Abs(moveXmag);
        }
        if (transform.position.x < player.transform.position.x)
        {
            moveXmag = Mathf.Abs(moveXmag);
        }
    }
}