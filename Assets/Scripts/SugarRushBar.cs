using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRushBar : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[2];


    SpriteRenderer spRend;
    int animationFrame;

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = animationSprites[0];
    }

    public void Charge_Bar()
    {
        animationFrame++;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = 0;
        }
        spRend.sprite = animationSprites[animationFrame];
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Charge_Bar();
        }
    }
}