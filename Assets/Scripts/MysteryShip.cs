using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class MysteryShip : MonoBehaviour
{
    public GameObject LeftEdge;
    public GameObject RightEdge;

    float speed = 5f;
    float cycleTime = 5f;

    Vector2 leftDestination;
    Vector2 rightDestination;
    int direction = -1;
    bool isVisible;

    SpriteRenderer sr; 

    
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();



        Vector3 leftEdge = LeftEdge.transform.position;
        Vector3 rightEdge = RightEdge.transform.position;

        //positionen d�r den kommer stanna utanf�r sk�rmen.
        leftDestination = new Vector2(leftEdge.x - 2f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 2f, transform.position.y);

        SetInvisible();
    }


    void Update()
    {
        if (!isVisible) //�r den inte synlig s� ska den ej r�ra sig.
        {
            return;
        }

        if(direction == 1)
        {
            //r�r sig �t h�ger
            transform.position += speed * Time.deltaTime * Vector3.right;
            sr.flipX = true;

            if (transform.position.x >= rightDestination.x)
            {
                SetInvisible();
 
            }
        }
        else
        {
            //r�r sig �t v�nster
            transform.position += speed * Time.deltaTime * Vector3.left;
            sr.flipX = false;

            if (transform.position.x <= leftDestination.x)
            {
                SetInvisible();
            }
        }
    }

  
    //flyttar den till en plast precis utanf�r scenen.
    void SetInvisible()
    {
        isVisible = false;

        if(direction == 1)
        {
            transform.position = rightDestination;
        }
        else
        {
            transform.position = leftDestination;
        }

        Invoke(nameof(SetVisible), cycleTime); //anropar SetVisible efter ett visst antal sekunder
    }

    void SetVisible()
    {
        direction *= -1; //�ndrar riktningen

        isVisible = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Laser"))
        {
            SetInvisible();
            GameManager.Instance.OnMysteryShipKilled(this);
        }
    }
}
