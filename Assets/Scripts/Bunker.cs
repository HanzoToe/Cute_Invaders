using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using UnityEngine.U2D;

[RequireComponent(typeof(BoxCollider2D))]
public class Bunker : MonoBehaviour
{
    public int nrOfHits = 0;
    public List<Transform> Child = new List<Transform>();
    public SpriteShapeRenderer sSRender;
    float fadeDuration = 2.0f;
    private void Awake()
    {
        Child = new List<Transform>(GetComponentsInChildren<Transform>());
        Child.RemoveAt(0);
        int lastIndex = Child.Count - 1;
        Child.RemoveAt(lastIndex);

        sSRender = GetComponentInChildren<SpriteShapeRenderer>();
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
                    pointRB.velocity = new Vector2((transform.position.x - childTransform.position.x) * 3, (transform.position.y - childTransform.position.y) * 3);
                    
                }
                DestroyBunker();
            }
            
        }
    }

    public void ResetBunker()
    {
        gameObject.SetActive(true);
        nrOfHits = 0; 
    }

    IEnumerator DestroyBunker ()
    {
        Color originalColor = sSRender.color;
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(originalColor.a, 0, elapsedTime / fadeDuration);
            sSRender.color = new Color(originalColor.r, originalColor.g, originalColor.b, newAlpha);
            yield return null;
        }

        sSRender.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0);
    }
}
