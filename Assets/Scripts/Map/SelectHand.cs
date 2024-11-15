using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SelectHand : MonoBehaviour
{
    [SerializeField] private int selectNumber;//選択した手札　手札のどれを生成するか
    [SerializeField] private int[] handsNumber = new int[8];//持っている手札　選択した手札はCSVの何番目のデータなのか
    [SerializeField] private GameObject[] hands = new GameObject[8];
    [SerializeField] private GameObject MapCreator;
    private const float size=1.0f;
    private int makeNumber;
    MapCreate MC;
    public int GetMakeNumber { get { return makeNumber; } }
    public int SetSelectNumber {  get { return selectNumber; } set {  selectNumber = value; } }
    private void Start()
    {
        MC = MapCreator.GetComponent<MapCreate>();
        for(int i = 0; i < handsNumber.Length; i++)
        {
            handsNumber[i] = i;
        }
        selectNumber = -1;
        makeNumber = 0;
        HandMake();
    }

    private void Update()
    {
        if(selectNumber>=0)
        {
            makeNumber = handsNumber[selectNumber];
        }
        Debug.Log(makeNumber);
    }

    private void HandMake()
    {
        for(int i=0;i<hands.Length;i++)
        {
            MC.PieceCreator(MC.PieceData, handsNumber[i], size,hands[i].transform.position, hands[i],false);
        }
    }
}
