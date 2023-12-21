using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;

public class GeneralPlayerFuncs : MonoBehaviour
{

    public CoinsTracker coinsTracker;
    public GunSwitchControl gunSwitchControl;

    public int updatedCoinCount;

    private void Start()
    {
        updatedCoinCount = 0;
    }
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("coin"))
        {
            UpdateCoin();
           // Destroy(other.gameObject);
            CoinPrefabScript coinPrefabScript = other.GetComponent<CoinPrefabScript>();
            if(coinPrefabScript != null)
            {
                coinPrefabScript.StartDestroying();
            }
            else
            {
                Debug.Log("coin prefab scriot is not present");
            }
        }
    }

    public void UpdateCoin()
    {
        updatedCoinCount++;
        coinsTracker.coinsCollected = updatedCoinCount;
        gunSwitchControl.UpdateCoinText(updatedCoinCount);
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
