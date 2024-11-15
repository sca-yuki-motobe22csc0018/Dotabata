using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("��u�����������Ƃ��ɕۏႳ��Ă�Œ዗��")]
    private float minMovePower;
    [SerializeField, Header("�ő咷���������Ƃ��ɑ�����鋗��")]
    private float maxMovePower;
    [SerializeField, Header("�ő咷�����܂ł̎���")]
    private float maxChargeTime;
    private float onClickingTimeer;
    private Vector3 mousePosition;

    private Rigidbody2D rb;

    [SerializeField, Header("��C��R�ɂ�錸���̒��x"), Range(0.0f, 5.0f)]
    private float aerodynamicDragLevel;

    private Vector3 velocity;
    private float aerodynamicDragTimeer;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUpdate();
    }
    float EaseOutCirc(float t)
    {
        float x = (t - 1);

        return 1.0f > t ? 1 - Mathf.Sqrt(1 - x * x) : 0.0f;
    }

    void PlayerUpdate()
    {
        onClickingTimeer += Time.deltaTime;
        aerodynamicDragTimeer += Time.deltaTime * aerodynamicDragLevel;
        rb.velocity = velocity * EaseOutCirc(aerodynamicDragTimeer);

        if (Input.GetMouseButtonDown(0))
        {
            onClickingTimeer = 0.0f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            float movePowerRate = onClickingTimeer / maxChargeTime;
            if (onClickingTimeer >= maxChargeTime)
            {
                movePowerRate = 1.0f;
            }

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 moveVector = mousePosition - transform.position;
            moveVector.z = 0.0f;
            velocity = Vector3.Normalize(moveVector) * (minMovePower + maxMovePower * movePowerRate);
            aerodynamicDragTimeer = 0.0f;
        }
    }
}
