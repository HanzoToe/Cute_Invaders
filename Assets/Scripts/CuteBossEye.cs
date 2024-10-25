using System.Collections;
using UnityEngine;


[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]

public class CuteBossEye : BossesScript
{
    public GameObject bullet;
    GameObject Position;
    public Transform bulletSpawnPoint;
    Transform playerPosition; 

    bool halfHp = false;
    bool isDashing = true;
    bool isShooting = false; 

    Rigidbody2D rb;
    Vector3 direction;

    float originalHp;
  
    SpriteRenderer sr;
    BossLaser bl;

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        originalHp = hp;
    }

    // Start is called before the first frame update
    void Start()
    {
        movementSpeed = 2000f;
    }

    // Update is called once per frame
    void Update()
    {
        FindPLayerPosition();
        FacePlayer();


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

        if(halfHp && !isShooting)
        {
            StartCoroutine("Shoot");
        }
  
    }

    void HpCheck()
    {
        if (hp == originalHp / 2)
        {
            halfHp = true;
            movementSpeed = 2500f;
        }
        else if(hp <= 0)
        {
            Destroy(gameObject);
        }    
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

    void FacePlayer()
    {
        Vector2 direction = ((Vector2)playerPosition.position - (Vector2)transform.position).normalized;
        transform.right = direction.normalized;

        if (direction.x < 0)
        {
            sr.flipX = true;
            sr.flipY = true;
        }
        else if (direction.x > 0)
        {
            sr.flipY = false;
            sr.flipX = true;
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
        isShooting = true; 
        float shotIntervals = 0.1f;
        float originalShotIntervals = shotIntervals;

        Debug.Log(shotIntervals);

        if (!isDashing && playerPosition.gameObject.active) 
        {
            while(shotIntervals > 0f)
            {
                Instantiate(bullet, bulletSpawnPoint.position, playerPosition.localRotation);

                shotIntervals -= 0.1f;
            }

            yield return new WaitForSeconds(0.1f);
            shotIntervals = originalShotIntervals;
        }

        isShooting = false; 

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            bl = collision.gameObject.GetComponent<BossLaser>();

            if (bl != null)
                hp -= bl.dmg;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            bl = collision.gameObject.GetComponent<BossLaser>();

            if (bl != null)
                hp -= bl.dmg;
        }
    }
}

