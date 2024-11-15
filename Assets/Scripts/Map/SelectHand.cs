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
    [SerializeField] private GameObject handsWindow;
    [SerializeField] private GameObject[] handObjects;
    private const float size = 1f;
    private int makeNumber;
    MapCreate MC;
    SpriteRenderer sr;
    public int GetMakeNumber { get { return makeNumber; } }
    public int SelectNumber {  get { return selectNumber; } set {  selectNumber = value; } }

    private void OnEnable()
    {
        for (int i = 0; i < handObjects.Length; i++)
        {
            sr = handObjects[i].GetComponent<SpriteRenderer>();
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 1.0f);
        }
        DestroyLastHands();
        MC = MapCreator.GetComponent<MapCreate>();
        for (int i = 0; i < handsNumber.Length; i++)
        {
            handsNumber[i] = i;
        }
        selectNumber = -1;
        makeNumber = 0;
        handsWindow.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        HandMake();
        handsWindow.transform.localScale = new Vector3(0.7f, 0.7f, 0);
    }
    private void Start()
    {
        
    }

    private void Update()
    {
        if(selectNumber>=0)
        {
            makeNumber = handsNumber[selectNumber];
            for (int i = 0; i < handObjects.Length; i++)
            {
                sr = handObjects[i].GetComponent<SpriteRenderer>();
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);
            }
        }
    }

    private void HandMake()
    {
        for (int i=0;i<hands.Length;i++)
        {
            MC.PieceCreator(MC.PieceData, handsNumber[i], size,hands[i].transform.position, hands[i],false);
        }
    }

    private void DestroyLastHands()
    {
        for(int i=0;i<hands.Length;i++)
        {
            for(int j =0;j<hands[i].transform.childCount;j++)
            {
                Destroy(hands[i].transform.GetChild(j).gameObject);
            }
        }
    }
}
