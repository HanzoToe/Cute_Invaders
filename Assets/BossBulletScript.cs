using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    Transform playerPosition;
    float chasingSpeed = 5f;

    Vector3 direction; 

    // Start is called before the first frame update
    void Start()
    {
        direction = transform.right; 
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += chasingSpeed * Time.deltaTime * direction;
    }
}
