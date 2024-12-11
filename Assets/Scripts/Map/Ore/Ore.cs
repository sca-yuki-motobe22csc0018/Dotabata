using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// �S��:�F�J
/// </summary>

public class Ore : MonoBehaviour 
{
    private GameObject player;
    private GameObject comboManager;
    private ComboManager cm;
    private Rigidbody2D playerRb;
    private GameObject oreManager;
    private SelectHand sh;
    private OreData oreData =new OreData();
    public delegate void OreEvent();
    private const string playerTag = "Player";
    private short hitCount;
    private float hitTimer;
    private bool hitPlayer;
    private const float validVelocity=3.0f;
    private float playerPower = 0;

    //�z�΂̎����
    public struct OreInfo
    {
        public string name;//���O
        public int number;//�ԍ�
        public int durability;
        public Sprite sprite;//������
        public List<OreEvent> events;//���蓖�Ă������
        public List<int> eventPercentage;//�e���ʂ̔�������

    }

    OreInfo info = new OreInfo();
    public OreInfo Info { get { return info; } set { info=value; } }

    private void Awake()
    {
        player = GameObject.Find(playerTag);
        comboManager=GameObject.Find("ComboManager");
        cm=comboManager.GetComponent<ComboManager>();
        Debug.Log(GameObject.Find("Main Camera"));
        sh=GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SelectHand>();
        playerRb=player.GetComponent<Rigidbody2D>();
        oreManager = GameObject.Find("OreManager");
        oreData=oreManager.GetComponent<OreData>();
        info.sprite = oreData.GetSprite[info.number];
        info.name = oreData.GetNames[0];
        OreData.EventPercentage ep = oreData.GetEventPercentages;
        info.eventPercentage = ep.oreOne;
        hitCount = 0;
        hitTimer = 0;
        hitPlayer = false;
        info.durability = 1;
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(cm);
    }

    // Update is called once per frame
    void Update()
    {
        if(hitPlayer){ hitTimer += Time.deltaTime;}
        if (hitTimer > 0.5f) { hitPlayer = false; }
        DestroyMe();
        playerPower= Mathf.Abs(playerRb.velocity.x) + Mathf.Abs(playerRb.velocity.y);
    }

    private void EventSet(OreEvent e)
    {
        info.events.Add(e);
    }

    private void DestroyMe()
    {
        if(hitCount>=info.durability)
        {
            GetHand();
            Debug.Log(cm.ComboCount);
            cm.ComboCount++;
            cm.ComboTimer = 0.0f;
            cm.ComboFlag = true;
            Destroy(this.transform.GetChild(0).gameObject);
            Destroy(this.gameObject.GetComponent<Ore>());
            Destroy(this.gameObject.GetComponent<Collider2D>());
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(!collision.gameObject.CompareTag(playerTag)||hitPlayer)
        {
            return;
        }
       
        if(playerPower>=validVelocity)
        {
            hitPlayer = true;
            hitTimer=0;
            hitCount++;
        }
    }

    private void GetHand()//�z�΂��͂���������}�b�v���l��
    {
        int size = sh.HandNumber.Count;
        const int maxHand = 8;
        if(size<maxHand)//���ݏ������Ă���}�b�v�̐�����D�����菭�Ȃ����
        {
            int getHandNumber = Random.Range(0, 8);//���B�����̐����͌�Ń}�b�v���ʂɕύX
            sh.HandNumber.Add(getHandNumber);
        }
    }
    
}
