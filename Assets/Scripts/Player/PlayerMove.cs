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
    public float speed;
    public float maxSpeed;
    public PhysicsMaterial2D physicsMaterial;
    public float bouncinessNeutral;
    public float bouncinessDown;
    public float bouncinessUp;
    public GameObject PlayerSkin;
    [SerializeField] Rigidbody2D rb = new Rigidbody2D();
    public float PowerMaxTime;
    public float PowerCoolTime;
    float PowerTimer;
    public Image gaugeImage; // �Q�[�W�Ɏg�p����Image�R���|�[�l���g
    [Range(0, 1)] public float fillAmount = 1.0f; // �h��Ԃ���
    bool move;
    [SerializeField] private Color fullHealthColor = Color.white;   // HP�ő厞�̐F
    [SerializeField] private Color lowHealthColor = Color.red;     // HP�ŏ����̐F
    [SerializeField] private float forceMultiplier = 10f; // �͂̑傫���𒲐�
    [SerializeField] private GameObject gameManger;
    private PlayerManager pm;
    [SerializeField] private GameObject camera;
    private Camera cam;
    public float camSize;
    public GameObject Back;
    public float BackPos;

    //�z�Ί֘A
    //Plus
    public bool coolTimeDown; //�ړ��N�[���^�C������
    public bool knockbackDown; //�m�b�N�o�b�N�y��
    public bool scoreUp; //�Z���Ԋl���X�R�A�㏸
    public bool visibilityUp; //���E�͈͏㏸
    public bool lavaSpeedDown; //�n�⑬�x�ቺ
    //Neutral
    public bool chargeMax; //�펞�`���[�W�ړ�
    public bool destroyWithOneHit; //�z�Έꌂ�j��
    public bool speedUp; //�ړ�����UP
    //Minus
    public bool lavaSpeedUp; //�n�⑬�x�㏸
    public bool coolTimeUp; //�ړ��N�[���^�C������
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
        cam=camera.GetComponent<Camera>();
        move = false;
        PowerTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        physicsMaterial.bounciness = bouncinessNeutral;

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

    // Update is called once per frame
    private void Update()
    {
    }
    // �C�ӂŃQ�[�W��ݒ肷�郁�\�b�h
    public void SetGauge(float value)
    {
        fillAmount = Mathf.Clamp01(value); // 0�`1�ɃN�����v
    }

    private void PlayerUpdate()
    {
        cam.orthographicSize = camSize;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
        if (Back != null&&BackPos!=0)
        {
            Back.transform.position=new Vector3(camera.transform.position.x/BackPos,this.transform.position.y/BackPos/2,0);
        }

        // ���݂̑��x���擾
        Vector2 velocity = rb.velocity;

        // ���x������𒴂����ꍇ�A������������
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
            // �}�E�X�̃��[���h���W���擾
            Vector3 mouseP = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseP.z = 0; // 2D�̂���Z����0��

            // �N���b�N�����������v�Z
            Vector2 dir = (mouseP - transform.position).normalized*speed;

            // Rigidbody2D�ɗ͂�������
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

        // �}�E�X�̃X�N���[�����W���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D�Ȃ̂�Z���W�͖���

        // �I�u�W�F�N�g�̈ʒu����}�E�X�̈ʒu�܂ł̃x�N�g�����v�Z
        Vector2 direction = mousePosition - transform.position;

        // �x�N�g���̊p�x���擾���ĉ�]������
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
        //�z�Ί֘A
        //Plus
        if (coolTimeDown)
        {

        }
        if (knockbackDown)
        {

        }
        if (scoreUp)
        {

        }
        if (visibilityUp)
        {

        }
        if (lavaSpeedDown)
        {

        }
        //Neutral
        if (chargeMax)
        {

        }
        if (destroyWithOneHit)
        {

        }
        if (speedUp)
        {

        }
        //Minus
        if (lavaSpeedUp)
        {

        }
        if (coolTimeUp)
        {

        }
        if (scoreDown)
        {

        }
        if (mapAmountDown)
        {

        }
        if (knockbackUp)
        {

        }
        if (mapDropDown)
        {

        }
        if (mapBlind)
        {

        }
        if (visibilityDown)
        {

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
