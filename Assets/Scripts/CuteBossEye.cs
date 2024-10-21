using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class CuteBossEye : BossesScript
{
    GameObject bullet;

    Transform bulletSpawnPoint;
    Transform playerPosition; 

    bool halfHp = false;
    public bool isDashing = true;

    Rigidbody2D rb;
    Vector3 direction;
    private void Awake()
    {
        playerPosition = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 1000f; 
    }

    // Update is called once per frame
    void Update()
    {
        HpCheck();

        if(isDashing)
        {
            StartCoroutine("Chase");
        }
        else if (!isDashing)
        {
            StartCoroutine("RunAway");
        }
        

        if (playerPosition)
        {
            direction = (playerPosition.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
    }

    void HpCheck()
    {
        if (hp == hp * 0.5)
            halfHp = true;
    }

    IEnumerator Chase()
    {
        float _chaseTime = 1f;
        float _waitTime = 1f;
        Vector2 moveDirection = direction;

        if (playerPosition)
        {                   
            rb.velocity = new Vector2(moveDirection.x, moveDirection.y) * movementSpeed * Time.deltaTime;
        }

        yield return new WaitForSeconds(_chaseTime);

        isDashing = false;

        yield return new WaitForSeconds(_waitTime);

        isDashing = true;
    }

    IEnumerator RunAway()
    {
        Vector3 _direction = (transform.position - playerPosition.position).normalized;
        Vector2 moveAway = _direction;
        float runAwaySpeed = 500f;

        if (playerPosition)
        {
            rb.velocity = new Vector2(moveAway.x, moveAway.y) * runAwaySpeed * Time.deltaTime; 
        }

        yield return null; 
    }

    IEnumerator Shoot()
    {
        //Make bullets constantly follow the player until the player destroys them! 
        
        float shootWaitTime = 1f;
        Instantiate(bullet, bulletSpawnPoint, bulletSpawnPoint);
        yield return null; 

    }
}
