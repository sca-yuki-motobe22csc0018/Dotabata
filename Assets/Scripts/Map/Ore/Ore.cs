using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 担当:熊谷
/// </summary>

public class Ore : MonoBehaviour 
{
    [SerializeField]
    string n;
    GameObject oreManager;
    OreData oreData=new OreData();
    public delegate void OreEvent();
    public struct OreInfo{//鉱石の持つ情報
        public string name;//名前
        public int number;//番号
        public Sprite sprite;//見た目
        public List<OreEvent> events;//割り当てられる効果
        public List<int> eventPercentage;//各効果の発生割合
    }

    OreInfo info = new OreInfo();
    public OreInfo Info { get { return info; } set { info=value; } }

    private void Awake()
    {
        oreManager = GameObject.Find("OreManager");
        oreData=oreManager.GetComponent<OreData>();
        info.sprite = oreData.GetSprite[info.number];
        info.name = oreData.GetNames[0];
        
        n = info.name;
        OreData.EventPercentage ep = oreData.GetEventPercentages;
        info.eventPercentage = ep.oreOne;
    }
    // Start is called before the first frame update
    void Start()
    {
   
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            Debug.Log(info.name);
        }
    }

    private void EventSet(OreEvent e)
    {
        info.events.Add(e);
    }
}
