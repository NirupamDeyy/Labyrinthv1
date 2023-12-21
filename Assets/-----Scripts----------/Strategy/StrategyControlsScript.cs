using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StrategyControlsScript : MonoBehaviour
{
    [SerializeField] private Transform backgroundImage;
    [SerializeField] private Button openCloseButton;

    private void Start()
    {
        openCloseButton.onClick.AddListener(() => { OpenCloseMethod(); });   
    }
    bool isClosed = false;
    private void OpenCloseMethod()
    {
        TMP_Text text = openCloseButton.GetComponentInChildren<TMP_Text>();

        if(isClosed)
        {
            backgroundImage.gameObject.SetActive(true);
            text.text = "HIDE";
            isClosed = false;

        }
        else
        {
            backgroundImage.gameObject.SetActive(false);
            text.text = "CONTROLS";
            isClosed = true;
        }
    }
}
