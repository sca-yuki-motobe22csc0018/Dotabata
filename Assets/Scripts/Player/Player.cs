using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField, Header("一瞬だけ押したときに保障されてる最低距離")]
    private float minMovePower;
    [SerializeField, Header("最大長押ししたときに足される距離")]
    private float maxMovePower;
    [SerializeField, Header("最大長押しまでの時間")]
    private float maxChargeTime;
    private float onClickingTimeer;
    private Vector3 mousePosition;

    [SerializeField]
    private GameObject player;
    private Rigidbody2D rb;

    [SerializeField]
    private GameObject GameManager;
    PlayerManager pm;

    [SerializeField]
    private GameObject camera;
    private Camera cam;
    [SerializeField, Header("空気抵抗による減衰の程度"), Range(0.0f, 5.0f)]
    private float aerodynamicDragLevel;

    private Vector3 velocity;
    private float aerodynamicDragTimeer;

    // Start is called before the first frame update
    void Start()
    {
        rb = player.GetComponent<Rigidbody2D>();
        cam = camera.GetComponent<Camera>();
        pm = GameManager.GetComponent<PlayerManager>();
        pm.AddFunction(PlayerUpdate, 0);
    }

    // Update is called once per frame
    void Update()
    {
       //PlayerUpdate();
       if(PlayerManager.state==PlayerManager.PlayerState.MapCreate)
        {
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
        }
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
        
    }
    float EaseOutCirc(float t)
    {
        float x = (t - 1);

        return 1.0f > t ? 1 - Mathf.Sqrt(1 - x * x) : 0.0f;
    }

    void PlayerUpdate()
    {
        rb.constraints = RigidbodyConstraints2D.None;
        cam.orthographicSize= 2;
        onClickingTimeer += Time.deltaTime;
        aerodynamicDragTimeer += Time.deltaTime * aerodynamicDragLevel;
        rb.velocity = velocity * EaseOutCirc(aerodynamicDragTimeer);
       
        if (Input.GetMouseButtonDown(0))
        {
          
            onClickingTimeer = 0.0f;
        }
        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("PlayerUpdate");
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
