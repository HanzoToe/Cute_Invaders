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

    
    void Start()
    {
        Vector3 leftEdge = LeftEdge.transform.position;
        Vector3 rightEdge = RightEdge.transform.position;

        //positionen där den kommer stanna utanför skärmen.
        leftDestination = new Vector2(leftEdge.x - 2f, transform.position.y);
        rightDestination = new Vector2(rightEdge.x + 2f, transform.position.y);

        SetInvisible();
    }


    void Update()
    {
        if (!isVisible) //är den inte synlig så ska den ej röra sig.
        {
            return;
        }

        if(direction == 1)
        {
            //rör sig åt höger
            transform.position += speed * Time.deltaTime * Vector3.right;

            if (transform.position.x >= rightDestination.x)
            {
                SetInvisible();
            }
        }
        else
        {
            //rör sig åt vänster
            transform.position += speed * Time.deltaTime * Vector3.left;

            if (transform.position.x <= leftDestination.x)
            {
                SetInvisible();
            }
        }
    }

  
    //flyttar den till en plast precis utanför scenen.
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
        direction *= -1; //Ändrar riktningen

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
