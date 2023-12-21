using UnityEngine;
using TMPro;
public class Timer : MonoBehaviour
{
    public ShowInfoTextScript showInfoTextScript;
    public float timeRemaininginSecs = 120;
    public bool timerIsRunning = false;
    public TMP_Text showTime;
    public ActionUIconrol actionUIconrol;
    float initialTime = 2f;

    private void Start()
    {
        
    }
    public void SetDuration(int mins)
    {
        initialTime = mins;
        timeRemaininginSecs = mins * 60;
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

    public string GetTimePassed()
    {
        float timeleftinSecs = initialTime * 60 - timeRemaininginSecs;
        return ((int)timeleftinSecs / 60) + "." + ((int)(timeleftinSecs % 60));
    }

    void Update()
    {
        if (timerIsRunning)
        {
            if (timeRemaininginSecs > 0)
            {
                timeRemaininginSecs -= Time.deltaTime;

                ChangeTimeText(timeRemaininginSecs);
            }
            else
            {
                Debug.Log("Time has run out!");
                timeRemaininginSecs = 0;
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