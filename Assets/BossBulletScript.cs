using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBulletScript : MonoBehaviour
{
    Transform playerPosition;
    float shotSpeed = 20f;
    float timeBeforeDestroy = 1f;

    Vector3 direction; 
   

    // Start is called before the first frame update
    void Start()
    {
        if(playerPosition == null)
        {
            try
            {
                playerPosition = GameObject.Find("Player").transform;
                direction = (playerPosition.position - transform.position).normalized;
            }
            catch
            {
                Debug.Log("No player");
            }
        }
          
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += shotSpeed * Time.deltaTime * direction;
        timeBeforeDestroy -= Time.deltaTime;

        if(timeBeforeDestroy <= 0)
        {
            Destroy(gameObject);
        }

    }

    
}
