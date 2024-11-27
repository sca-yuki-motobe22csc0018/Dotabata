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
    [SerializeField] private List<int> handsNumber = new List<int>();//‚Á‚Ä‚¢‚éèD@‘I‘ğ‚µ‚½èD‚ÍCSV‚Ì‰½”Ô–Ú‚Ìƒf[ƒ^‚È‚Ì‚©
    [SerializeField] private GameObject[] hands = new GameObject[8];
    [SerializeField] private GameObject MapCreator;
    [SerializeField] private GameObject handsWindow;
    [SerializeField] private GameObject[] handObjects;
    public List<int> HandNumber { get { return handsNumber; } set { handsNumber = value; } }
    private const float size = 1f;
    MapCreate MC;
    SpriteRenderer sr;
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
        selectNumber = -1;
        handsWindow.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        HandMake();
        handsWindow.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
    }
    private void Update()
    {
        if(selectNumber>=0)
        {
            if (handsNumber.Count == 0)
            {
                return;
            }
            for (int i = 0; i < handObjects.Length; i++)
            {
                sr = handObjects[i].GetComponent<SpriteRenderer>();
                sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, 0.0f);
            }
        }
    }

    private void HandMake()
    {
        for (int i=0;i<handsNumber.Count;i++)
        {
            Debug.Log(MC);
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
