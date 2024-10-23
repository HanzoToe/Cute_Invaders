using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : Projectile
{


    // Start is called before the first frame update
    void Start()
    {
        MoveTowardsMousePos();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void MoveTowardsMousePos()
    {
        //Set the "direction" to the mouseposition

        Vector2 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        direction = (mousepos - (Vector2)transform.position).normalized;

        transform.position = direction * speed * Time.deltaTime; 
        
    }
}
