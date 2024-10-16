using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviour
{
    public int nrOfHits = 0;
    public List<Transform> Child = new List<Transform>();
    private void Awake()
    {
        Child = new List<Transform>(GetComponentsInChildren<Transform>());
        Child.RemoveAt(0);
        int lastIndex = Child.Count - 1;
        Child.RemoveAt(lastIndex);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
       
        if (other.gameObject.layer == LayerMask.NameToLayer("Missile") || other.gameObject.layer == LayerMask.NameToLayer("Invader"))
        {


            nrOfHits++;
            if (nrOfHits == 4)
            {
                transform.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
                foreach(Transform childTransform in Child)
                {
                    childTransform.gameObject.GetComponent<SpringJoint2D>().enabled = false;
                    childTransform.gameObject.GetComponent<DistanceJoint2D>().enabled = false;

                    Rigidbody2D pointRB = childTransform.gameObject.GetComponent<Rigidbody2D>();
                    pointRB.velocity = new Vector2(0,-10);
                }
            }
            
        }
    }

    public void ResetBunker()
    {
        gameObject.SetActive(true);
        nrOfHits = 0; 
    }
}
