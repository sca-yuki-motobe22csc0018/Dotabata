using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectHand : MonoBehaviour
{
    [SerializeField] private int selectHandNumber;
    [SerializeField] private int selectNumber;
    [SerializeField] private int[] handsNumber = new int[8];
    private int makeNumber;
    public int GetMakeNumber { get { return makeNumber; } }
    public int SetSelectNumber {  get { return selectNumber; } set {  selectNumber = value; } }
    private void Start()
    {
        for(int i = 0; i < handsNumber.Length; i++)
        {
            handsNumber[i] = i;
        }
        selectNumber = -1;
    }

    private void Update()
    {
        if(makeNumber>0)
        {
            makeNumber = handsNumber[selectNumber];
        }
      
    }
}
