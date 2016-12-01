﻿using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour
{
    public GameObject projectile;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Attack()
    {
        var r = Instantiate(projectile);
        r.transform.parent = transform;
        r.transform.localPosition = new Vector3(-5, 0);
        r.transform.parent = null;
        r.GetComponent<Rigidbody2D>().velocity = new Vector2(2, 0);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        var player = collision.GetComponentInParent<Player>();
        if(player != null)
        {
            player.currentWeapon = this;
            this.transform.parent = player.transform;
            this.transform.localPosition = new Vector3(0.5f, 0.35f);
        }
    }
}
