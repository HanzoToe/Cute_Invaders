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
    SugarRush sugarRushScript; 

    private void Awake()
    {
        spRend = GetComponent<SpriteRenderer>();
        sugarRushScript = GameObject.Find("Player").GetComponent<SugarRush>();
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

        animationFrame = (Mathf.RoundToInt(sugarRushScript.startCharge));

        if (animationFrame <= 0)
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
