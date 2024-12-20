using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magma : MonoBehaviour
{
    [SerializeField,Header("１ブロック/設定秒")] private float maxSpeed;
    [SerializeField] private float defSpeed;
    [SerializeField] private float speed;
    [SerializeField] private string playerName;
    private GameObject player;
    private Vector3 playerPos;
    private const int mapHeight = 13;
    private float oreEffectSpeedRate;
    private float timer;
    private bool isMovable;
    [SerializeField, Header("開始後に指定秒待つ")] private float waitTime;

    private void Start()
    {
        player = GameObject.Find(playerName);
        playerPos = player.transform.position;
        speed = defSpeed;
        oreEffectSpeedRate = 1.0f;
        timer = 0.0f;
        isMovable = false;
        StartCoroutine(WaitAnySeconds());
    }

    // Update is called once per frame
    void Update()
    {
        if (isMovable)
        {
            Vector3 spd = Vector3.zero;
            float deff = Mathf.Abs(transform.position.y - playerPos.y);
            if (deff >= mapHeight)
            {
                speed = defSpeed;
            }
            spd.y = 1.0f / speed * oreEffectSpeedRate <= maxSpeed ?
                1.0f / speed * oreEffectSpeedRate : 0.5f;

            transform.position += spd;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { SceneManager.LoadScene("Title"); }
       
    }

    private IEnumerator WaitAnySeconds()
    {
        yield return new WaitForSeconds(waitTime);
        isMovable = true;
    }
}
