using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class PlayerController : MonoBehaviour
{
    //Rigidbody
    private Rigidbody2D rb;

    //位置関係
    public Vector3 StartPosition;
    public float EndPositionY;
    //デバック用
    public float StartPositionY;

    //ジャンプ関連
    public float JumpForce;
    public int MaxJumpCount;
    private int thisJumpCount;
    private bool onWall;
    public string StageTag;
    public string WallTag;
    private bool Jump;
    private float JumpCoolTime = 0.1f;
    private float JumpCoolTimer;


    //敵との判定等
    public string EnemyTag;
    private bool DamageTrigger;
    public SpriteRenderer DamageEffectPlayer;
    public SpriteRenderer DamageEffect;
    public float DamageTime;
    public float DamageColor;

    //HP関連
    private int thisHP;
    public int MAXHP;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.position = StartPosition;
        onWall = false;
        rb = GetComponent<Rigidbody2D>();
        Jump = false;
        JumpCoolTimer = 0;
        DamageTrigger = true;
        thisHP = MAXHP;
    }

    // Update is called once per frame
    void Update()
    {
        if (Jump)
        {
            JumpCoolTimer += Time.deltaTime;
            if (JumpCoolTimer > JumpCoolTime)
            {
                JumpCoolTimer = 0;
                Jump = false;
                DamageTrigger = true;
            }
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpAction();
        }
        if (this.transform.position.y < EndPositionY)
        {
            this.transform.position += new Vector3(0, StartPositionY, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(StageTag) && !onWall)
        {
            thisJumpCount = 0;
            Jump = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(WallTag))
        {
            onWall = true;
        }
        if (collision.gameObject.CompareTag(EnemyTag))
        {
            if (DamageTrigger)
            {
                Damage();
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(StageTag) && !onWall)
        {
            thisJumpCount = 0;
        }
        if (collision.gameObject.CompareTag(WallTag))
        {
            onWall = false;
        }
    }
    public void JumpAction()
    {
        if (thisJumpCount < MaxJumpCount)
        {
            rb.velocity = new Vector3(0, JumpForce, 0);
            thisJumpCount++;
            Jump = true;
        }
    }

    void Damage()
    {
        thisHP--;
        var sequence = DOTween.Sequence();
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffect.DOFade(DamageColor, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(0, DamageTime));
        sequence.Append(DamageEffect.DOFade(0, DamageTime));
        sequence.Join(DamageEffectPlayer.DOFade(DamageColor, DamageTime));
        sequence.Append(DamageEffectPlayer.DOFade(0, DamageTime));
    }
}