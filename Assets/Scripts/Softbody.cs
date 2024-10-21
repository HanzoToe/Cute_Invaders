using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class Softbody : MonoBehaviour
{
    public Transform[] points;
    public SpriteShapeController spriteShape;
    // Start is called before the first frame update
    void Awake()
    {
        UpdateVerticies();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateVerticies();
    }

    private void UpdateVerticies()
    {
        for (int i = 0; i < points.Length - 1; i++)
        {
            Vector2 vertex = points[i].localPosition;

            Vector2 towardsCenter = (Vector2.zero - vertex).normalized;

            float colliderRadius = points[i].gameObject.GetComponent<CircleCollider2D>().radius;

            spriteShape.spline.SetPosition(i, (vertex - towardsCenter * (colliderRadius * 0.5f)));

            Vector2 lt = spriteShape.spline.GetLeftTangent(i);

            Vector2 newRt = Vector2.Perpendicular(towardsCenter) * lt.magnitude;
            Vector2 newLt = Vector2.zero - (newRt);

            spriteShape.spline.SetRightTangent(i, newLt);
            spriteShape.spline.SetLeftTangent(i, newRt);
        }

    }

}
