using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemies : MonoBehaviour
{
    GameObject mario;

    private void Awake()
    {
        mario = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x >0 || collision.contacts[0].normal.x < 0))
        {
            if(mario.GetComponent<MarioController>().level == 0)
            {
                mario.GetComponent<MarioController>().DestroyMario();
            }
            else
            {
                mario.GetComponent<MarioController>().level -= 1;
                mario.GetComponent<MarioController>().isChangeMario = true ;
            }
        }
    }
}
