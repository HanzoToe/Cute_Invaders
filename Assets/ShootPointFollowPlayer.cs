
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class ShootPointFollowPlayer : MonoBehaviour
{
    Transform playerPosition;
    Vector2 direction;


    // Start is called before the first frame update
    void Start()
    {
        playerPosition = GameObject.Find("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        direction = (playerPosition.position - transform.position).normalized;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }
}
