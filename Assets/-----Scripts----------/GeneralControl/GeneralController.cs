using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
public class GeneralController : MonoBehaviour
{
    [System.Serializable]
    public class ScriptsReferences
    {
        public Timer timer;
        public ItemTracking itemTracking;
    }

    [System.Serializable]
    public class Environments
    {
        public Transform actionEnvironment;
        public Transform strategyEnvironment;
    }

    [System.Serializable]
    public class UIElements
    {
        public Transform durationRect;
        public Slider durationSlider;
        public TMP_Text duration;

    }
    [System.Serializable]
    public class Buttons
    {
        public Button startGame;
        public Button openDurationRect;
        public Button closeDurationRect;
        public Button SetDefaultTime;
        public Button setDuration;
    }

    public int durationValue;

    public ScriptsReferences scriptsReferences;
    public Environments environments;
    public UIElements uiElements;   
    public Buttons buttons;

    private void Start()
    {
        SetDefaultTimeFunction();
        uiElements.durationRect.gameObject.SetActive(false);
        durationValue = 2;
        buttons.startGame.onClick.AddListener(() => StartGameFunction());
        buttons.openDurationRect.onClick.AddListener(() => OpenDurationRectFunction());
        buttons.closeDurationRect.onClick.AddListener(() => CloseDurationRectFunction());
        buttons.SetDefaultTime.onClick.AddListener(() => SetDefaultTimeFunction());
        buttons.setDuration.onClick.AddListener(() => SetDurationFunction());
        uiElements.durationSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void SetDurationFunction()
    {
        scriptsReferences.timer.SetDuration(durationValue);
        CloseDurationRectFunction();
        //for now
        //scriptsReferences.timer.StartTimer();
    }

    private void SetDefaultTimeFunction()
    {
        int x = 2;
        uiElements.durationSlider.value = x;
        uiElements.duration.text = x + " mins";
        durationValue = x;
    }

    private void CloseDurationRectFunction()
    {
        uiElements.durationRect.gameObject.SetActive(false);
    }

    private void OpenDurationRectFunction()
    {
        uiElements.durationRect.gameObject.SetActive(true);
    }

    private void ValueChangeCheck()
    {
         float x = uiElements.durationSlider.value;
         uiElements.duration.text = x + " mins";
         durationValue = (int)x;
    }

    public void StartGameFunction()
    {
        if(scriptsReferences.itemTracking.AllItemsPlaced())
        {
            environments.actionEnvironment.gameObject.SetActive(true);
            environments.strategyEnvironment.gameObject.SetActive(false);
            scriptsReferences.timer.StartTimer();
        }

        else
        {
            Debug.Log("place the remaining objects");
        }
        
    }

}
