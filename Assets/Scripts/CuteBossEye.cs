using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CircleCollider2D))]

public class CuteBossEye : BossesScript
{
    GameObject bullet;

    Transform bulletSpawnPoint;
    Transform playerPosition; 

    bool halfHp = false;
    public bool isDashing = true;

    private void Awake()
    {
        playerPosition = GameObject.Find("Player").transform;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HpCheck();

        if(isDashing)
        StartCoroutine("Dash");
        
        
    }

    void HpCheck()
    {
        if (hp == hp * 0.5)
            halfHp = true;
    }

    IEnumerator Dash()
    {
        float _chaseTime = 2f;
        float _timeUntilNextDash = 0.5f;

        Vector2 targetPosition = playerPosition.transform.position;

        float _elapsedtime = 0f; 

        while(_elapsedtime < _chaseTime)
        {
            Vector2 _bossPosition = gameObject.transform.position;

            transform.position = Vector2.MoveTowards(_bossPosition, targetPosition, 0.5f) * movementSpeed * Time.deltaTime;

            _elapsedtime += Time.deltaTime;
 
        }

        isDashing = false;
        yield return new WaitForSeconds(_timeUntilNextDash);

        isDashing = true; 
    }

    IEnumerator Shoot()
    {
        //Make bullets constantly follow the player until the player destroys them! 
        
        float shootWaitTime = 1f;
        Instantiate(bullet, bulletSpawnPoint, bulletSpawnPoint);
        yield return null; 

    }
}
