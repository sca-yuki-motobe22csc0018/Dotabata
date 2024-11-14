using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EventSet : MonoBehaviour
{
    public delegate void SetEvent();
    private string corectEventID;
    protected const string enter = "Enter";
    protected const string exit = "Exit";
    protected const string down = "Down";
    protected const string up = "Up";
    protected const string click  = "Click";
    public virtual void PointerEnter()
    {
        Debug.Log("���N���X���̊֐����Ă΂�Ă��܂�\n�p����̊֐����I�[�o�[���C�h���Ă�������");//���N���X���̊֐����Ăяo����Ă��邱�Ƃ�m�点��
    }

    public virtual void PointerDown()
    {
        Debug.Log("���N���X���̊֐����Ă΂�Ă��܂�\\n�p����̊֐����I�[�o�[���C�h���Ă�������\"");
    }

    public virtual void PointerExit()
    {
        Debug.Log("���N���X���̊֐����Ă΂�Ă��܂�\\n�p����̊֐����I�[�o�[���C�h���Ă�������\"");
    }

    public virtual void SetEventType(string eventID, SetEvent e)
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entry = new EventTrigger.Entry();

        switch (eventID)
        {
            case (enter):
                {
                    entry.eventID = EventTriggerType.PointerEnter;
                }
                break;
            case (down):
                {
                    entry.eventID = EventTriggerType.PointerDown;
                }
                break;
            case (exit):
                {
                    entry.eventID = EventTriggerType.PointerExit;
                }
                break;
            case (up):
                {
                    entry.eventID = EventTriggerType.PointerUp;
                }
                break;
        }

        entry.callback.AddListener((data) => { e(); });
        trigger.triggers.Add(entry);
    }
}
