using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �S��:�F�J
/// </summary>

public class Ore : MonoBehaviour 
{
    [SerializeField]
    string n;
    GameObject oreManager;
    OreData oreData=new OreData();
    public delegate void OreEvent();
    public struct OreInfo{//�z�΂̎����
        public string name;//���O
        public int number;//�ԍ�
        public Sprite sprite;//������
        public List<OreEvent> events;//���蓖�Ă������
        public List<int> eventPercentage;//�e���ʂ̔�������
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
