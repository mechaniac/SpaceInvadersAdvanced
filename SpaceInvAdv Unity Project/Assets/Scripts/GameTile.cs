using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameTile : MonoBehaviour
{
    public int xCoord;
    public int zCoord;

    public bool isTaken = false;

    GameTile[] neighbours;

    Text text;

    public void InitializeTile(int x, int z, bool TileTextVisible)
    {
        xCoord = x;
        zCoord = z;

        text = GetComponentInChildren<Text>();
        text.text = $"{x} / {z}";

        text.gameObject.SetActive(TileTextVisible);
        
    }

    public void SetText(string addText)
    {
        text.text = $"{xCoord} / {zCoord}\n" + addText;
    }

    private void Update()
    {
        
        string setTileState ="";
        if(isTaken == true) { setTileState = "taken"; }
        SetText(setTileState);
    }
}
