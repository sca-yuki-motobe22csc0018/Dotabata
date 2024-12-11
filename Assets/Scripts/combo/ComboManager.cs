using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboManager : MonoBehaviour
{
    private const float comboTime=4.0f;//�R���{���Ȃ�������A�����̍z�΂�4�b�ȓ��ɏՓ�
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
