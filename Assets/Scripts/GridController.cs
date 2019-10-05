using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridController : MonoBehaviour
{
    public int Row;
    public int Col;

    //private bool isSecond = true;
    public TextMeshProUGUI scoreTxt;
    public TextMeshProUGUI moveTxt;
    public GameObject hexagonPrefab;
    public GameObject tempSelectedHexagon;
    public List<Hexagon> Hexagons;
    public bool isMatch = false;

    public int moveCount;
    public int Score;
    void Start()
    {
        Hexagons = new List<Hexagon>();
        CreateGrid();
        
    }

    public void HexagonNeigClear()
    {
        foreach (var hex in Hexagons)
        {
            hex.ClearList();
        }
    }
    
    public void ScoreAdd()
    {
        Score += 5;
        scoreTxt.SetText("Score : " + Score);
    }

    public void MatchHexagons(Hexagon[] hexagons)
    {
        
        foreach (var hex in hexagons)
        {
            hex.MatchControl();
        }
    }

    public void MoveAdd()
    {
        moveCount++;
        moveTxt.SetText("Hamle: " + moveCount);
    }

    public void HexagonOffCollider()
    {
        foreach (var hex in Hexagons)
        {
            hex.ColliderOff();
        }
    }
    public void HexagonOnCollider()
    {
        foreach (var hex in Hexagons)
        {
            hex.ColliderOn();
        }
    }
    public void CreateGrid()
    {
        for (int i = 0; i < Row; i++)
        {
           
            for (int j = 0; j < Col; j++)
            {
                Vector2 curPosition;

                if (i%2==0)
                    curPosition = new Vector2(i, j - 0.5f);
                else
                    curPosition = new Vector2(i, j);
                GameObject tempHex = Instantiate(hexagonPrefab, curPosition, Quaternion.identity);
               
                Hexagons.Add(tempHex.GetComponent<Hexagon>());
            }
        }
    }
}
