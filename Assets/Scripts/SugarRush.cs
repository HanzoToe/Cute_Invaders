using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : MonoBehaviour
{
    private Player playerScript;
    public Invaders invadersScript;
    private SugarRushBar sugarRushBarScript; 
  
    //Bools
    private bool hasAddedCharge = false; //Check if charge is added
    private bool isCharged = false;
    private bool timerChanged = false;
    public bool sugarRushModeActive = false; 
   
    private int previousInvaderCount; // Track the previous invader count

    public float startCharge = 0f;
    private float maxCharge = 50f;

    private float chargeAdded = 2f;
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
        sugarRushBarScript = GameObject.Find("SugarRushChargeBar").GetComponent<SugarRushBar>();
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
        bool isDivisbleByTwo = invadersScript.numberOfInvaders % 2 == 0;


        // Only add charge if the invader count has changed and is divisible by 5
        if (invaderCountChanged && isDivisbleByTwo && !hasAddedCharge && !sugarRushModeActive)
        {
            if (startCharge < maxCharge)
            {
                sugarRushBarScript.Charge_Bar();
                startCharge += chargeAdded;
                hasAddedCharge = true;
            }
        }
        else if (!isDivisbleByTwo && hasAddedCharge)
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
