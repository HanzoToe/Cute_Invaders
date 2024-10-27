
using Cinemachine;
using UnityEngine;


[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(BoxCollider2D))]
public class Player : MonoBehaviour
{
    public GameObject LeftEdge;
    public GameObject RightEdge;
    public Laser laserPrefab;
    public SugarRush sugarRushScript; 
    Laser laser;
    public float speed = 5f;
    public float timer = 0.5f;
    public float Orginaltime;

    AudioManagerScript AudioManagerScript;

    
    private void Start()
    {
        sugarRushScript = GetComponent<SugarRush>();
        Orginaltime = timer;
        AudioManagerScript = GameObject.Find("AudioManager").GetComponent<AudioManagerScript>();

        //-Love
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            position.x -= speed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            position.x += speed * Time.deltaTime;
        }

        Vector3 leftEdge = LeftEdge.transform.position;
        Vector3 rightEdge = RightEdge.transform.position;

        position.x = Mathf.Clamp(position.x, leftEdge.x, rightEdge.x);

        transform.position = position;

        

        //Shooting effects - Love

        timer -= Time.deltaTime;

        if (Input.GetKey(KeyCode.Space) && timer <= 0)
        {
            
            laser = Instantiate(laserPrefab, transform.position + new Vector3(0, 1), Quaternion.identity);
            timer = Orginaltime;

            int Shootingnum = Random.Range(1, 3);
            if (Shootingnum == 1)
            {
                AudioManagerScript.Instance.PlaySFX("Shoot1");
            }
            else if (Shootingnum == 2 || Shootingnum == 3)
            {
                AudioManagerScript.Instance.PlaySFX("Shoot2");
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Missile") || collision.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {
            GameManager.Instance.OnPlayerKilled(this);
        }
    }
}
