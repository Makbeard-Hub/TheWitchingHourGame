using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGhostMovement : MonoBehaviour
{
    public GameObject leftMapEdge;
    public GameObject rightMapEdge;
    public float moveSpeed;
    private Rigidbody2D rig2d;

	void Start () {
        //if no speed is selected, default to 1
        if (moveSpeed == 0)
        {
            moveSpeed = 1;
        }
        rig2d = GetComponent<Rigidbody2D>();
        leftMapEdge = GameObject.Find("safety wall");
        rightMapEdge = GameObject.Find("wallend Right");
	}
	
	void FixedUpdate ()
    {
        rig2d.velocity = new Vector2(moveSpeed,0f);
	}

    void OnCollisionEnter2D(Collision2D col)
    {
        //Turn ghost around once hitting edge of map
        if (col.gameObject.name == leftMapEdge.name || col.gameObject.name == rightMapEdge.name)
        {
            moveSpeed *= -1f;
        }        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Turn ghost around once hitting edge of map
        if (other.gameObject.name == leftMapEdge.name || other.gameObject.name == rightMapEdge.name)
        {
            moveSpeed *= -1f;            
        }       
    }
}