using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaser : Projectile
{


    // Start is called before the first frame update
    void Start()
    {
        transform.position = transform.forward * speed; 
    }

    // Update is called once per frame
    void Update()
    {
        
    }



}
