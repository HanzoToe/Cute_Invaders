using System.Collections;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]

public class CuteBossEye : BossesScript
{
    GameObject bullet;

    Transform bulletSpawnPoint;
    Transform playerPosition; 

    bool halfHp = false;

    private void Awake()
    {
       
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HpCheck();
    }

    void HpCheck()
    {
        if (hp == hp * 0.5)
            halfHp = true;
    }

    void Dash()
    {
        Vector2 _playerPosition = playerPosition.transform.position;
        Vector2 _bossPosition = gameObject.transform.position;

        transform.position = (_playerPosition - _bossPosition * movementSpeed) * Time.deltaTime;

    }

    IEnumerator Shoot()
    {
        //Make bullets constantly follow the player until the player destroys them! 
        
        float shootWaitTime = 1f;
        Instantiate(bullet, bulletSpawnPoint, bulletSpawnPoint);
        yield return null; 

    }
}
