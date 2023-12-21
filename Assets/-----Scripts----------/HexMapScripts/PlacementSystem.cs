using System;
using System.Collections;
using System.Collections.Generic;
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

    

    public void StartRemoving()
    {

        StopPlacement();
        isPlacing = false;
        gridVisualization.SetActive(true);
        buildingState = new RemovingState(grid, previewSystem, floorData, cubeData, objectPlacer, soundFeedback);
        inputManager.OnClicked += PlaceStructure;
        inputManager.OnExit += StopPlacement;
    }

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
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);

        buildingState.OnAction(gridPosition);

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
