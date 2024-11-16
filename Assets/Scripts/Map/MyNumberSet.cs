using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class MyNumberSet : EventSet, IPointerClickHandler
{
    SelectHand sh;
    GameObject backGround;
    bool leftClick;
    SpriteRenderer sr;
    Collider2D collider;

    
    void Awake()
    {
        backGround = GameObject.Find("BackGround");
        sh = backGround.GetComponent<SelectHand>();
        collider = GetComponent<Collider2D>();
        
    }
    void OnEnable()
    {
        collider.enabled = true;
        sh.SelectNumber = -1;
        for (int i = 0; i < this.transform.childCount; i++)
        {
            
            sr = this.gameObject.transform.GetChild(i).GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
        SetEvent setEvent = new SetEvent(PointerClick);
        SetEventType(click,setEvent);
        leftClick = false;
       
     
    }

    private void Update()
    {
        leftClick = Input.GetMouseButtonDown(0);
        if(sh.SelectNumber>=0)
        {
            for (int i = 0; i < this.transform.childCount; i++)
            {
                sr = this.transform.GetChild(i).GetComponent<SpriteRenderer>();
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);
                
                sr = backGround.GetComponent<SpriteRenderer>();
                if (sr.color.a == 0.0f)
                {
                   
                    StartCoroutine(delay());
                }
            }
        }
        this.gameObject.SetActive(PlayerManager.state == PlayerManager.PlayerState.MapCreate);
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
            sh.SelectNumber = int.Parse(this.name);
        }
    }

    private IEnumerator delay()
    {
        yield return new WaitForSeconds(0.1f);
        collider.enabled = false;

    }
}
