using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MissileFireIndicator : MonoBehaviour
{
    [SerializeField] private Color readyColor;
    [SerializeField] private Color waitColor;

    [SerializeField] private Image indicatorImage;
    [SerializeField] private Transform indicatorLight;

    [SerializeField] private Material lightMaterial;
    private void Start()
    {
        
    }
    public void Indicate()
    {
        indicatorImage.DOFade(.2f, 0.5f);
       // lightMaterial.DOColor(waitColor, 0.5f);
        lightMaterial.SetColor("_BaseColor", waitColor);
        lightMaterial.SetColor("_EmissionColor", waitColor * Mathf.LinearToGammaSpace(10f));
    }

    public void ResetIndicator()
    {
        indicatorImage.DOFade(1, 0.5f);
        //lightMaterial.DOColor(readyColor, 0.5f);
        lightMaterial.SetColor("_BaseColor", readyColor);
        lightMaterial.SetColor("_EmissionColor", readyColor * Mathf.LinearToGammaSpace(10f));

    }
}
