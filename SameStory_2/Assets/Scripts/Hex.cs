using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hex : MonoBehaviour
{
    [SerializeField]
    private GlowHighlight highlight;
    private HexCoordinates hexCoordinates;

    [SerializeField]
    private HexType hexType;

    public Vector3Int HexCoords => hexCoordinates.GetHexCoords();

    public int GetCost() => hexType switch // цена кожного hex tile 
    {
        HexType.Difficult => 20,
        HexType.Default => 10,
        HexType.Road => 5,
        _ => throw new Exception($"Hex of type{hexType} not supported")
    };
    public bool IsObstacle() 
    { return this.hexType == HexType.Obstracle; }
    public bool IsWater()
    { return this.hexType == HexType.Water; }
    private void Awake()
    {
        hexCoordinates = GetComponent<HexCoordinates>();
        highlight = GetComponent<GlowHighlight>();
    }
    public void EnableHighlight()
    {
        highlight.ToggleGlow(true);
    }
    public void DisableHighlight()
    {
        highlight.ToggleGlow(false);
    }

    internal void ResetHighlight()
    {
        highlight.ResetGlowHighlight();
    }

    internal void HiglightPath()
    {
        highlight.HighlightValidPath();
    }
}

public enum HexType
{
    None,
    Default,
    Difficult,
    Road,
    Water,
    Obstracle
}