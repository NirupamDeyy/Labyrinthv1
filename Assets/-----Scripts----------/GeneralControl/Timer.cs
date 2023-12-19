using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public ShowInfoTextScript showInfoTextScript;
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public TMP_Text showTime;
    public ActionUIconrol actionUIconrol;
    float initialTime;

    private void Start()
    {
        
    }
    public void SetDuration(int mins)
    {
        initialTime = mins;
        timeRemaining = mins * 60;
        showInfoTextScript.ShowInfoText("Duration is Set to: " + mins + " mins. ", 0);
    }

    public void StartTimer()
    {
        timerIsRunning = true;
    }

    private void ChangeTimeText(float time)
    {
        showTime.text = ((int)time / 60) + "." + ((int)(time % 60)); 
    }

    public float GetTimePassed()
    {
        return initialTime -  timeRemaining/60;
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
        actionUIconrol.Pause();
        actionUIconrol.ShowTimeUpText();
    }
}