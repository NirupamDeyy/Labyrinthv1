using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemTracking : MonoBehaviour
{
    [SerializeField]
    private ShowInfoTextScript showInfoText;
    [SerializeField] 
    private TMP_Text[] itemTexts;
    public CoinsTracker coinsTracker;
    public int[] maxItemCounts;
    public bool isGeneratingProcedurally;
    [SerializeField]
    private int[] itemsLeft;

    [SerializeField]
    public  List<Vector3Int> wallPositions = new();
    [SerializeField]
    public List<Vector3Int> FloorPositions = new();
    [SerializeField]
    public List<Vector3Int> TurretPositions = new();

    private void Start()
    {
        OnMaxNumberChange();
    }
    public void OnMaxNumberChange()
    {
        itemsLeft[0] = maxItemCounts[0];
        itemsLeft[1] = maxItemCounts[1];
        itemsLeft[2] = maxItemCounts[2];
        itemTexts[0].text = maxItemCounts[0].ToString();
        itemTexts[1].text = maxItemCounts[1].ToString();
        itemTexts[2].text = maxItemCounts[2].ToString();
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
        if (isPlacin)
        {
            if (FloorPositions.Contains(pos) || wallPositions.Contains(pos) || TurretPositions.Contains(pos) )
            {
                if (!isGeneratingProcedurally)
                    showInfoText.ShowInfoText("Cannot Place Here.", 2);
            }
            else
            {
                switch (ID)
                {
                    case 1:
                        //save position of ID 0

                        FloorPositions.Add(pos);
                        if(!isGeneratingProcedurally)
                            showInfoText.ShowInfoText("Coin Placed at " + pos + " .", 0);
                        break;
                    case 2:
                        //save position of ID 1
                        wallPositions.Add(pos);
                        if (!isGeneratingProcedurally)
                            showInfoText.ShowInfoText("Wall Placed at " + pos + " .", 0);
                        break;
                    case 3:
                        //save position of ID 1
                        TurretPositions.Add(pos);
                        if (!isGeneratingProcedurally)
                            showInfoText.ShowInfoText("Wall Placed at " + pos + " .", 0);
                        break;
                    default:

                        break;

                }
            }
           
        }
        else if (!isPlacin)
        {
            if (FloorPositions.Contains(pos))
            {
                showInfoText.ShowInfoText("Coin Removed From " + pos + " .", 1);

            }
            else if (wallPositions.Contains(pos))
            {
                showInfoText.ShowInfoText("Wall Removed From " + pos + " .", 1);

            }
            else if (TurretPositions.Contains(pos))
            {
                showInfoText.ShowInfoText("Turret Removed From " + pos + " .", 1);

            }
            FloorPositions.Remove(pos);
            wallPositions.Remove(pos);
            TurretPositions.Remove(pos);
        }
        UpdateItemTexts();
    }

    private void UpdateItemTexts()
    {
        itemsLeft[0] = (maxItemCounts[0] - FloorPositions.Count);
        itemsLeft[1] = (maxItemCounts[1] - wallPositions.Count);
        itemsLeft[2] = (maxItemCounts[2] - TurretPositions.Count);
        itemTexts[0].text = (maxItemCounts[0] - FloorPositions.Count).ToString();
        itemTexts[1].text = (maxItemCounts[1] - wallPositions.Count).ToString();
        itemTexts[2].text = (maxItemCounts[2] - TurretPositions.Count).ToString();
    }

    bool canPlace;
    public List<int> itemsCount = new();
    public bool CanPlaceItems(int ID)
    {
        //Debug.Log(ID);
        itemsCount.Clear();
        itemsCount.Insert(0, FloorPositions.Count);
        itemsCount.Insert(1, wallPositions.Count);
        itemsCount.Insert(2, TurretPositions.Count);

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
            showInfoText.ShowInfoText("Please Select An Item", 1);
            return false;
            
        }
    }

}
