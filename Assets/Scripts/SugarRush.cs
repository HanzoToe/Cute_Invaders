using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : MonoBehaviour
{
    private Player playerScript;
    public Invaders invadersScript; 
    public float startCharge = 0f;
    public float maxCharge = 100;


    private bool hasAddedCharge;
    public bool isCharged = false; 


    private float chargeAdded = 5f;
    private float chargeRemoved = 10f; 


    private void Awake()
    {
        playerScript = GetComponent<Player>();
        invadersScript = GameObject.Find("EnemySpawner").GetComponent<Invaders>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        SugarRushActivation();
        AddCharge();
        ChargeChecks();
    }

    void AddCharge()
    {
        if(invadersScript.numberOfInvaders % 5 == 0 && !hasAddedCharge)
        {
            if(startCharge != maxCharge)
            {
                startCharge += chargeAdded;
                hasAddedCharge = true;
            }
           
        }
        else if(invadersScript.numberOfInvaders % 5 != 0 && hasAddedCharge)
        {
            hasAddedCharge = false; 
        }
    }

    void SugarRushActivation()
    {
        if (Input.GetKey(KeyCode.LeftShift) && startCharge > 0)
        {
            startCharge -= chargeRemoved * Time.deltaTime;
            isCharged = true; 
        }
        else if (isCharged)
        {
            startCharge = Mathf.Round(startCharge);
            isCharged = false;          
        }
    }

    void ChargeChecks()
    {
        if (startCharge < 0)
            startCharge = 0; 
    }
}
