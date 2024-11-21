using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// ’S“–:ŒF’J
/// </summary>

public class Ore : MonoBehaviour 
{
    [SerializeField]
    string n;
    GameObject oreManager;
    OreData oreData=new OreData();
    public delegate void OreEvent();
    public struct OreInfo{//zÎ‚Ì‚Âî•ñ
        public string name;//–¼‘O
        public int number;//”Ô†
        public Sprite sprite;//Œ©‚½–Ú
        public List<OreEvent> events;//Š„‚è“–‚Ä‚ç‚ê‚éŒø‰Ê
        public List<int> eventPercentage;//ŠeŒø‰Ê‚Ì”­¶Š„‡
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
