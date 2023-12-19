using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;


public class ShowInfoTextScript : MonoBehaviour
{
    [SerializeField] private Color successColor = Color.green;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.yellow;
    [SerializeField] private Color errorColor = Color.red;

    [SerializeField] private Transform prefabOrigin;
    [SerializeField] private Image uiPrefab;

    [SerializeField] private float colorBrightnessModifier;

     List<Image> imagesList = new List<Image>();
    [SerializeField] private float distance = 5f;

    public bool showText;
    void Start()
    {
       // ShowInfoText("this is dangerous", 3);
    }

    private Color DecreasedBrightnessOfColor( Color color)
    {
        float h, s, v;
        Color.RGBToHSV(color, out h, out s, out v);

        // Modify brightness (value component in HSV)
        v = Mathf.Clamp01(v + colorBrightnessModifier);

        // Return the modified color
        return Color.HSVToRGB(h, s, v);
    }

    public void ShowInfoText(string infoText, int level)
    {
        Color color ;
        
        switch (level)
        {
            case 0:
                color = successColor;
                break;
                
            case 1:
                color = normalColor;
                break;

            case 2:
                color = warningColor;
                break;

            case 3:
                color = errorColor;
                break;

            default:
                color = normalColor;
                break
;       }

        Image newPrefab = Instantiate(uiPrefab, prefabOrigin);
        imagesList.Add(newPrefab);
        MovePrefab();
        StartCoroutine(KillImage(newPrefab));
        newPrefab.color = DecreasedBrightnessOfColor(color);
        TMP_Text text = newPrefab.GetComponentInChildren<TMP_Text>();
        text.text = infoText;
        text.color = color;
        
    }

    private IEnumerator KillImage(Image image)
    {
        yield return new WaitForSeconds(5f);
        imagesList.Remove(image);
        image.DOFade(0, 1f).OnComplete(() =>
        { Destroy(image.gameObject); });
        Destroy(image.GetComponentInChildren<TMP_Text>());
    }

    private void MovePrefab()
    {
        foreach(Image image in imagesList)
        {
            image.transform.DOMoveY(image.transform.position.y + distance, 1f,true);
           
        }
    }
   /* public int i = 1;
    void FixedUpdate()
    {
        if(showText)
        {
            ShowInfoText("this is dangerous", i);
            showText = false;
        }
    }*/
}
