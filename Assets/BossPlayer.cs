using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class BossPlayer : MonoBehaviour
{
    public GameObject LeftEdge;
    public GameObject RightEdge;
    public Laser laserPrefab;
    Laser laser;
    public float speed = 500f;
    public float timer = 0.2f;
    public float Orginaltime;
    Rigidbody2D rb;
    Vector2 movement;
    Vector3 mouspos; 

    AudioManagerScript AudioManagerScript;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Orginaltime = timer;
        AudioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();

        //-Love
    }

    // Update is called once per frame
    void Update()
    {

        timer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timer <= 0)
        {
            laser = Instantiate(laserPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);
            timer = Orginaltime;

            AudioManagerScript.Instance.PlaySFX("Shoot1");
            //Sound effects -Love

        }

        GetMousePos();
    }

    private void FixedUpdate()
    {
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        rb.velocity = movement * speed * Time.deltaTime;

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
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(null,this);
        }
    }
}
