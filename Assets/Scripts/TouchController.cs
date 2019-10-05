using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchController : MonoBehaviour
{
    private GridController gridController;
    void Start()
    {
        gridController = GameObject.Find("Grid").GetComponent<GridController>();
    }



    public void TurnRight()
    {
        gridController.HexagonNeigClear();
        gridController.MoveAdd();
        StartCoroutine(Rotate(Vector3.forward, 120, 1.0f));
    }
    public void TurnLeft()
    {
        gridController.HexagonNeigClear();
        gridController.MoveAdd();
        StartCoroutine(Rotate(Vector3.forward, -120, 1.0f));
    }

    IEnumerator Rotate(Vector3 axis, float angle, float duration = 1.0f)
    {
        for (int i = 0; i < 3; i++)
        {


            gridController.HexagonOffCollider();
            gridController.HexagonNeigClear();
            Quaternion from = gridController.tempSelectedHexagon.transform.parent.gameObject.transform.rotation;
            Quaternion to = gridController.tempSelectedHexagon.transform.parent.gameObject.transform.rotation;
            to *= Quaternion.Euler(axis * angle);

            float elapsed = 0.0f;
            while (elapsed < duration)
            {
                gridController.tempSelectedHexagon.transform.parent.gameObject.transform.rotation = Quaternion.Slerp(from, to, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            gridController.tempSelectedHexagon.transform.parent.gameObject.transform.rotation = to;
            Hexagon[] hexagons = new Hexagon[3];
            hexagons[0] = gridController.tempSelectedHexagon.transform.parent.GetChild(0).GetComponent<Hexagon>();
            hexagons[1] = gridController.tempSelectedHexagon.transform.parent.GetChild(1).GetComponent<Hexagon>();
            hexagons[2] = gridController.tempSelectedHexagon.transform.parent.GetChild(2).GetComponent<Hexagon>();
            gridController.HexagonOnCollider();
            gridController.MatchHexagons(hexagons);
            yield return new WaitForSeconds(2);

            if (gridController.isMatch)
            {
                gridController.isMatch = false;
                yield break;
            }
        }

    }
}
