using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class DefaultTileManager : EventSet
{
    private GameObject mapCreator;
    
    MapCreate mapCreate;
    private void Start()
    {
        SetEvent setEvent = new SetEvent(PointerClick);
        mapCreator = GameObject.Find("MapCreator");
        SetEventType(click, setEvent);
        mapCreate = mapCreator.GetComponent<MapCreate>();  
    }

    public void PointerClick()
    {
        mapCreate.SetPosition = this.transform.position;
        mapCreate.MakeMap = true;
        Destroy(this.gameObject);
    }
}
