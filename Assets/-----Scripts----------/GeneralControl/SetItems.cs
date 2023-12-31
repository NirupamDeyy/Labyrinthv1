using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class SetItems : MonoBehaviour
{
    public ItemTracking itemTracking;
    public PlacementSystem placementSystem;
    [SerializeField] 
    private TMP_Text totalItem1, tI1, totalItem2, tI2, totalItem3, tI3;
   /* [SerializeField] 
    private TMP_Text currentItem1, currentItem2;*/ // aleady taken care by ItemTracking

    [SerializeField]
    private Transform itemSetterRect;

    [SerializeField]
    private Button openItemSetterRect, setDefaultNumberOfItems, setNumberOfItems;

    [SerializeField]
    private Slider coinSlider, HexWallSlider, turretSlider;

    bool itemSetterRectisOpenned = false;

    void Start()
    {
        itemSetterRect.gameObject.SetActive(false);

        setNumberOfItems.onClick.AddListener(() => SetTotalNumberOfItems());
        setDefaultNumberOfItems.onClick.AddListener(() => SetDefaultNumberOfItems());
        openItemSetterRect.onClick.AddListener(() => OpenCloseItemSetterRect());
        coinSlider.onValueChanged.AddListener(delegate { CoinNumberValueChange(); });
        HexWallSlider.onValueChanged.AddListener(delegate { HexWallNumberValueChange(); });
        turretSlider.onValueChanged.AddListener(delegate { TurretNumberValueChange(); });
        SetDefaultNumberOfItems();
        SetTotalNumberOfItems();
    }

    int totalCoin, totalHexWall, totalTurret;
    private void SetDefaultNumberOfItems()
    {
        coinSlider.value = 5;
        HexWallSlider.value = 10;
        turretSlider.value = 2;
        CoinNumberValueChange();
        HexWallNumberValueChange();
        TurretNumberValueChange();
    }
    private void SetTotalNumberOfItems()
    {
        totalItem1.text = totalCoin.ToString();
        itemTracking.maxItemCounts[0] = totalCoin;

        totalItem2.text = totalHexWall.ToString();
        itemTracking.maxItemCounts[1] = totalHexWall;

        totalItem3.text = totalTurret.ToString();
        itemTracking.maxItemCounts[2] = totalTurret;

        itemTracking.OnMaxNumberChange();
        placementSystem.RemoveAll();
        OpenCloseItemSetterRect();
    }
    private void OpenCloseItemSetterRect()
    {
        Transform textTransform = openItemSetterRect.transform.GetChild(0);
        TMP_Text text = textTransform.GetComponent<TMP_Text>();
        if (itemSetterRectisOpenned)
        {
            itemSetterRect.gameObject.SetActive(false);
            itemSetterRectisOpenned = false;
            text.color = Color.white;
            text.text = "Set Item Numbers";
        }
        else
        {
            itemSetterRect.gameObject.SetActive(true);
            itemSetterRectisOpenned = true;
            text.color = Color.red;
            text.text = "Close";
        }
    }
    private void CoinNumberValueChange()
    {
        totalCoin = (int)coinSlider.value;
        tI1.text = totalCoin.ToString();
    }
    private void HexWallNumberValueChange()
    {
        totalHexWall = (int)HexWallSlider.value;
        tI2.text = totalHexWall.ToString();
        
    }
    private void TurretNumberValueChange()
    {
        totalTurret = (int)turretSlider.value;
        tI3.text = totalTurret.ToString();
    }
}
