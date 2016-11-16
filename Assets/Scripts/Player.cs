﻿using UnityEngine;
using System.Collections;
using System;
using UnityStandardAssets.CrossPlatformInput;

public class Player : MonoBehaviour {
    const float DEAD_ZONE_HEIGHT = -2;
    public float maxSpeed = 3;
    public float jumpForce = 5;

    private bool isDucking = false;

    private Vector3 startPosition;
    private new Rigidbody2D rigidbody2D;

    // Use this for initialization
    void Start () {
        rigidbody2D = GetComponent<Rigidbody2D>();
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
	    if(transform.position.y < DEAD_ZONE_HEIGHT)
        {
            Die();
        }

        var x_force = CrossPlatformInputManager.GetAxis("Horizontal");
        rigidbody2D.velocity += Vector2.right * x_force;
        rigidbody2D.velocity = Vector2.ClampMagnitude(rigidbody2D.velocity,  maxSpeed);

        if ( CrossPlatformInputManager.GetButtonDown("Jump") && rigidbody2D.velocity.y == 0)
        {
            rigidbody2D.velocity += Vector2.up * jumpForce;
        }

        // Duck if needed
        if ( CrossPlatformInputManager.GetAxis("Vertical") < 0 && !isDucking)
        {
            var s = transform.localScale;
            s.y *= .7f;
            transform.localScale = s;
            isDucking = true;
        }
        if ( CrossPlatformInputManager.GetAxis("Vertical") >= 0 && isDucking)
        {
            var s = transform.localScale;
            s.y /= .7f;
            transform.localScale = s;
            isDucking = false;
        }

        // Flip to look in right direction
        if (rigidbody2D.velocity.x > 0)
        {
            transform.rotation = new Quaternion(0, 0, 0, 0);
        }else if(rigidbody2D.velocity.x < 0)
        {
            transform.rotation = new Quaternion(0, 180, 0, 0);
        }
    }

    public void Die()
    {
        transform.position = startPosition;
        rigidbody2D.velocity = new Vector2();
        FindObjectOfType<GM>().LifeWasLost();
    }
}
