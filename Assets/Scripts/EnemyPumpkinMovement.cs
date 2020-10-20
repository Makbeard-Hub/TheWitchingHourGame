using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPumpkinMovement : MonoBehaviour {
    Rigidbody2D rig2d;
    CircleCollider2D playerSensor;
    bool playerInRange;
    float moveTimer;
    Vector2 moveDirection;
    GameObject player;

    public GameObject parentGameObj;
    public float moveTimeDelay;
    public float moveXmag;
    public float moveYmag;

    // Use this for initialization
    void Start () {
        rig2d = parentGameObj.GetComponent<Rigidbody2D>();
        playerSensor = GetComponent<CircleCollider2D>();
        moveTimer = 0;
        moveDirection = new Vector2(moveXmag, moveYmag);
        playerInRange = false;  
	}
	
	// Update is called once per frame
	void Update () {
        moveTimer += Time.deltaTime;
        if (playerInRange && moveTimer > moveTimeDelay) {
            CheckMoveDirection();
            moveDirection = new Vector2(moveXmag, moveYmag);
            rig2d.AddForce(moveDirection, ForceMode2D.Impulse);
            moveTimer = 0;            
        }              
    }

    void OnTriggerStay2D(Collider2D other) {
        if (other.tag == "Player") {
            playerInRange = true;
            player = other.gameObject;           
        }
    }

    void OnTriggerExit2D(Collider2D other) {
        if (other.tag == "Player") {
            playerInRange = false;
        }
    }

    void CheckMoveDirection() {
        if (transform.position.x > player.transform.position.x)
        {
            moveXmag = -Mathf.Abs(moveXmag);
            
        }
        if (transform.position.x < player.transform.position.x)
        {
            moveXmag = Mathf.Abs(moveXmag);
            
        }
        //Debug.DrawRay(transform.right, player.transform.position, Color.red, 3f);
    }
}
