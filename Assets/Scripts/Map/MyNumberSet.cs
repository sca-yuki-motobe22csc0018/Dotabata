using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyNumberSet : EventSet
{
    SelectHand sH;
    // Start is called before the first frame update
    void Start()
    {
        sH=transform.parent.GetComponent<SelectHand>();
        SetEvent setEvent = new SetEvent(PointerClick);
        SetEventType(click,setEvent);

    }

    public void PointerClick()
    {
        sH.SetSelectNumber=int.Parse(this.name);
    }

    public void PointrerEnter()
    {
        sH.SetSelectNumber = -1;//‰½‚à‘I‘ð‚µ‚Ä‚¢‚È‚¢
    }






}
