using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VirtualTouchController : MonoBehaviour
{

    public List<GameObject> selectedHexagon;

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.tag == "Arrow")
            return;
            
            
            Debug.Log(collision.gameObject.transform.position);
            selectedHexagon.Add(collision.gameObject);
        
    }

    
    
}
