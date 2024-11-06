using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : Projectile
{

     Rigidbody2D rb;

     public float dmg;

     bool destroying = false;


    // Start is called before the first frame update
    void Start()
    {
        dmg = damage;


        rb = GetComponent<Rigidbody2D>();

        Vector2 Mouseposition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        rb = GetComponent<Rigidbody2D>();

        Vector2 direction = new Vector2(Mouseposition.x - transform.position.x, Mouseposition.y - transform.position.y).normalized;

        rb.velocity = direction.normalized * speed;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, rot + 90);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    public void ToggleMouseLockstate()
    {

    }
    
}
