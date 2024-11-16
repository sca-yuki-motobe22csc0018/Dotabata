using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class CircularGauge : MonoBehaviour
{
    public Image gaugeImage; // �Q�[�W�Ɏg�p����Image�R���|�[�l���g
    [Range(0, 1)] public float fillAmount = 1.0f; // �h��Ԃ���

    void Update()
    {
        if (gaugeImage != null)
        {
            gaugeImage.fillAmount = fillAmount;
        }
    }

    // �C�ӂŃQ�[�W��ݒ肷�郁�\�b�h
    public void SetGauge(float value)
    {
        fillAmount = Mathf.Clamp01(value); // 0�`1�ɃN�����v
    }
}
