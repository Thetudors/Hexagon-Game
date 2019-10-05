using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hexagon : MonoBehaviour
{
    public Color[] Color;
    int randomColorIndex;
    public GameObject virtualTouch;
    public GameObject CenterObject;
    public GameObject tempCenterObject;
    public GameObject tempVirtualTouch;
    private GridController gridController;
    public GameObject[] neigHex;
    public bool isSelected = false;

    private void Awake()
    {
        neigHex = new GameObject[6];
    }
    void Start()
    {
        randomColorIndex = UnityEngine.Random.Range(0, 5);
        gameObject.GetComponent<SpriteRenderer>().color = Color[randomColorIndex];
        gameObject.tag = ColorToTag(randomColorIndex);
        gridController = GameObject.Find("Grid").GetComponent<GridController>();
        
    }

    public void ColliderOff()
    {
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = false;
        
    }
    public void ColliderOn()
    {
        this.gameObject.GetComponent<PolygonCollider2D>().enabled = true;

    }
    IEnumerator ExecuteAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        for (int i = 0; i < 6; i++)
        {
            var current = i;

            var next = i + 1 == 6 ? 0 : i + 1;

            if (neigHex[current] == null || neigHex[next] == null)
            {
                continue;
            }
            if (neigHex[current].tag == neigHex[next].tag && neigHex[current].tag == this.gameObject.tag)
            {
                Destroy(neigHex[current].gameObject);
                Destroy(neigHex[next].gameObject);
                Destroy(this.gameObject.gameObject);
                gridController.Hexagons.Remove(neigHex[current].GetComponent<Hexagon>());
                gridController.Hexagons.Remove(neigHex[next].GetComponent<Hexagon>());
                gridController.Hexagons.Remove(this.gameObject.GetComponent<Hexagon>());
                //neigHex[current].SetActive(false);
                //neigHex[next].SetActive(false);
                //this.gameObject.SetActive(false);
                gridController.isMatch = true;
            }
        }
    }

    public void MatchControl()
    {
        StartCoroutine(ExecuteAfterTime(0.5f));

    }
    public string ColorToTag(int colorid)
    {
        switch (colorid)
        {
            case 0:
                return "Red";
            case 1:
                return "Blue";
            case 2:
                return "Yellow";
            case 3:
                return "Green";
            case 4:
                return "Purple";
            default:
                return "Error";
        }
    }
    private void OnDestroy()
    {
        gridController.ScoreAdd();
    }
    private void OnMouseDown()
    {
        if (!isSelected)
        {
            if (gridController.tempSelectedHexagon == null)
            {
                gridController.tempSelectedHexagon = this.gameObject;
            }
            else
            {
                gridController.tempSelectedHexagon.GetComponent<Hexagon>().isSelected = false;
                gridController.tempSelectedHexagon = this.gameObject;
            }
            tempVirtualTouch = Instantiate(virtualTouch, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            isSelected = true;
        }
        
    }
    private void OnMouseUp()
    {
            CalcCenterPoint();       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.transform.tag == "VirtualBtn")
            return;


        if (collision.transform.parent == null || collision.transform.parent.gameObject == this.gameObject)
            return;

        if(collision.gameObject.tag == "Arrow")
        {
            neigHex[Convert.ToInt32(collision.gameObject.name)]=collision.gameObject.transform.parent.gameObject;
        }

        
    }

    public void ClearList()
    {
        neigHex = new GameObject[6];
    }

    public void CalcCenterPoint()
    {
        float pointX = (tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[0].transform.position.x + tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[1].transform.position.x + tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[2].transform.position.x) / 3;
        float pointY = (tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[0].transform.position.y + tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[1].transform.position.y + tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[2].transform.position.y) / 3;

        Vector2 vector2 = new Vector2(pointX, pointY);
        tempCenterObject = Instantiate(CenterObject, vector2, Quaternion.identity);
        tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[0].transform.parent = tempCenterObject.transform;
        tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[1].transform.parent = tempCenterObject.transform;
        tempVirtualTouch.GetComponent<VirtualTouchController>().selectedHexagon[2].transform.parent = tempCenterObject.transform;
    }
}
