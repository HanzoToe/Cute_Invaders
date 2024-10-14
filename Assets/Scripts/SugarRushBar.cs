using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class SugarRushBar : MonoBehaviour
{
    public Sprite[] animationSprites = new Sprite[2];


    SpriteRenderer spRend;
    int animationFrame;

    public bool hasAddedBar = false; 

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        spRend.sprite = animationSprites[0];
    }

    public void Charge_Bar()
    {
        animationFrame += 2;
        if (animationFrame >= animationSprites.Length)
        {
            animationFrame = animationSprites.Length - 2;
        }
        spRend.sprite = animationSprites[animationFrame];
    }

    public void RemoveBar()
    {
        float animationSpeed = 2f;

        animationFrame -= (int)(animationSpeed * Time.deltaTime * animationSprites.Length);

        if (animationFrame < 0)
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

    }
}
