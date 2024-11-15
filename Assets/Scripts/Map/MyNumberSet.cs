using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class MyNumberSet : EventSet, IPointerClickHandler
{
    SelectHand sH;
    GameObject backGround;
    bool leftClick;
    // Start is called before the first frame update
    void Start()
    {
        backGround = GameObject.Find("BackGround");
        sH=backGround.GetComponent<SelectHand>();
        SetEvent setEvent = new SetEvent(PointerClick);
        SetEventType(click,setEvent);
        Debug.Log(sH);
        leftClick = false;
    }

    private void Update()
    {
        leftClick = Input.GetMouseButtonDown(0);
    }

    public void PointerClick()
    {
        //if(leftClick) { sH.SetSelectNumber = int.Parse(this.name);Debug.Log("”½‰ž‚µ‚Ä‚¢‚é"); }
    }

    public void OnPointer(PointerEventData eventData)
    {
        
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.pointerId == -1)
        {
            sH.SetSelectNumber = int.Parse(this.name);
            Debug.Log(name);
        }
    }
}
