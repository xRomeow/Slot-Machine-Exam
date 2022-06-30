using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlotRows : MonoBehaviour
{
    private int randomValue;
    private float timeInterval;

    public bool rowStopped;
    public string stoppedSlot;

    public void Start()
    {
        Initialize();
        rowStopped = true;
    }

    void Initialize()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.ON_SPIN_CLICKED, StartSpin);
    }

    public void StartSpin(Parameters param = null)
    {
        stoppedSlot = "";
        StartCoroutine(Rotate());
    }

    private IEnumerator Rotate()
    {
        rowStopped = false;
        timeInterval = 0.025f;

        for (int i = 0; i < 30; i++)
        {
            if (transform.position.y < 0f)
                transform.position = new Vector2(transform.position.x, 3.2f);

            //Debug.Log("START LOOP VALUE: " + i + "Position Y: " + transform.position.y);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.2f);

            //Debug.Log("START LOOP VALUE 2: " + i + "Position Y: " + transform.position.y);


            yield return new WaitForSeconds(timeInterval);
        }

        randomValue = Random.Range(90, 100); // Original is 60, 100

        switch (randomValue % 2)
        {
            case 0:
                randomValue += 1;
                break;
        }

        for (int i = 0; i < randomValue; i++)
        {
            if(transform.position.y < 0.4f)
                transform.position = new Vector2(transform.position.x, 3.2f);

            transform.position = new Vector2(transform.position.x, transform.position.y - 0.2f);

            // Slow motion roll (optional)
            //if (i > Mathf.RoundToInt(randomValue * 0.25f))
            //    timeInterval = 0.05f;
            //if (i > Mathf.RoundToInt(randomValue * 0.5f))
            //    timeInterval = 0.1f;
            //if (i > Mathf.RoundToInt(randomValue * 0.75f))
            //    timeInterval = 0.15f;
            //if (i > Mathf.RoundToInt(randomValue * 0.95f))
            //    timeInterval = 0.2f;

            yield return new WaitForSeconds(timeInterval);
        }

        if (transform.position.y >= -1f && transform.position.y <= 0.39f) // Diamond 
            stoppedSlot = "A";
        else if (transform.position.y >= 0.4f && transform.position.y <= 0.79f) // Crown  
            stoppedSlot = "B";
        else if (transform.position.y >= 0.8f && transform.position.y <= 1.19f) // Watermelon 
            stoppedSlot = "C";
        else if (transform.position.y >= 1.2f && transform.position.y <= 1.59f) // BAR 
            stoppedSlot = "D";
        else if (transform.position.y >= 1.6f && transform.position.y <= 1.99f) // Seven
            stoppedSlot = "E";
        else if (transform.position.y >= 2.0f && transform.position.y <= 2.39f) // Cherry 
            stoppedSlot = "F";
        else if (transform.position.y >= 2.4f && transform.position.y <= 2.79f) // Lemon  
            stoppedSlot = "G";
        else if (transform.position.y >= 2.8f) // Diamond
            stoppedSlot = "A";

        rowStopped = true;

    }

}
