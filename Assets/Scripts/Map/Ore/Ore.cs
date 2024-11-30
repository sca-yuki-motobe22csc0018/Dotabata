using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// 担当:熊谷
/// </summary>

public class Ore : MonoBehaviour 
{
    GameObject player;
    Rigidbody2D playerRb;
    GameObject oreManager;
    SelectHand sh;
    OreData oreData=new OreData();
    public delegate void OreEvent();
    private const string playerTag = "Player";
    private short hitCount;
    private float hitTimer;
    private bool hitPlayer;
    private const float validVelocity=3.0f;
    private float playerPower = 0;
    
    public struct OreInfo{//鉱石の持つ情報
        public string name;//名前
        public int number;//番号
        public int durability;
        public float knockBackPower;//プレイヤーが鉱石に衝突したときに発生するノックバックの強さ
        public Sprite sprite;//見た目
        public List<OreEvent> events;//割り当てられる効果
        public List<int> eventPercentage;//各効果の発生割合

    }

    OreInfo info = new OreInfo();
    public OreInfo Info { get { return info; } set { info=value; } }

    private void Awake()
    {
        player = GameObject.Find(playerTag);
        Debug.Log(GameObject.Find("Main Camera"));
        sh=GameObject.Find("Main Camera").transform.GetChild(0).gameObject.transform.GetChild(0).GetComponent<SelectHand>();
        playerRb=player.GetComponent<Rigidbody2D>();
        oreManager = GameObject.Find("OreManager");
        oreData=oreManager.GetComponent<OreData>();
        info.sprite = oreData.GetSprite[info.number];
        info.name = oreData.GetNames[0];
        info.knockBackPower = 1;
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

    private void GetHand()//鉱石をはかいしたらマップを獲得
    {
        int size = sh.HandNumber.Count;
        const int maxHand = 8;
        if(size<maxHand)//現在所持しているマップの数が手札上限より少なければ
        {
            int getHandNumber = Random.Range(0, 8);//仮。ここの数字は後でマップ総量に変更
            sh.HandNumber.Add(getHandNumber);
        }
    }
    
}
