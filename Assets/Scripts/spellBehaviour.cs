using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spellBehaviour : MonoBehaviour {
    //private GameObject playerWitch;
    private Player_Controls pl_controls;
    private float damageAmnt;
    private Vector2 startLoc;
    private Vector2 endLoc;
    private Vector2 deltaLoc;
    private float lifeTime;
    public float maxLifeTime;
    public float velocity;
    private TrailRenderer trailRen;
    private AudioSource audio;

    // Use this for initialization
    void Start () {
        pl_controls = GameObject.FindGameObjectWithTag("Player").GetComponent<Player_Controls>();
        damageAmnt = pl_controls.spellDamage;
        startLoc = pl_controls.targetStart.position;
        endLoc = pl_controls.targetEnd;
        lifeTime = 0f;
        trailRen = GetComponent<TrailRenderer>();
        audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        deltaLoc = (endLoc - startLoc).normalized;
        /*if (Vector2.SqrMagnitude(endLoc - startLoc) < 50)
        {

            velocity *= 1.05f;
        }*/

        transform.Translate((deltaLoc)*Time.deltaTime*velocity);
        lifeTime += Time.deltaTime;
        if (lifeTime >= maxLifeTime) {
            Destroy(gameObject);
        } 
          
	}

    void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_Combat>().TakeDamage(damageAmnt);
        }
        if (other.gameObject.tag == "Boss") {
            other.gameObject.GetComponent<EnemyBoss_Combat>().TakeDamage(damageAmnt);
        }
            trailRen.enabled = false;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            StartCoroutine(PlayImpactSound());
            //Destroy(gameObject);        
    }

     IEnumerator PlayImpactSound()
    {
        
        audio.Play();
        
        
        yield return new WaitForSeconds(1f);
        
        
    }

    void Destroy() {

    }
}
