using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerMove : MonoBehaviour
{
    private float Speed;
    public float SpeedNeutral;
    public float SpeedUp;
    public float maxSpeed;
    public float torqueSpeed;
    private PhysicsMaterial2D dynamicMaterial;
    private Collider2D col2D;
    public float bouncinessNeutral;
    public float bouncinessDown;
    public float bouncinessUp;
    public float friction;
    public GameObject PlayerSkin;
    private float skinSize=0.5f;
    [SerializeField] Rigidbody2D rb = new Rigidbody2D();

    public float PowerMaxTime;
    private float PowerCoolTimeRight;
    float PowerTimerRight;
    private float PowerCoolTimeLeft;
    float PowerTimerLeft;

    public float PowerCoolTimeNeutral;
    public float PowerCoolTimeDown;
    public float PowerCoolTimeUp;

    private string TitleSceneName="Title";
    private string GoalTagName="Goal";

    
    //public Image gaugeImage; // ゲージに使用するImageコンポーネント
    public Image gaugeImageRight; // ゲージに使用するImageコンポーネント
    public Image gaugeImageLeft; // ゲージに使用するImageコンポーネント
    [Range(0, 1)] public float fillAmountRight = 1.0f; // 塗りつぶし率
    [Range(0, 1)] public float fillAmountLeft = 1.0f; // 塗りつぶし率
    bool moveRight;
    bool moveLeft;
    bool move;
    [SerializeField] private Color fullHealthColor = Color.white;   // HP最大時の色
    [SerializeField] private Color lowHealthColor = Color.red;     // HP最小時の色
    [SerializeField] private float forceMultiplier = 10f; // 力の大きさを調整
    [SerializeField] private GameObject gameManger;
    private PlayerManager pm;
    [SerializeField] private GameObject playerCamera;
    private Camera cam;
    private float camSize;
    private float camSizeNotZero=0.01f;
    private float camZNeutral=10.0f;
    public float camSizeSpeed;
    public float camSizeNeutral;
    public float camSizeUp;
    public float camSizeDown;
    public GameObject Back;
    public float BackPos;

    //鉱石関連
    //Plus
    public static bool coolTimeDown; //移動クールタイム減少
    public bool knockbackDown; //ノックバック軽減
    public bool scoreUp; //短期間獲得スコア上昇
    public bool visibilityUp; //視界範囲上昇
    public bool lavaSpeedDown; //溶岩速度低下
    //Neutral
    public static bool chargeMax; //常時チャージ移動
    public bool destroyWithOneHit; //鉱石一撃破壊
    public static  bool speedUp; //移動距離UP
    //Minus
    public bool lavaSpeedUp; //溶岩速度上昇
    public static bool coolTimeUp; //移動クールタイム増加
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
        moveRight = false;
        moveLeft = false;
        move = false;
        PowerTimerRight = 0;
        PowerTimerLeft = 0;
        rb = GetComponent<Rigidbody2D>();
        dynamicMaterial = new PhysicsMaterial2D();
        PowerCoolTimeRight = PowerCoolTimeNeutral;
        PowerCoolTimeLeft = PowerCoolTimeNeutral;
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

    // 任意でゲージを設定するメソッド
    public void SetGaugeRight(float value)
    {
        fillAmountRight = Mathf.Clamp01(value); // 0～1にクランプ
    }
    public void SetGaugeLeft(float value)
    {
        fillAmountLeft = Mathf.Clamp01(value); // 0～1にクランプ
    }

    private void PlayerUpdate()
    {
        cam.orthographicSize = camSize;
        playerCamera.transform.position = new Vector3(transform.position.x, transform.position.y, -camZNeutral);
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
            SceneManager.LoadScene(TitleSceneName);
        }
        if (gaugeImageRight != null && gaugeImageLeft != null)
        {
            gaugeImageRight.fillAmount = fillAmountRight;
            gaugeImageLeft.fillAmount = fillAmountLeft;
        }
        //右翼
        if (Input.GetMouseButton(1) && PowerTimerRight < PowerMaxTime && !moveRight)
        {
            PowerTimerRight += Time.deltaTime;
        }

        if (Input.GetMouseButtonUp(1)&& !moveRight)
        {
            moveRight = true;
            //PlayerSkin.transform.localScale = new Vector3(-skinSize, skinSize, 1);
            rb.angularVelocity = 0f;
            rb.AddTorque(-torqueSpeed*PowerTimerRight);
        }
        //左翼
        if (Input.GetMouseButton(0)&& PowerTimerLeft < PowerMaxTime && !moveLeft)
        {
            PowerTimerLeft += Time.deltaTime;
        }
        if (Input.GetMouseButtonUp(0) && !moveLeft)
        {
            moveLeft = true;
            //PlayerSkin.transform.localScale = new Vector3(skinSize, skinSize, 1);
            rb.angularVelocity = 0f;
            rb.AddTorque(torqueSpeed * PowerTimerLeft);
        }
        if (moveLeft && moveRight && !move)
        {
            move=true;
            float Power = PowerTimerLeft+PowerTimerRight;
            rb.angularVelocity = 0f;
            rb.AddForce(transform.up * Power, ForceMode2D.Impulse);
        }
        if (moveRight)
        {
            PowerTimerRight -= PowerCoolTimeRight * Time.deltaTime;
            gaugeImageRight.color = lowHealthColor;
        }
        if (PowerTimerRight <= 0)
        {
            PowerTimerRight = 0;
            moveRight = false;
            move = false;
            gaugeImageRight.color = fullHealthColor;
        }
        SetGaugeRight(PowerTimerRight / PowerMaxTime);

        if (moveLeft)
        {
            PowerTimerLeft -= PowerCoolTimeLeft * Time.deltaTime;
            gaugeImageLeft.color = lowHealthColor;
        }
        if (PowerTimerLeft <= 0)
        {
            PowerTimerLeft = 0;
            moveLeft = false;
            move=false;
            gaugeImageLeft.color = fullHealthColor;
        }
        SetGaugeLeft(PowerTimerLeft / PowerMaxTime);

        //鉱石関連
        //Plus
        if (coolTimeDown)
        {
            if (PowerCoolTimeRight != PowerCoolTimeDown)
            {
                PowerCoolTimeRight = PowerCoolTimeDown;
                PowerCoolTimeLeft = PowerCoolTimeDown;
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
            if (Input.GetKey(KeyCode.D) && PowerTimerRight < PowerMaxTime && !moveRight)
            {
                PowerTimerRight = PowerMaxTime;
            }
            if (Input.GetKey(KeyCode.A) && PowerTimerLeft < PowerMaxTime && !moveLeft)
            {
                PowerTimerLeft = PowerMaxTime;
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
            if (PowerCoolTimeRight != PowerCoolTimeUp)
            {
                PowerCoolTimeRight = PowerCoolTimeUp;
                PowerCoolTimeLeft = PowerCoolTimeUp;
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
            if (PowerCoolTimeRight != PowerCoolTimeNeutral)
            {
                PowerCoolTimeRight = PowerCoolTimeNeutral;
                PowerCoolTimeLeft = PowerCoolTimeNeutral;
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
            if (camSizeNotZero < camSize - camSizeNeutral && -camSizeNotZero > camSize-camSizeNeutral)
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
        if(coll.gameObject.CompareTag(GoalTagName))
        {
            SceneManager.LoadScene(TitleSceneName);
        }
    }
}
