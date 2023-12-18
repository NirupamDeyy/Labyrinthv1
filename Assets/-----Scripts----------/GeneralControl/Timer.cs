using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TMP_Text showTime;

    public void SetDuration(int mins)
    {
        timeRemaining = mins * 60;
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    private void ChangeTimeText(float time)
    {
        showTime.text = ((int)time / 60) + "." + ((int)(time % 60)); 
    }


    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;

                ChangeTimeText(timeRemaining);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false;
                TimeEndedFunction();
            }
        }
    }

    private void TimeEndedFunction()
    {

    }
}