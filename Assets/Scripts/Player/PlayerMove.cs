using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Build.Content;
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
    private Vector3 moveDirection; // 移動方向
    public Image gaugeImage; // ゲージに使用するImageコンポーネント
    [Range(0, 1)] public float fillAmount = 1.0f; // 塗りつぶし率
    bool move;
    [SerializeField] private Color fullHealthColor = Color.white;   // HP最大時の色
    [SerializeField] private Color lowHealthColor = Color.red;     // HP最小時の色
    [SerializeField] private float forceMultiplier = 10f; // 力の大きさを調整
    [SerializeField] private GameObject gameManger;
    private PlayerManager pm;
    [SerializeField] private GameObject camera;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        pm = gameManger.GetComponent<PlayerManager>();
        pm.AddFunction(PlayerUpdate);
        cam=camera.GetComponent<Camera>();
        move = false;
        PowerTimer = 0;
        rb = GetComponent<Rigidbody2D>();
        this.transform.position = StartPosition;
    }

    // Update is called once per frame
    private void Update()
    {
    }
    // 任意でゲージを設定するメソッド
    public void SetGauge(float value)
    {
        fillAmount = Mathf.Clamp01(value); // 0～1にクランプ
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

    private void PlayerUpdate()
    {
        cam.orthographicSize = 3;
        camera.transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
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
            Vector2 dir = (mouseP - transform.position).normalized*speed;

            // Rigidbody2Dに力を加える
            rb.AddForce(dir * PowerTimer, ForceMode2D.Impulse);
        }
        if (move)
        {
            PowerTimer -= PowerCoolTime * Time.deltaTime;
           // gaugeImage.color = lowHealthColor;
        }
        if (PowerTimer <= 0)
        {
            PowerTimer = 0;
            move = false;
            //gaugeImage.color = fullHealthColor;
        }
        SetGauge(PowerTimer / PowerMaxTime);

        // マウスのスクリーン座標を取得
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // 2DなのでZ座標は無視

        // オブジェクトの位置からマウスの位置までのベクトルを計算
        Vector2 direction = mousePosition - transform.position;

        // ベクトルの角度を取得して回転させる
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        //if (!move)
        //{
        //    //PlayerSkin.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        //    if (PlayerSkin.transform.rotation.z > 0.7f || PlayerSkin.transform.rotation.z < -0.7f)
        //    {
        //        PlayerSkin.transform.localScale = new Vector3(-0.5f, -0.5f, 1);
        //    }
        //    else
        //    {
        //        PlayerSkin.transform.localScale = new Vector3(-0.5f, 0.5f, 1);
        //    }
        //}

    }

    private void OnCollisionEnter(Collision collision)
    {
        

    }
}
