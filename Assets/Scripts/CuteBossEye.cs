using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class CuteBossEye : BossesScript
{
    public GameObject bullet;

    public Transform bulletSpawnPoint;
    Transform playerPosition; 

    bool halfHp = false;
    public bool isDashing = true;

    Rigidbody2D rb;
    Vector3 direction;

    float originalHp; 

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        originalHp = hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 2000f;
        hp /= 2;
    }

    // Update is called once per frame
    void Update()
    {
        FindPLayerPosition();


        Debug.Log(hp);
        Debug.Log(originalHp);

        HpCheck();

        if(isDashing)
        {
            StartCoroutine("Chase");
        }
        else if (!isDashing)
        {
            StartCoroutine("RunAway");
        }

        if(halfHp)
        {
            StartCoroutine("Shoot");
        }
  
    }

    void HpCheck()
    {
        if (hp == originalHp / 2)
            halfHp = true;
    }

    void FindPLayerPosition()
    {
        if (playerPosition)
        {
            direction = (playerPosition.position - transform.position).normalized;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            rb.rotation = angle;
        }
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
        if (!isDashing)
        {
            Instantiate(bullet, bulletSpawnPoint.position, Quaternion.identity);     
        }

        yield return null; 

    }
}
