﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioController : MonoBehaviour
{
    private const float maxSpeedWhenHoldKey = 12;
    private const float checkTimeHoldKey = 0.2f;

    //default value setting
    private float velocityWhenPress = 7;
    
    private float velocityJump=450;
    private float velocityFall=5;
    private float smallJump=5;

    private float timeHoldKey = 0;

    private float velocity = 0;
    private bool isOnGround = true;
    private bool isNavigation = false;
    private bool isRight = true;

    private Animator animator;
    private Rigidbody2D r2d;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("velocity", velocity);
        animator.SetBool("isOnGround", isOnGround);
        animator.SetBool("isNavigation", isNavigation);
        OnJump();
        ShootAndSpeed();
    }
    private void FixedUpdate()
    {
        OnMove();
    }
    private void OnMove()
    {
        float velocityKeyInput = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(velocityWhenPress * velocityKeyInput, r2d.velocity.y);
        velocity = Mathf.Abs(velocityWhenPress * velocityKeyInput);
        if (velocityKeyInput > 0 && !isRight) OnDirection();
        if (velocityKeyInput < 0 && isRight) OnDirection();
    }

    private void OnDirection()
    {
        isRight = !isRight;
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
        if (velocityWhenPress > 1f) StartCoroutine(OnNavigation());
    }
    private void OnJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            r2d.AddForce((Vector2.up) * velocityJump);
            isOnGround = false;
        }
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (velocityFall -1)*Time.deltaTime;
        }else if(r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (smallJump - 1) * Time.deltaTime;
        }
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            isOnGround = true;
        }
    }
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Ground")
        {
            isOnGround = true;
        }
    }

    IEnumerator OnNavigation()
    {
        isNavigation = true;
        yield return new WaitForSeconds(0.2f);
        isNavigation = false;
    }


    //bắn đạn và chạy nhanh.
    private void ShootAndSpeed()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            timeHoldKey += Time.deltaTime;
            if (timeHoldKey < checkTimeHoldKey) 
            {

            }
            else
            {
                velocityWhenPress *= 1.01f;
                if (velocityWhenPress >= maxSpeedWhenHoldKey)
                {
                    velocityWhenPress = maxSpeedWhenHoldKey;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Z))
        {
            velocityWhenPress = 7f;
            timeHoldKey = 0f;
        }
    }
}
