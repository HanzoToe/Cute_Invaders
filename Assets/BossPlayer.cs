using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BossPlayer : MonoBehaviour
{
    public GameObject LeftEdge;
    public GameObject RightEdge;
    public BossLaser laserPrefab;
    BossLaser laser;
    public float speed = 500f;
    public float timer = 0.2f;
    public float Orginaltime;
    Rigidbody2D rb;
    Vector2 movement;
    Vector3 mouspos; 
    public Transform laserSpawnPoint;
    AudioManagerScript AudioManagerScript;
    

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        rb = GetComponent<Rigidbody2D>();
        Orginaltime = timer;
        
        
        AudioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();

        //-Love ^
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;


        if (Input.GetKey(KeyCode.Space) && timer <= 0)
        {
            laser = Instantiate(laserPrefab, laserSpawnPoint.position + new Vector3(0, 1), Quaternion.identity);
            timer = Orginaltime;

            AudioManagerScript.Instance.PlaySFX("Shoot1");
            //Sound effects -Love

        }

        int Shootingnum = Random.Range(1, 3);
        Debug.Log(Shootingnum);
        if (Shootingnum == 1)
        {
            AudioManagerScript.Instance.PlaySFX("Shoot1");
        }
        else if (Shootingnum == 2 || Shootingnum == 3)
        {
            AudioManagerScript.Instance.PlaySFX("Shoot2");
        }

        if (Time.timeScale == 1f)
        {
            GetMousePos();
        }
            
    }

    private void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;

        rb.velocity = movement * speed * Time.fixedDeltaTime;

    }

    void GetMousePos()
    {
        mouspos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector2 direction = (mouspos - transform.position).normalized;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle - 90);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader") || collision.gameObject.layer == LayerMask.NameToLayer("Boss"))
        {
            BossGameManager.Instance.OnPlayerKilled(this);
        }
    }
}
