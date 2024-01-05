using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GeneralController : MonoBehaviour
{
    [System.Serializable]
    public class ScriptsReferences
    {
        public Timer timer;
        public ItemTracking itemTracking;
        public InputManager inputManager;
        public PlacementSystem placementSystem;
        public ShowInfoTextScript showInfoTextScript;
        public ImageFaderScript imageFaderScript;
        public ItemActivator itemActivator;
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
        public Button quitGame;
        public Button startGame;
        public Button openDurationRect;
        public Button closeDurationRect;
        public Button SetDefaultTime;
        public Button setDuration;
        public Button removeAllItems;
        public Button generateProceduralMap;
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
        buttons.quitGame.onClick.AddListener(() => QuitGame());
        buttons.removeAllItems.onClick.AddListener(() => scriptsReferences.placementSystem.RemoveAll());
        buttons.generateProceduralMap.onClick.AddListener(() => StartCoroutine(scriptsReferences.placementSystem.GenerateProceduralMap())) ;

        uiElements.durationSlider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
    }

    private void QuitGame()
    {
        scriptsReferences.imageFaderScript.FadeImageMethod(2, false);
        Invoke("QuitGameDelayed", 2f);

    }
    private void QuitGameDelayed()
    {
        Application.Quit();
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
        if (scriptsReferences.itemTracking.AllItemsPlaced())
        {
            scriptsReferences.placementSystem.StopPlacement();  
            scriptsReferences.imageFaderScript.FadeImageMethod(2, true);
            Invoke("StartGameDelayed", 2f);
        }

        else
        {
            scriptsReferences.showInfoTextScript.ShowInfoText("Place the remaining objects", 3);
            Debug.Log("place the remaining objects");
        }

    }

    public void StartGameDelayed()
    {
        environments.actionEnvironment.gameObject.SetActive(true);
        environments.strategyEnvironment.gameObject.SetActive(false);
        scriptsReferences.itemActivator.ActivateItems();
        scriptsReferences.timer.StartTimer();
    }

}
