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

    [SerializeField] GameObject tagObj;

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

        if (OreSave.Load(selectNum))
        {
            tagObj.SetActive(true);
        }
        else
        {
            tagObj.SetActive(false);
        }

        if(selectNum == 1)
        {
            //���O�e�L�X�g
            NameText.text = "�A�C�\���[�g�A�W���[��";
            //���ʃe�L�X�g
            EffectText[0].text = "�E�ړ��N�[���^�C������";
            EffectText[1].text = "�E�펞�`���[�W�ړ�";
            EffectText[2].text = "�E�ړ��N�[���^�C������";
            EffectText[3].text = "�E�m�b�N�o�b�N����";
            //�����e�L�X�g
            ExplanationText[0].text = "���A�̐[���ŐÂ��ɋP������_��̍z�΁B";
            ExplanationText[1].text = "���񂾐F�͑��̍z�΂̒ǐ����������A";
            ExplanationText[2].text = "�G�ꂽ�҂͉^���ɏj�������Ƃ��B";
            ExplanationText[3].text = "";
        }
        else if(selectNum == 2)
        {
            //���O�e�L�X�g
            NameText.text = "�N�����]���q��";
            //���ʃe�L�X�g
            EffectText[0].text = "�E�n�⑬�x�ቺ";
            EffectText[1].text = "�E�z�Έꌂ�j��";
            EffectText[2].text = "�E�n�⑬�x�㏸";
            EffectText[3].text = "�E�����\�}�b�v���ቺ";
            //�����e�L�X�g
            ExplanationText[0].text = "�n�̌ۓ������񂾂ƌ������M�̍z�΁B";
            ExplanationText[1].text = "���̋P���͔R���鉊�̂悤�ŁA";
            ExplanationText[2].text = "�����҂̋t�������z����͂�";
            ExplanationText[3].text = "������Ɠ`�����Ă���B";
        }
        else if(selectNum == 3)
        {
            //���O�e�L�X�g
            NameText.text = "�A���o�[�G�b�O";
            //���ʃe�L�X�g
            EffectText[0].text = "�E�Z���Ԋl���X�R�A�㏸";
            EffectText[1].text = "�E���E�͈͏㏸";
            EffectText[2].text = "�E3��l���X�R�A����";
            EffectText[3].text = "�E���E�͈͌���";
            //�����e�L�X�g
            ExplanationText[0].text = "�����h��Ƃ���Ă��闑�^�̍z�΁B";
            ExplanationText[1].text = "�z�΂̒��ɂ͔����ɂ��߂������h��A";
            ExplanationText[2].text = "������������Ɛ��E�𓔂���";
            ExplanationText[3].text = "�`���Ɍ���Ă���B";
        }
        else if(selectNum == 4)
        {
            //���O�e�L�X�g
            NameText.text = "�T�v�����O�h���V��";
            //���ʃe�L�X�g
            EffectText[0].text = "�E�ړ������A�b�v";
            EffectText[1].text = "�E�m�b�N�o�b�N�y��";
            EffectText[2].text = "�E�Öٍ\�z�s�[�X��";
            EffectText[3].text = "�E�s�[�X�h���b�v���ቺ";
            //�����e�L�X�g
            ExplanationText[0].text = "���E���̉��b���n����ʂ��Č�����������";
            ExplanationText[1].text = "�`������z�΁B";
            ExplanationText[2].text = "���̋P���͐��X��������̗t���v�킹�A";
            ExplanationText[3].text = "�����͂𕪂��^����Ƃ���Ă���B";
        }
        else if(selectNum == 5)
        {
            //���O�e�L�X�g
            NameText.text = "�t�����O�t���A";
            //���ʃe�L�X�g
            EffectText[0].text = "�E���E�͈͏㏸";
            EffectText[1].text = "�E�Z���Ԋl���X�R�A�㏸";
            EffectText[2].text = "�E�n�⑬�x�ቺ";
            EffectText[3].text = "�E�n�⑬�x�㏸";
            //�����e�L�X�g
            ExplanationText[0].text = "���Ə�M���ے������K�N�̍z�΁B";
            ExplanationText[1].text = "���l�ւ̑��蕨�Ƃ��ďd�󂳂�Ă��邪�A";
            ExplanationText[2].text = "���������ƂƂ�ł��Ȃ����ƂɂȂ�Ƃ��c�H";
            ExplanationText[3].text = "";
        }
        else
        {
            //���O�e�L�X�g
            NameText.text = "�z�Ζ�";
            //���ʃe�L�X�g
            EffectText[0].text = "�E����";
            EffectText[1].text = "�E";
            EffectText[2].text = "�E";
            EffectText[3].text = "�E";
            //�����e�L�X�g
            ExplanationText[0].text = "����";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
    }
}
