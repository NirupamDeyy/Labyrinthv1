using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemovingState : IBuildingState
{
    private int gameObjectIndex = -1;
    Grid grid;
    PreviewSystem previewSystem;
    GridData floorData;
    GridData cubeData;
    ObjectPlacer objectPlacer;
    SoundFeedback soundFeedback;

    public RemovingState(Grid grid,
                         PreviewSystem previewSystem,
                         GridData floorData,
                         GridData cubeData,
                         ObjectPlacer objectPlacer,
                         SoundFeedback soundFeedback)
    {
        this.grid = grid;
        this.previewSystem = previewSystem;
        this.floorData = floorData;
        this.cubeData = cubeData;
        this.objectPlacer = objectPlacer;
        this.soundFeedback = soundFeedback;

        previewSystem.StartShowingRemovePreview();
        this.soundFeedback = soundFeedback;
    }

    public void EndState()
    {
        previewSystem.StopShowingPreview();
    }

    public void OnAction(Vector3Int gridPosition)
    {
        GridData selectedData = null;
        if(cubeData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = cubeData;
        }
        else if (floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one) == false)
        {
            selectedData = floorData;
        }

        if(selectedData == null)
        {
            soundFeedback.PlaySound(SoundType.Remove);
        }
        else
        {
            gameObjectIndex = selectedData.GetRepresentationIndex(gridPosition);
            if (gameObjectIndex == -1)
                return;

            selectedData.RemoveObjectAt(gridPosition);
            objectPlacer.RemoveObjectAt(gameObjectIndex);
        }

        Vector3 cellPosition = grid.CellToWorld(gridPosition);
        previewSystem.UpdatePosition(cellPosition, checkIfSelectionIsValid(gridPosition));
        //Debug.Log("placing structure number " +  "at: " + gridPosition);

    }

    private bool checkIfSelectionIsValid(Vector3Int gridPosition)
    {
        return ! (cubeData.CanPlaceObjectAt(gridPosition, Vector2Int.one) && floorData.CanPlaceObjectAt(gridPosition, Vector2Int.one));
    }

    public void UpdateState(Vector3Int gridPosition)
    {
        bool validity = checkIfSelectionIsValid(gridPosition);
        previewSystem.UpdatePosition(grid.CellToWorld(gridPosition), validity); 
    }
}
