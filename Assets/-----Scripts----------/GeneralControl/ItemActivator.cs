using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemActivator : MonoBehaviour
{
    public ObjectPlacer objectPlacer;
    TurretStateManager turretStateManager;
    enum Item { 
        Coin,
        Wall,
        Turret
    }

    public void ActivateItems()
    {
        ResetList(objectPlacer.placedGameObject);
        foreach(GameObject item in modifiedListpublic)
        {
            if(item.CompareTag("Turret"))
            {
                item.GetComponent<TurretStateManager>().enabled = true;
            }
        }
    }

    public List<GameObject> modifiedListpublic = new();
    private void ResetList(List<GameObject> itemPlacedList)
    {
        List<GameObject> modifiedList = new List<GameObject>();

        foreach (GameObject item in itemPlacedList)
        {
            if(item != null)
            {
                modifiedList.Add(item);
                modifiedListpublic.Add(item);
            }
            
        }


       // return modifiedList;
    }

    


}
