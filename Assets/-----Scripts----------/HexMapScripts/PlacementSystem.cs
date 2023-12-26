
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlacementSystem : MonoBehaviour
{
    [SerializeField]
    private ItemTracking itemTracking;

    [SerializeField]
    private ShowInfoTextScript showInfoTextScript;

    [SerializeField]
    private InputManager inputManager;
    [SerializeField]
    private Grid grid;

    [SerializeField]
    private ObjectsDatabaseSO database;

    [SerializeField]
    private GameObject gridVisualization;

    [SerializeField]
    private PreviewSystem previewSystem;

    [SerializeField]
    private ObjectPlacer objectPlacer;

    [SerializeField]
    private SoundFeedback soundFeedback;

    private GridData floorData, cubeData;

    private Vector3Int lastDetectedPosition = Vector3Int.zero;

    

    [SerializeField]
    private bool isPlacing;

    [SerializeField]
    private int itemIDCache;

    IBuildingState buildingState;


    private void Start()
    {
        StopPlacement();
        floorData = new GridData();
        cubeData = new GridData();
    }

    public void StartPlacement(int ID)
    {
        StopPlacement();
        isPlacing = true;
        //Switch
        itemIDCache = ID;
        
        gridVisualization.SetActive(true);
        buildingState = new PlacementState(ID,
                                           grid,
                                           previewSystem,
                                           database,
                                           floorData,
                                           cubeData,
                                           objectPlacer,
                                           soundFeedback);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

    public void RemoveAll()
    {
        for (int x = -11; x <= 11; x++)
        {
            for (int y = -5; y <= 7; y++)
            {

            }
        }
    }

    public void StartRemoving()
    {
        Debug.Log("startRemoving");
        StopPlacement();
        isPlacing = false;
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, previewSystem, floorData, cubeData, objectPlacer, soundFeedback);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }
    public bool canGenerate = false;
    Vector3Int newPos;
    public int xOrigin, yorigin;
    public bool removeAll;

    public void RemoveAllItems()
    {
        Debug.Log("Removing all");
        StartRemoving();
        removeAll = true;
        createProceduraly = true;
        canGenerate = true;
        Invoke("iswhatfalse", 1f);
        
    }

    private void iswhatfalse()
    {
        canGenerate = false;
        createProceduraly = false;
        removeAll = false;
    }
    public IEnumerator  GenerateProceduralMap()
    {
        RemoveAllItems();
        yield return new WaitForSeconds(1);
        StartPlacement(1);
        createProceduraly = true;
        canGenerate = true;
        yield return new WaitForSeconds(1);
        iswhatfalse();
    }
    private void CreateProceduralMap( )
    {
        //Assign new position
        int[,] pos = new int[14,18];
        
        newPos = new Vector3Int();
        int i = 300;

        for (int a = 0; a < 12; a++)
        {
            for (int x = -a; x <= a; x++)
            {
                for (int y = -5; y <= 7; y++)
                {
                    if (i > 0 ) 
                    {
                        newPos = new Vector3Int(x, y, 0);
                        bool RandomBool = Random.value > 0.95;
                        if (RandomBool && ! removeAll)
                        {
                            PlaceStructure();
                            i--;
                        }

                        if (removeAll)
                        {
                            PlaceStructure();
                            
                        }
                    }
                }
            }
        
        }

        
    }

    IEnumerator GenerateTile( Vector3Int pos)
    {
        yield return new WaitForSeconds(10);
        Debug.Log(pos);
        newPos = pos;
        PlaceStructure();
    }
    public bool createProceduraly;

  
    private void PlaceStructure()
    {
         
        if(itemTracking.CanPlaceItems(itemIDCache) && isPlacing)
        {
            if(itemIDCache == 1)
            {
                showInfoTextScript.ShowInfoText("No Coin Left", 2);
            }
            else if(itemIDCache == 2)
            {
                showInfoTextScript.ShowInfoText("No Tile Left", 2);

            }
            return;
        }

        if(MouseOverUILayerObject.IsPointerOverUIObject())
        {
           
            return;
        }
        /*if (inputManager.IsPointerOverUI())
        {
            return;
        }*/
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition;
        if (!createProceduraly)
        {
            gridPosition = grid.WorldToCell(mousePosition);
        }
        else
        {
            gridPosition = newPos;
        }
        

        buildingState.OnAction(gridPosition);
        //Debug.Log(gridPosition);
        itemTracking.SavePositionsOfItems(itemIDCache, isPlacing, gridPosition);///////////
       //
    }

    public void StopPlacement()
    {
        if(buildingState == null)
            return;

        gridVisualization.SetActive(false);
        buildingState.EndState();
        inputManager.OnClicked -= PlaceStructure;
        inputManager.OnExit -= StopPlacement;
        lastDetectedPosition = Vector3Int.zero;
        buildingState = null;
    }

    private void Update()
    {
        if(canGenerate)
        {
           // StartPlacement(2);
            //StartRemoving();

            CreateProceduralMap();
            //canGenerate = false;
        }
        if (buildingState == null) 
            return;
        Vector3 mousePosition = inputManager.GetSelectedMapPosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        if(lastDetectedPosition != gridPosition)
        {
            buildingState.UpdateState(gridPosition);
            lastDetectedPosition = gridPosition;
        }
    }

    
}
