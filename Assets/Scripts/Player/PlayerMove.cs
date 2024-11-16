using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour
{
    float x = 0;
    float y = 0;
    private Vector3 StartPosition = new Vector3(0, 0, 0);
    Vector3 dir = Vector3.zero;
    public float speed;
    public float BoundForce;
    public GameObject PlayerSkin;
    [SerializeField] Rigidbody2D rb = new Rigidbody2D();
    public float PowerMaxTime;
    public float PowerCoolTime;
    float PowerTimer;
    float PowerTimeStock;
    private Vector3 moveDirection; // �ړ�����
    public Image gaugeImage; // �Q�[�W�Ɏg�p����Image�R���|�[�l���g
    [Range(0, 1)] public float fillAmount = 1.0f; // �h��Ԃ���
    bool move;
    public Color fullHealthColor = Color.white;   // HP�ő厞�̐F
    public Color lowHealthColor = Color.red;     // HP�ŏ����̐F
    public float forceMultiplier = 10f; // �͂̑傫���𒲐�
    // Start is called before the first frame update
    void Start()
    {
        move = false;
        PowerTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        this.transform.position = StartPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = fillAmount;
        }
        if (Input.GetMouseButton(0)&&PowerTimer<PowerMaxTime&&!move)
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
            Vector2 dir = (mouseP - transform.position).normalized;

            // Rigidbody2D�ɗ͂�������
            rb.AddForce(dir * PowerTimer/4, ForceMode2D.Impulse);
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
        SetGauge(PowerTimer/PowerMaxTime);

        // �}�E�X�̃X�N���[�����W���擾
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2D�Ȃ̂�Z���W�͖���

        // �I�u�W�F�N�g�̈ʒu����}�E�X�̈ʒu�܂ł̃x�N�g�����v�Z
        Vector2 direction = mousePosition - transform.position;

        // �x�N�g���̊p�x���擾���ĉ�]������
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        
        if (!move)
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
        
        
    }

    // �C�ӂŃQ�[�W��ݒ肷�郁�\�b�h
    public void SetGauge(float value)
    {
        fillAmount = Mathf.Clamp01(value); // 0�`1�ɃN�����v
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {/*
        if (collision.gameObject.CompareTag("Wall"))
        {
            rb.velocity = new Vector3(0, 0, 0);
            dir = new Vector3(0, 0, 0);
            dir += new Vector3(-x, y, 0).normalized * BoundForce * speed * Time.deltaTime;
            x *= 0;
            y *= 0;
        }
        if (collision.gameObject.CompareTag("Ceiling"))
        {
            rb.velocity = new Vector3(0, 0, 0);
            dir = new Vector3(0, 0, 0);
            dir += new Vector3(x, -y, 0).normalized * BoundForce * speed * Time.deltaTime;
            x = 0;
            y = 0;
        }
     */
    }
}
