using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 担当:熊谷
/// </summary>

public class ComboManager : MonoBehaviour
{
    private const float comboTime=4.0f;//コンボがつながる条件、次ぎの鉱石に4秒以内に衝突
    [SerializeField]private float comboTimer;
    [SerializeField] private int comboCount;
    [SerializeField] private bool comboFlag;

    public float ComboTimer {  get { return comboTimer; } set {  comboTimer = value; } }
    public int ComboCount { get {  return comboCount; } set {  comboCount = value; } } 
    public bool ComboFlag { get { return comboFlag; } set {comboFlag=value; } }


    private void Start()
    {
        comboTimer=0.0f;
        comboCount=0;
        comboFlag=false;
    }

    private void Update()
    {
        Combo();
    }

    private void Combo()
    {
        if(comboFlag)
        {
            comboTimer+=Time.deltaTime;
        }
        if(comboTimer > comboTime)
        {
            comboTimer=0.0f;
            comboCount=0;
            comboFlag=false;
        }
    }
}
