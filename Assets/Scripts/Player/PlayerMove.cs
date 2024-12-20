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

    
    //public Image gaugeImage; // �Q�[�W�Ɏg�p����Image�R���|�[�l���g
    public Image gaugeImageRight; // �Q�[�W�Ɏg�p����Image�R���|�[�l���g
    public Image gaugeImageLeft; // �Q�[�W�Ɏg�p����Image�R���|�[�l���g
    [Range(0, 1)] public float fillAmountRight = 1.0f; // �h��Ԃ���
    [Range(0, 1)] public float fillAmountLeft = 1.0f; // �h��Ԃ���
    bool moveRight;
    bool moveLeft;
    bool move;
    [SerializeField] private Color fullHealthColor = Color.white;   // HP�ő厞�̐F
    [SerializeField] private Color lowHealthColor = Color.red;     // HP�ŏ����̐F
    [SerializeField] private float forceMultiplier = 10f; // �͂̑傫���𒲐�
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

    //�z�Ί֘A
    //Plus
    public static bool coolTimeDown; //�ړ��N�[���^�C������
    public bool knockbackDown; //�m�b�N�o�b�N�y��
    public bool scoreUp; //�Z���Ԋl���X�R�A�㏸
    public bool visibilityUp; //���E�͈͏㏸
    public bool lavaSpeedDown; //�n�⑬�x�ቺ
    //Neutral
    public static bool chargeMax; //�펞�`���[�W�ړ�
    public bool destroyWithOneHit; //�z�Έꌂ�j��
    public static  bool speedUp; //�ړ�����UP
    //Minus
    public bool lavaSpeedUp; //�n�⑬�x�㏸
    public static bool coolTimeUp; //�ړ��N�[���^�C������
    public bool scoreDown; //3��l���X�R�A����
    public bool mapAmountDown; //�����\�}�b�v���ቺ
    public bool knockbackUp; //�m�b�N�o�b�N����
    public bool mapDropDown; //�s�[�X�h���b�v���ቺ
    public bool mapBlind; //�Öٍ\�z�s�[�X��
    public bool visibilityDown; //���E�͈͌���

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
        dynamicMaterial.bounciness = bouncinessNeutral; // �����l
        dynamicMaterial.friction = friction;   // �C��
        col2D.sharedMaterial = dynamicMaterial;

        //�z�Ί֘A
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

    // �C�ӂŃQ�[�W��ݒ肷�郁�\�b�h
    public void SetGaugeRight(float value)
    {
        fillAmountRight = Mathf.Clamp01(value); // 0�`1�ɃN�����v
    }
    public void SetGaugeLeft(float value)
    {
        fillAmountLeft = Mathf.Clamp01(value); // 0�`1�ɃN�����v
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

        // ���݂̑��x���擾
        Vector2 velocity = rb.velocity;

        // ���x������𒴂����ꍇ�A������������
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
        //�E��
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
        //����
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

        //�z�Ί֘A
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

        //�߂�����
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
