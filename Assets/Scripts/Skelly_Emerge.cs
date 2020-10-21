using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skelly_Emerge : MonoBehaviour
{
    private Animator anim;

	void Start ()
    {
        anim = GetComponentInParent<Animator>();
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player") {
            anim.enabled = true;
        }
    }
}