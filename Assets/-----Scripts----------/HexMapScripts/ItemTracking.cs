using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ItemTracking : MonoBehaviour
{
    [SerializeField] 
    private TMP_Text[] itemTexts;
    public CoinsTracker coinsTracker;
    public int[] maxItemCounts;

    [SerializeField]
    private int[] itemsLeft;

    [SerializeField]
    public  List<Vector3Int> wallPositions = new();
    [SerializeField]
    public List<Vector3Int> FloorPositions = new();

    private void Start()
    {
        OnMaxNumberChange();
    }
    public void OnMaxNumberChange()
    {
        itemsLeft[0] = maxItemCounts[0];
        itemsLeft[1] = maxItemCounts[1];
        itemTexts[0].text = maxItemCounts[0].ToString();
        itemTexts[1].text = maxItemCounts[1].ToString();    
    }

    public bool AllItemsPlaced()
    {
        int cacheSumNum = 0;
        for (int i = 0; i < itemsLeft.Length; i++ )
        {
            cacheSumNum += itemsLeft[i];
        }

        if ( cacheSumNum <= 0 )
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void SavePositionsOfItems(int ID, bool isPlacin, Vector3Int pos)
    {
        if(isPlacin)
        {
            switch (ID)
            {
                case 1:
                    //save position of ID 0

                    FloorPositions.Add(pos);
          
                    break;
                case 2:
                    //save position of ID 1
                    wallPositions.Add(pos);
                    break;
                default:

                    break;

            }
        }
        else if(!isPlacin)
        {          
            FloorPositions.Remove(pos);
            wallPositions.Remove(pos);
        }
        UpdateItemTexts();
    }

    private void UpdateItemTexts()
    {
        itemsLeft[0] = (maxItemCounts[0] - FloorPositions.Count);
        itemsLeft[1] = (maxItemCounts[1] - wallPositions.Count);
        itemTexts[0].text = (maxItemCounts[0] - FloorPositions.Count).ToString();
        itemTexts[1].text = (maxItemCounts[1] - wallPositions.Count).ToString();

    }

    bool canPlace;
    public List<int> itemsCount = new();
    public bool CanPlaceItems(int ID)
    {
        //Debug.Log(ID);
        itemsCount.Clear();
        itemsCount.Insert(0, FloorPositions.Count);
        itemsCount.Insert(1, wallPositions.Count);

        /*itemsCount[0] = FloorPositions.Count;
        itemsCount[1] = wallPositions.Count;*/
        if(ID > 0)
        {
            if (itemsCount[ID - 1] < maxItemCounts[ID - 1])
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            Debug.Log("select an Item");
            return false;
            
        }
    }

}
