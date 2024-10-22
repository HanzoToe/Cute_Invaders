using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    Transform playerPosition;
    float shotSpeed = 20f;

    Vector3 direction; 

    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
        direction = (playerPosition.position - transform.position).normalized; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shotSpeed * Time.deltaTime * direction;
    }
}
