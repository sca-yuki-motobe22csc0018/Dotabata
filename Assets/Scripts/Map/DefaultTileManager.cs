using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class DefaultTileManager : EventSet,IPointerUpHandler
{
    private GameObject mapCreator;
    private GameObject selectedMap;
    private SelectHand sh;
    MapCreate mapCreate;
    private bool onCursor;
    private void Start()
    {
        onCursor = false;
        mapCreator = GameObject.Find("MapCreator");
        mapCreate = mapCreator.GetComponent<MapCreate>();  
        sh = GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(0).gameObject.GetComponent<SelectHand>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0)&&PlayerManager.state==PlayerManager.PlayerState.MapCreate&&onCursor)
        {
            mapCreate.PieceCreator(mapCreate.PieceData, sh.HandNumber[sh.SelectNumber], 1, this.transform.position);
            mapCreate.SetPosition = this.transform.position;
            mapCreate.MakeMap = true;
            selectedMap = sh.SelectedMap;
            var sMS = selectedMap.GetComponent<SpriteRenderer>();
            sMS.color=new Color(sMS.color.r, sMS.color.g, sMS.color.b,0.0f);
            
            Destroy(selectedMap);
            sh.Onemore = true;
            sh.PutedMap = true;
            sh.HandMake();
            selectedMap = null;
            Destroy(this.gameObject);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        Debug.Log("OnPointerUp");
        if(eventData.pointerId==-1&&PlayerManager.state==PlayerManager.PlayerState.MapCreate)
        {
            //mapCreate.SetPosition = this.transform.position;
            //mapCreate.MakeMap = true;
            //sh.SelectNumber = -1;
            //sh.Onemore = true;
            //selectedMap = sh.SelectedMap;
            //selectedMap.transform.position = this.transform.position;
            //selectedMap.transform.parent = null;
            //selectedMap.GetComponent<MyNumberSet>().enabled = false;
            //sh.PutedMap = true;
            //sh.HandNumber.Remove(sh.SelectNumber);
            //sh.HandMake();
            //selectedMap = null;
        }
      
    }

    public void OnPointerEnter()
    {
        onCursor = true;
    }
    public void OnPointerExit()
    {
        onCursor = false;
    }

    public void PointerUp()
    {
        if(onCursor)
        {
            Debug.Log("EN");
        }
    }
}
