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
    public bool sugarRushModeActive = false; 
   
    private int previousInvaderCount; // Track the previous invader count

    private float startCharge = 0f;
    private float maxCharge = 30f;

    private float chargeAdded = 5f;
    private float chargeRemoved = 7f;

    private float originalPlayerSpeed;
    private float orifinalShootingSpeed;
    private float newTime = 0.15f; 


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
        SugarRushMode();
        AddCharge();
        ChargeChecks();
    }

    void AddCharge()
    {
        bool invaderCountChanged = invadersScript.numberOfInvaders != previousInvaderCount; 
        bool isDivisbleByFive = invadersScript.numberOfInvaders % 5 == 0;


        // Only add charge if the invader count has changed and is divisible by 5
        if (invaderCountChanged && isDivisbleByFive && !hasAddedCharge && !sugarRushModeActive)
        {
            if (startCharge < maxCharge)
            {
                startCharge += chargeAdded;
                hasAddedCharge = true;
            }
        }
        else if (!isDivisbleByFive && hasAddedCharge)
        {
            hasAddedCharge = false;
        }
    }

    void SugarRushMode()
    {
        //If left shift is pressed and the startcharge is greater than zero, allow "SugarRush" activation. 
        if (Input.GetKeyDown(KeyCode.LeftShift) && startCharge == maxCharge)
        {
            ActivateSugarRush();                 
        } 
       
        if (sugarRushModeActive)
        {
            HandleSugarRush();
        }
        else if(isCharged && !sugarRushModeActive)
        {
            ResetSugarRush();
        }
    }

    void ActivateSugarRush()
    {
        sugarRushModeActive = true;

        playerScript.speed = 12f;

        if (!timerChanged)
        {
            timerChanged = true;
            playerScript.Orginaltime = newTime;
        }
    }

    void HandleSugarRush()
    {
        startCharge -= chargeRemoved * Time.deltaTime;
        isCharged = true;

        if (startCharge <= 0)
            sugarRushModeActive = false;
    }

    void ResetSugarRush()
    {
        startCharge = Mathf.Round(startCharge);
        isCharged = false;
        timerChanged = false;
        playerScript.speed = originalPlayerSpeed;
        playerScript.Orginaltime = orifinalShootingSpeed;
    }


    void ChargeChecks()
    {
        startCharge = Mathf.Max(0, startCharge);
    }
}
