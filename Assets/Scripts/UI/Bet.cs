using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Bet : MonoBehaviour
{
    [SerializeField] private Text betText; // convert bet text to int
    [SerializeField] private Button increase; // decrease by 10
    [SerializeField] private Button decrease; // decrease by 10
    private int betCash; // Cash - bet

    public int playerCash;

    void Start()
    {
        betCash = 10;
        betText.text = betCash.ToString();
        //EventBroadcaster.Instance.AddObserver(EventNames.ON_CASH_DEDUCTED, ModifyBetIncrease);
    }

    public void ModifyBetDecrease()
    {
        betCash -= 10;
        betText.text = betCash.ToString();

        if (betCash <= 0)
        {
            this.decrease.enabled = false;
        } else {
            this.decrease.enabled = true;
        }
        Parameters param = new Parameters();
        param.AddParameter<int>("currBetValue", +10);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_BET_MODIFIED, param);

    }

    public void ModifyBetIncrease()
    {
        betCash += 10;
        betText.text = betCash.ToString();
        Parameters param = new Parameters();
        param.AddParameter<int>("currBetValue", -10);
        EventBroadcaster.Instance.PostEvent(EventNames.ON_BET_MODIFIED, param);
    }
}
