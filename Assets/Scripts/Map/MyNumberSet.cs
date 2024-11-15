using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNumberSet : EventSet
{
    SelectHand sH;
    GameObject backGround;
    // Start is called before the first frame update
    void Start()
    {
        backGround = GameObject.Find("BackGround");
        sH=backGround.GetComponent<SelectHand>();
        SetEvent setEvent = new SetEvent(PointerClick);
        SetEventType(click,setEvent);
        Debug.Log(sH);
    }

    public void PointerClick()
    {
        sH.SetSelectNumber=int.Parse(this.name);
    }







}
