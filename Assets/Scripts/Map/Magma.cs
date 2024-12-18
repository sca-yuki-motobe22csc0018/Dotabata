using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Magma : MonoBehaviour
{
    [SerializeField] private float minSpeed;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float defSpeed;
    [SerializeField] private float speed;
    [SerializeField] private string playerName;
    private GameObject player;
    private Vector3 playerPos;
    private const int mapHeight = 13;
    private float oreEffectSpeedRate;
    private float timer;

    private void Start()
    {
        player = GameObject.Find(playerName);
        playerPos = player.transform.position;
        speed = defSpeed;
        oreEffectSpeedRate = 1.0f;
        timer = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 spd = Vector3.zero;
        float deff=Mathf.Abs(transform.position.y - playerPos.y);
        if (deff >= mapHeight)
        {
            speed = minSpeed;
        }
        //spd.y = 1.0f / speed * oreEffectSpeedRate<=maxSpeed?

        if (timer > 30.0f) transform.position += spd;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player")) { SceneManager.LoadScene("Title"); }
       
    }

}
