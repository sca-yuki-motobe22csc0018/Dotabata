using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private float Speed;
    public float SpeedNeutral;
    public float SpeedUp;
    public float maxSpeed;
    private PhysicsMaterial2D dynamicMaterial;
    private Collider2D col2D;
    public float bouncinessNeutral;
    public float bouncinessDown;
    public float bouncinessUp;
    public float friction;
    public GameObject PlayerSkin;
    [SerializeField] Rigidbody2D rb = new Rigidbody2D();
    public float PowerMaxTime;
    private float PowerCoolTime;
    public float PowerCoolTimeNeutral;
    public float PowerCoolTimeDown;
    public float PowerCoolTimeUp;
    float PowerTimer;
    public Image gaugeImage; // ゲージに使用するImageコンポーネント
    [Range(0, 1)] public float fillAmount = 1.0f; // 塗りつぶし率
    bool move;
    [SerializeField] private Color fullHealthColor = Color.white;   // HP最大時の色
    [SerializeField] private Color lowHealthColor = Color.red;     // HP最小時の色
    [SerializeField] private float forceMultiplier = 10f; // 力の大きさを調整
    [SerializeField] private GameObject gameManger;
    private PlayerManager pm;
    [SerializeField] private GameObject playerCamera;
    private Camera cam;
    private float camSize;
    public float camSizeSpeed;
    public float camSizeNeutral;
    public float camSizeUp;
    public float camSizeDown;
    public GameObject Back;
    public float BackPos;

    //鉱石関連
    //Plus
    public bool coolTimeDown; //移動クールタイム減少
    public bool knockbackDown; //ノックバック軽減
    public bool scoreUp; //短期間獲得スコア上昇
    public bool visibilityUp; //視界範囲上昇
    public bool lavaSpeedDown; //溶岩速度低下
    //Neutral
    public bool chargeMax; //常時チャージ移動
    public bool destroyWithOneHit; //鉱石一撃破壊
    public bool speedUp; //移動距離UP
    //Minus
    public bool lavaSpeedUp; //溶岩速度上昇
    public bool coolTimeUp; //移動クールタイム増加
    public bool scoreDown; //3回獲得スコア減少
    public bool mapAmountDown; //所持可能マップ数低下
    public bool knockbackUp; //ノックバック増加
    public bool mapDropDown; //ピースドロップ率低下
    public bool mapBlind; //暗黙構想ピース化
    public bool visibilityDown; //視界範囲減少

    // Start is called before the first frame update
    void Start()
    {
        pm = gameManger.GetComponent<PlayerManager>();
        pm.AddFunction(PlayerUpdate);
        cam= playerCamera.GetComponent<Camera>();
        move = false;
        PowerTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        //physicsMaterial.bounciness = bouncinessNeutral;
        dynamicMaterial = new PhysicsMaterial2D();
        PowerCoolTime = PowerCoolTimeNeutral;
        Speed = SpeedNeutral;
        camSize = camSizeNeutral;

        col2D = GetComponent<Collider2D>();
        dynamicMaterial.bounciness = bouncinessNeutral; // 初期値
        dynamicMaterial.friction = friction;   // 任意
        col2D.sharedMaterial = dynamicMaterial;

        //鉱石関連
        coolTimeDown = false;
        knockbackDown = false;
        scoreUp = false;
        visibilityUp = false;
        lavaSpeedDown = false;
        chargeMax = false;
        destroyWithOneHit = false;
        speedUp = false;
        lavaSpeedUp = false;
        coolTimeUp = false;
        scoreDown = false;
        mapAmountDown = false;
        knockbackUp = false;
        mapDropDown = false;
        mapBlind = false;
        visibilityDown = false;
    }

    // Update is called once per frame
    private void Update()
    {
    }
    // 任意でゲージを設定するメソッド
    public void SetGauge(float value)
    {
        fillAmount = Mathf.Clamp01(value); // 0〜1にクランプ
    }

    private void PlayerUpdate()
    {
        cam.orthographicSize = camSize;
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
        if (Back != null&&BackPos!=0)
        {
            Back.transform.position=new Vector3(playerCamera.transform.position.x/BackPos,this.transform.position.y/BackPos,0);
        }

        dynamicMaterial.bounciness = Mathf.PingPong(Time.time, 1.0f);

        // 現在の速度を取得
        Vector2 velocity = rb.velocity;

        // 速度が上限を超えた場合、制限をかける
        if (velocity.magnitude > maxSpeed)
        {
            rb.velocity = velocity.normalized * maxSpeed;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = fillAmount;
        }
        if (Input.GetMouseButton(0) && PowerTimer < PowerMaxTime && !move)
        {
            PowerTimer += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0))
        {
            move = true;
            // マウスのワールド座標を取得
            Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseP.z = 0; // 2DのためZ軸を0に

            // クリックした方向を計算
            Vector2 dir = (mouseP - transform.position).normalized*Speed;

            // Rigidbody2Dに力を加える
            rb.AddForce(dir * PowerTimer, ForceMode2D.Impulse);
        }
        if (move)
        {
            PowerTimer -= PowerCoolTime * Time.deltaTime;
            gaugeImage.color = lowHealthColor;
        }
        if (PowerTimer <= 0)
        {
            PowerTimer = 0;
            move = false;
            gaugeImage.color = fullHealthColor;
        }
        SetGauge(PowerTimer / PowerMaxTime);

        // マウスのスクリーン座標を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2DなのでZ座標は無視

        // オブジェクトの位置からマウスの位置までのベクトルを計算
        Vector2 direction = mousePosition - transform.position;

        // ベクトルの角度を取得して回転させる
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        if (!move&&PlayerSkin!=null)
        {
            PlayerSkin.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
            if (PlayerSkin.transform.rotation.z > 0.7f || PlayerSkin.transform.rotation.z < -0.7f)
            {
                PlayerSkin.transform.localScale = new Vector3(-0.5f, -0.5f, 1);
            }
            else
            {
                PlayerSkin.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
            }
        }
        //鉱石関連
        //Plus
        if (coolTimeDown)
        {
            if (PowerCoolTime != PowerCoolTimeDown)
            {
                PowerCoolTime = PowerCoolTimeDown;
            }
        }
        if (knockbackDown)
        {
            if(dynamicMaterial.bounciness != bouncinessDown)
            {
                col2D = GetComponent<Collider2D>();
                dynamicMaterial.bounciness = bouncinessDown;
                col2D.sharedMaterial = dynamicMaterial;
            }
        }
        if (scoreUp)
        {

        }
        if (visibilityUp)
        {
            if (camSize >= camSizeUp)
            {
                camSize = camSizeUp;

            }else
            {
                camSize += Time.deltaTime* camSizeSpeed;
            }
        }
        if (lavaSpeedDown)
        {

        }
        //Neutral
        if (chargeMax)
        {
            if (Input.GetMouseButton(0) && PowerTimer < PowerMaxTime && !move)
            {
                PowerTimer = PowerMaxTime;
            }
        }
        if (destroyWithOneHit)
        {

        }
        if (speedUp)
        {
            if (Speed != SpeedUp)
            {
                Speed = SpeedUp;
            }
        }
        //Minus
        if (lavaSpeedUp)
        {

        }
        if (coolTimeUp)
        {
            if (PowerCoolTime != PowerCoolTimeUp)
            {
                PowerCoolTime = PowerCoolTimeUp;
            }
        }
        if (scoreDown)
        {

        }
        if (mapAmountDown)
        {

        }
        if (knockbackUp)
        {
            if (dynamicMaterial.bounciness != bouncinessUp)
            {
                col2D = GetComponent<Collider2D>();
                dynamicMaterial.bounciness = bouncinessUp;
                col2D.sharedMaterial = dynamicMaterial;
            }
        }
        if (mapDropDown)
        {

        }
        if (mapBlind)
        {

        }
        if (visibilityDown)
        {
            if (camSize <= camSizeDown)
            {
                camSize = camSizeDown;
            }
            else
            {
                camSize -= Time.deltaTime* camSizeSpeed;
            }
        }

        //戻す処理
        if (!coolTimeDown&&!coolTimeUp)
        {
            if (PowerCoolTime != PowerCoolTimeNeutral)
            {
                PowerCoolTime = PowerCoolTimeNeutral;
            }
        }
        if (!knockbackUp && !knockbackDown)
        {
            if (dynamicMaterial.bounciness != bouncinessNeutral)
            {
                col2D = GetComponent<Collider2D>();
                dynamicMaterial.bounciness = bouncinessNeutral;
                col2D.sharedMaterial = dynamicMaterial;
            }
        }
        if (!lavaSpeedDown&&lavaSpeedUp)
        {

        }
        if (!visibilityDown && !visibilityUp)
        {
            if(camSize<camSizeNeutral)
            {
                camSize += Time.deltaTime * camSizeSpeed;
            }
            if (camSize > camSizeNeutral)
            {
                camSize -= Time.deltaTime * camSizeSpeed;
            }
            if (0.01f < camSize - camSizeNeutral && -0.01 > camSize-camSizeNeutral)
            {
                camSize = camSizeNeutral;
            }
        }
        if (!speedUp)
        {
            Speed = SpeedNeutral;
        }
    }

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.CompareTag("Goal"))
        {
            SceneManager.LoadScene("Title");
        }
    }
   
}
