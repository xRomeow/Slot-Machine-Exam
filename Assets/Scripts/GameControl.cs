using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class GameControl : MonoBehaviour
{
    // This is the master control where the player clicks anything, this will be called.
    [SerializeField] private Text prizeText; // control prize text game object at the middle top
    [SerializeField] private SlotRows[] rows; // help us get information about our row 
    [SerializeField] private Transform handle; // control the handle
    [SerializeField] private Text playerCash; // total money of the player
    [SerializeField] private Text winText; // win value bar
    [SerializeField] private Text payoutLines; // to be changed into shapes
    [SerializeField] private Button spinButton; // for disable and enable purposes

    
    public int playerCashInt; // Convert int to string for your player cash
    private int winValue; // Win value that we hold after winning
    private bool resultsChecked = false; // will not allow to check variables multiple times? when rows stop spinning
    private int totalBet;

    void Start()
    {
        Initialize();

    }
    void Initialize()
    {
        playerCashInt = 200;
        playerCash.text = playerCashInt.ToString();
        EventBroadcaster.Instance.AddObserver(EventNames.ON_BET_MODIFIED, BetTextValue);
    }

    void Update()
    {
        

        if (playerCashInt <= -1)
        {
            this.spinButton.enabled = false;
        } else
        {
            this.spinButton.enabled = true;
        }

        if (!rows[0].rowStopped || !rows[1].rowStopped || !rows[2].rowStopped || !rows[3].rowStopped || !rows[4].rowStopped)
        {
            winValue = 0;
            prizeText.enabled = false;
            resultsChecked = false;
        }

        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && rows[3].rowStopped && rows[4].rowStopped && !resultsChecked)
        {
            CheckResults();
            prizeText.enabled = true;
            prizeText.text = "PRIZE: " + winValue;
        }

    }

    public void BetTextValue(Parameters param = null)
    {
        if (param != null)
        {
            int bet = param.GetParameter<int>("currBetValue", 0);
            playerCashInt += bet;
            playerCash.text = playerCashInt.ToString();
        }
    }


    public void ClickSpin()
    {
        Parameters param = null;
        EventBroadcaster.Instance.PostEvent(EventNames.ON_SPIN_CLICKED, param);


        if (rows[0].rowStopped && rows[1].rowStopped && rows[2].rowStopped && rows[3].rowStopped && rows[4].rowStopped)
            StartCoroutine(PullHandle());

        Debug.Log("CLICKED");

    }


    private IEnumerator PullHandle()
    {
        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, i);
            yield return new WaitForSeconds(0.1f);
        }

        //HandlePulled();

        for (int i = 0; i < 15; i += 5)
        {
            handle.Rotate(0f, 0f, -i);
            yield return new WaitForSeconds(0.1f);
        }
    }

    // Results Payout Lines and Win Values
    private void CheckResults()
    {
        if (rows[0].stoppedSlot == "A" && rows[1].stoppedSlot == "A" && rows[2].stoppedSlot == "A" && rows[3].stoppedSlot == "A" && rows[4].stoppedSlot == "A") 
            Payouts(5000);

        else if (rows[0].stoppedSlot == "B" && rows[1].stoppedSlot == "B" && rows[2].stoppedSlot == "B" && rows[3].stoppedSlot == "B" && rows[4].stoppedSlot == "B")
            Payouts(4500);

        else if (rows[0].stoppedSlot == "C" && rows[1].stoppedSlot == "C" && rows[2].stoppedSlot == "C" && rows[3].stoppedSlot == "C" && rows[4].stoppedSlot == "C")
            Payouts(4000);

        else if (rows[0].stoppedSlot == "D" && rows[1].stoppedSlot == "D" && rows[2].stoppedSlot == "D" && rows[3].stoppedSlot == "D" && rows[4].stoppedSlot == "D")
            Payouts(3500);

        else if (rows[0].stoppedSlot == "E" && rows[1].stoppedSlot == "E" && rows[2].stoppedSlot == "E" && rows[3].stoppedSlot == "E" && rows[4].stoppedSlot == "E")
            Payouts(3000);

        else if (rows[0].stoppedSlot == "F" && rows[1].stoppedSlot == "F" && rows[2].stoppedSlot == "F" && rows[3].stoppedSlot == "F" && rows[4].stoppedSlot == "F")
            Payouts(2500);

        else if (rows[0].stoppedSlot == "G" && rows[1].stoppedSlot == "G" && rows[2].stoppedSlot == "G" && rows[3].stoppedSlot == "G" && rows[4].stoppedSlot == "G")
            Payouts(2000);


        //   \/ A
        else if (rows[0].stoppedSlot == "A" && rows[1].stoppedSlot == "G" && rows[2].stoppedSlot == "F" && rows[3].stoppedSlot == "G" && rows[4].stoppedSlot == "A")
            Payouts(2000);

        //   /\ A
        else if (rows[0].stoppedSlot == "F" && rows[1].stoppedSlot == "G" && rows[2].stoppedSlot == "A" && rows[3].stoppedSlot == "G" && rows[4].stoppedSlot == "F")
            Payouts(2000);

        // \/ B
        else if (rows[0].stoppedSlot == "B" && rows[1].stoppedSlot == "A" && rows[2].stoppedSlot == "G" && rows[3].stoppedSlot == "A" && rows[4].stoppedSlot == "B")
            Payouts(1000);
        // /\ B
        else if (rows[0].stoppedSlot == "G" && rows[1].stoppedSlot == "A" && rows[2].stoppedSlot == "B" && rows[3].stoppedSlot == "A" && rows[4].stoppedSlot == "G")
            Payouts(1000);

        // \/ C
        else if (rows[0].stoppedSlot == "C" && rows[1].stoppedSlot == "B" && rows[2].stoppedSlot == "A" && rows[3].stoppedSlot == "B" && rows[4].stoppedSlot == "C")
            Payouts(1000);

        // /\ C
        else if (rows[0].stoppedSlot == "A" && rows[1].stoppedSlot == "B" && rows[2].stoppedSlot == "C" && rows[3].stoppedSlot == "B" && rows[4].stoppedSlot == "A")
            Payouts(1000);

        // \/ D
        else if (rows[0].stoppedSlot == "D" && rows[1].stoppedSlot == "C" && rows[2].stoppedSlot == "B" && rows[3].stoppedSlot == "C" && rows[4].stoppedSlot == "D")
            Payouts(1000);

        // /\ D
        else if (rows[0].stoppedSlot == "B" && rows[1].stoppedSlot == "C" && rows[2].stoppedSlot == "D" && rows[3].stoppedSlot == "C" && rows[4].stoppedSlot == "B")
            Payouts(1000);

        // \/ E
        else if (rows[0].stoppedSlot == "E" && rows[1].stoppedSlot == "D" && rows[2].stoppedSlot == "C" && rows[3].stoppedSlot == "D" && rows[4].stoppedSlot == "E")
            Payouts(1000);

        // /\ E
        else if (rows[0].stoppedSlot == "C" && rows[1].stoppedSlot == "D" && rows[2].stoppedSlot == "E" && rows[3].stoppedSlot == "D" && rows[4].stoppedSlot == "C")
            Payouts(1000);


        // \/ F
        else if (rows[0].stoppedSlot == "F" && rows[1].stoppedSlot == "E" && rows[2].stoppedSlot == "D" && rows[3].stoppedSlot == "E" && rows[4].stoppedSlot == "F")
            Payouts(1000);

        // /\ F
        else if (rows[0].stoppedSlot == "D" && rows[1].stoppedSlot == "E" && rows[2].stoppedSlot == "F" && rows[3].stoppedSlot == "E" && rows[4].stoppedSlot == "D")
            Payouts(1000);

        // \/ G
        else if (rows[0].stoppedSlot == "G" && rows[1].stoppedSlot == "F" && rows[2].stoppedSlot == "E" && rows[3].stoppedSlot == "F" && rows[4].stoppedSlot == "G")
            Payouts(1000);

        // /\ G
        else if (rows[0].stoppedSlot == "E" && rows[1].stoppedSlot == "F" && rows[2].stoppedSlot == "G" && rows[3].stoppedSlot == "F" && rows[4].stoppedSlot == "E")
            Payouts(1000);


        // 2 pair row straight
        else if (rows[0].stoppedSlot == "A" && rows[1].stoppedSlot == "A" || rows[1].stoppedSlot == "A" && rows[2].stoppedSlot == "A" || rows[2].stoppedSlot == "A" && rows[3].stoppedSlot == "A" || rows[3].stoppedSlot == "A" && rows[4].stoppedSlot == "A")
            Payouts(500);

        else if (rows[0].stoppedSlot == "B" && rows[1].stoppedSlot == "B" || rows[1].stoppedSlot == "B" && rows[2].stoppedSlot == "B" || rows[2].stoppedSlot == "B" && rows[3].stoppedSlot == "B" || rows[3].stoppedSlot == "B" && rows[4].stoppedSlot == "B")
            Payouts(500);

        else if (rows[0].stoppedSlot == "C" && rows[1].stoppedSlot == "C" || rows[1].stoppedSlot == "C" && rows[2].stoppedSlot == "C" || rows[2].stoppedSlot == "C" && rows[3].stoppedSlot == "C" || rows[3].stoppedSlot == "C" && rows[4].stoppedSlot == "C")
            Payouts(500);

        else if (rows[0].stoppedSlot == "D" && rows[1].stoppedSlot == "D" || rows[1].stoppedSlot == "D" && rows[2].stoppedSlot == "D" || rows[2].stoppedSlot == "D" && rows[3].stoppedSlot == "D" || rows[3].stoppedSlot == "D" && rows[4].stoppedSlot == "D")
            Payouts(500);

        else if (rows[0].stoppedSlot == "E" && rows[1].stoppedSlot == "E" || rows[1].stoppedSlot == "E" && rows[2].stoppedSlot == "E" || rows[2].stoppedSlot == "E" && rows[3].stoppedSlot == "E" || rows[3].stoppedSlot == "E" && rows[4].stoppedSlot == "E")
            Payouts(500);

        else if (rows[0].stoppedSlot == "F" && rows[1].stoppedSlot == "F" || rows[1].stoppedSlot == "F" && rows[2].stoppedSlot == "F" || rows[2].stoppedSlot == "F" && rows[3].stoppedSlot == "F" || rows[3].stoppedSlot == "F" && rows[4].stoppedSlot == "F")
            Payouts(500);

        else if (rows[0].stoppedSlot == "G" && rows[1].stoppedSlot == "G" || rows[1].stoppedSlot == "G" && rows[2].stoppedSlot == "G" || rows[2].stoppedSlot == "G" && rows[3].stoppedSlot == "G" || rows[3].stoppedSlot == "G" && rows[4].stoppedSlot == "G")
            Payouts(500);

        resultsChecked = true;
    }

    // Payout Lines to Cash
    public void Payouts(int value)
    {
        Debug.Log("TOTAL BET: " + totalBet);
        winValue += totalBet * value;
        winText.text = winValue.ToString();

        playerCashInt += winValue;
        playerCash.text = playerCashInt.ToString();
    }

}
