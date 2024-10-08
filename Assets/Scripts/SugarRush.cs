using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SugarRush : MonoBehaviour
{
    private Player playerScript;
    public Invaders invadersScript;
    private float startCharge = 0f;
    private float maxCharge = 100;

    private bool hasAddedCharge = false;
    private bool isCharged = false;
    private int previousInvaderCount; // Track the previous invader count

    private float chargeAdded = 5f;
    private float chargeRemoved = 20f;


    private void Awake()
    {
        playerScript = GetComponent<Player>();
        invadersScript = GameObject.Find("EnemySpawner").GetComponent<Invaders>();
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
