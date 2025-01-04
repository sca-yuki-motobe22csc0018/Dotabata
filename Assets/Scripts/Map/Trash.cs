using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour
{
    [SerializeField] GameObject selectHand;
    SelectHand sh;
    // Start is called before the first frame update
    void Start()
    {
        sh=selectHand.GetComponent<SelectHand>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerUp()
    {
        if(!sh.PutedMap&&PlayerManager.state==PlayerManager.PlayerState.MapCreate)
        {
            sh.HandNumber.RemoveAt(sh.SelectNumber);
            sh.SelectNumber = -1;
            sh.PutedMap = true;
            sh.HandMake();
            Destroy(sh.SelectedMap);
        }

      
    }
}
