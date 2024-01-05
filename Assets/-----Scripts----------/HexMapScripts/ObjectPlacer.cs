using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPlacer : MonoBehaviour
{
    public List<GameObject> placedGameObject = new();
    

    [SerializeField]
    private bool CanPlace;

    [SerializeField] 
    private PreviewSystem previewSystem;
    public CoinsTracker coinsTracker;
    public Color startingColor = Color.white;
    public Color endingColor = Color.blue;
    public int PlaceObject(GameObject prefab, Vector3 position)
    {
        GameObject newObject = Instantiate(prefab);
        //Debug.Log(prefab.name);
        
        if (newObject.tag == "coin")
        {
            Vector3 newPos = new Vector3(position.x, position.y, position.z);
            newObject.transform.position = newPos;
            coinsTracker.GetCoinsPrefab(newObject);
            CoinPrefabScript coinPrefabScript = newObject.GetComponent<CoinPrefabScript>();
            if (coinPrefabScript != null)
            {
                coinPrefabScript.IsPlaced();
            }
        }
        else
        {
            float randomInt = UnityEngine.Random.Range(0.0f, 0.1f);
            Vector3 newPos = new Vector3(position.x, position.y, position.z);
            Renderer renderer = newObject.GetComponentInChildren<Renderer>();
            Material childMat = renderer.material;
            
            Color randomColor = new Color(Random.Range(startingColor.r, endingColor.r),
                                          Random.Range(startingColor.g, endingColor.g),
                                          Random.Range(startingColor.b, endingColor.b));
            if (childMat != null)
            {
                childMat.SetColor("_BaseColor", randomColor);
            }
            newObject.transform.position = newPos;
        }

        placedGameObject.Add(newObject);

        //Debug.Log(placedGameObject.Count - 1);
        return placedGameObject.Count - 1;
    }

    internal void RemoveObjectAt(int gameObjectIndex)
    {
        if (placedGameObject.Count <= gameObjectIndex || placedGameObject[gameObjectIndex] == null) return;
        
        DOTween.Clear(placedGameObject[gameObjectIndex]);
               
        Destroy(placedGameObject[gameObjectIndex]);

        placedGameObject[gameObjectIndex] = null;
    }
}
