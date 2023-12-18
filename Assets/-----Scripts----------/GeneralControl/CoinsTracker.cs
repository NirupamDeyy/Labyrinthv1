using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinsTracker : MonoBehaviour
{
    public ItemTracking itemTracking;
    public ObjectPlacer objectplacer;
    public GunSwitchControl gunSwitchControl;
    [SerializeField] private int totalNumberOfCoins;
    [SerializeField] private List<GameObject> coins;
    public int coinsCollected;
    public ActionUIconrol actionUIconrol;
    public void SetTotalNumberOfCoins()
    {
        coinsCollected = 0;
        totalNumberOfCoins = itemTracking.maxItemCounts[0];
        checkWinSituation(coinsCollected, totalNumberOfCoins);
        if(isActiveAndEnabled)
        {
            gunSwitchControl.totalCoinsText.text = totalNumberOfCoins.ToString();

        }
        //UpdateCoinsCollectedText();
    }

    public void UpdateCoinsCollectedText()
    {
        gunSwitchControl.coinsText.text = coinsCollected.ToString();
    }

    // need to call this when a coin prefab is placed on the base
    // this is getting called in objectPlacer > PlaceObject
    public void GetCoinsPrefab(GameObject coinPrefab) 
    {
        coins.Add(coinPrefab);
    }

    private void checkWinSituation(int currentCoin, int totalCoin)
    {
        if(currentCoin >= totalCoin)
        {
            YouWon();
        }
    }

    private void YouWon()
    {
        actionUIconrol.Pause();
        actionUIconrol.ShowWinText(coinsCollected);
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        SetTotalNumberOfCoins();
    }
}
