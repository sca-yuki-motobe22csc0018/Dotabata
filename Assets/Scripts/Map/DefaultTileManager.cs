using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class DefaultTileManager : EventSet, IPointerClickHandler
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
      
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.pointerId==-1&&PlayerManager.state==PlayerManager.PlayerState.MapCreate)
        {
            mapCreate.SetPosition = this.transform.position;
            mapCreate.MakeMap = true;
            Destroy(this.gameObject);
        }
       
    }
}
