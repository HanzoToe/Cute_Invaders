using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : MonoBehaviour
{
    private Player playerScript;
    public Invaders invadersScript;
  
    //Bools
    private bool hasAddedCharge = false; //Check if charge is added
    private bool isCharged = false;
    private bool timerChanged = false; 
   
    private int previousInvaderCount; // Track the previous invader count


    private float startCharge = 0f;
    private float maxCharge = 30f;

    private float chargeAdded = 5f;
    private float chargeRemoved = 7f;

    private float originalPlayerSpeed;
    private float orifinalShootingSpeed;
    public float newTime = 0.15f; 


    private void Awake()
    {
        playerScript = GetComponent<Player>();
        invadersScript = GameObject.Find("EnemySpawner").GetComponent<Invaders>();
        originalPlayerSpeed = playerScript.speed;
        orifinalShootingSpeed = playerScript.timer; 
    }

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the previous invader count with the starting value
        previousInvaderCount = invadersScript.GetInvaderCount();
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

        // Only add charge if the invader count has changed and is divisible by 5
        if (invadersScript.numberOfInvaders != previousInvaderCount && invadersScript.numberOfInvaders % 5 == 0 && !hasAddedCharge)
        {
            if (startCharge != maxCharge)
            {
                startCharge += chargeAdded;
                hasAddedCharge = true;
            }
        }
        else if (invadersScript.numberOfInvaders % 5 != 0 && hasAddedCharge)
        {
            hasAddedCharge = false;
        }
    }

    void SugarRushActivation()
    {
        //If left shift is being held and the startcharge is greater than zero, allow "SugarRush" activation. 
        if (Input.GetKey(KeyCode.LeftShift) && startCharge > 0)
        {

            startCharge -= chargeRemoved * Time.deltaTime;
            isCharged = true;
            
            playerScript.speed = 12f;

            if (!timerChanged)
            {
                timerChanged = true;
                playerScript.Orginaltime = newTime;
            }
             
        }
        else if (isCharged)
        {
            startCharge = Mathf.Round(startCharge);
            isCharged = false;
            timerChanged = false;
            playerScript.speed = originalPlayerSpeed;
            playerScript.Orginaltime = orifinalShootingSpeed;
        }
    }

    void ChargeChecks()
    {
        if (startCharge < 0)
            startCharge = 0;
    }
}
