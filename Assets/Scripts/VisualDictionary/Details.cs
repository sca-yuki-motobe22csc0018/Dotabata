using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Details : MonoBehaviour
{
    public GameObject DetailsCanvas;
    public static int selectNum;

    [SerializeField] Text NumberText;
    [SerializeField] Text NameText;
    [SerializeField] Text[] EffectText;
    [SerializeField] Text[] ExplanationText;

    void Start()
    {
    }

    void Update()
    {
    }

    /// <summary>
    /// �ڍׂ����
    /// </summary>
    public void closeDetails()
    {
        DetailsCanvas.SetActive(false);
    }

    public static void openDetails(int _selectNum)
    {
        //numberText.text = "No." + (_selectNum).ToString("D3");
        selectNum = _selectNum;
    }

    /// <summary>
    /// �ڍׂ��X�V
    /// </summary>
    public void closeUpdate()
    {
        //�z�Δԍ��e�L�X�g
        NumberText.text = "No." + (selectNum).ToString("D3");

        if(selectNum == 1)
        {
            //���O�e�L�X�g
            NameText.text = "�A���W�X�g";
            //���ʃe�L�X�g
            EffectText[0].text = "�Eaaaa";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 2)
        {
            //���O�e�L�X�g
            NameText.text = "���r�[";
            //���ʃe�L�X�g
            EffectText[0].text = "�Eiiii";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 3)
        {
            //���O�e�L�X�g
            NameText.text = "�T�t�@�C�A";
            //���ʃe�L�X�g
            EffectText[0].text = "�Eiiii";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 4)
        {
            //���O�e�L�X�g
            NameText.text = "�g�p�[�Y";
            //���ʃe�L�X�g
            EffectText[0].text = "�Eiiii";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 5)
        {
            //���O�e�L�X�g
            NameText.text = "���j��";
            //���ʃe�L�X�g
            EffectText[0].text = "�Eiiii";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else
        {
            //���O�e�L�X�g
            NameText.text = "�z�Ζ�";
            //���ʃe�L�X�g
            EffectText[0].text = "�Eiiii";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
    }
}
