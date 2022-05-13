using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitManager : MonoBehaviour
{
    [SerializeField]
    private HexGrid hexGrid;
    [SerializeField]
    private MovementSystem movementSystem;
    public bool PlayersTurn { get; private set; } = true;
    [SerializeField]
    private Unit selectedUnit;
    private Hex previouslySelectedHex;

    public void HandUnitSelected(GameObject unit)
    {
        if (PlayersTurn == false)
            return;
        Unit unitReference = unit.GetComponent<Unit>();
        if (ChecIfTheSameUnitSelected(unitReference))
            return;
        PrepareUnitForMovement(unitReference);
    }

    private bool ChecIfTheSameUnitSelected(Unit unitReference)
    {
        if (this.selectedUnit == unitReference)
        {
            ClearOldSelection();
            return true;
        }
        return false;
    }

    public void HandleTerrainsSelected(GameObject hexGo)
    {
        if (selectedUnit == null || PlayersTurn == false)
        {
            return;
        }
        Hex selectedHex = hexGo.GetComponent<Hex>();
        if (HandleHexOutRange(selectedHex.HexCoords) || HandleSelectedIsUnitHex(selectedHex.HexCoords))
            return;
        HandleTargetHexSelected(selectedHex, selectedHex.HexCoords);

    }
    private void PrepareUnitForMovement(Unit unitReference)
    {
        if (this.selectedUnit != null)
        {
            ClearOldSelection();
        }
        this.selectedUnit = unitReference;
        this.selectedUnit.Select();
        movementSystem.ShowRange(this.selectedUnit, this.hexGrid);
    }
    private void ClearOldSelection()
    {
        previouslySelectedHex = null;
        this.selectedUnit.Deselect();
        movementSystem.HideRange(this.hexGrid);
        this.selectedUnit = null;
    }
    private void HandleTargetHexSelected(Hex selectedHex, Vector3Int hexCoords)
    {
        if (previouslySelectedHex == null || previouslySelectedHex != selectedHex)
        {
            previouslySelectedHex = selectedHex;
            movementSystem.ShowPath(selectedHex.HexCoords, this.hexGrid);
        }
        else
        {
            movementSystem.MoveUnit(selectedUnit, this.hexGrid);
            PlayersTurn = false;
            selectedUnit.MovementFinished += ResetTurn;
            ClearOldSelection();
        }
    }
    private bool HandleSelectedIsUnitHex(Vector3Int hexPosition)
    {
        if (hexPosition == hexGrid.GetClosesHex(selectedUnit.transform.position))
        {
            selectedUnit.Deselect();
            ClearOldSelection();
            return true;
        }
        return false;
    }
    private bool HandleHexOutRange(Vector3Int hexPosition)
    {
        if (movementSystem.IsHexInRange(hexPosition) == false)
        {
            Debug.Log("Hex Out of range!");
            return true;
        }
        return false;
    }
    private void ResetTurn(Unit selectedUnit)
    {
        selectedUnit.MovementFinished -= ResetTurn;
        PlayersTurn = true;
    }
}
