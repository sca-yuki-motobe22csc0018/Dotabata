using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class SelectHand : MonoBehaviour
{
    [SerializeField] private int selectNumber;//‘I‘ğ‚µ‚½èD@èD‚Ì‚Ç‚ê‚ğ¶¬‚·‚é‚©
    [SerializeField] private int[] handsNumber = new int[8];//‚Á‚Ä‚¢‚éèD@‘I‘ğ‚µ‚½èD‚ÍCSV‚Ì‰½”Ô–Ú‚Ìƒf[ƒ^‚È‚Ì‚©
    [SerializeField] private GameObject[] hands = new GameObject[8];
    [SerializeField] private GameObject MapCreator;
    private const float size = 1.0f;
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
