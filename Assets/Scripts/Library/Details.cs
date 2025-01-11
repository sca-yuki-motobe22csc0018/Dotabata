using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Details : MonoBehaviour
{
    public GameObject DetailsCanvas;
    public static int selectNum;
    int oreMaxNum = 5;

    [SerializeField] Image detailImage;
    [SerializeField] Sprite[] detailSprites;
    [SerializeField] Sprite lockDetailSprites;

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
        selectNum = _selectNum;
    }

    /// <summary>
    /// �ڍׂ��X�V
    /// </summary>
    public void closeUpdate()
    {
        if (OreSave.Load(selectNum))
        {
            detailImage.sprite = detailSprites[selectNum - 1];
        }
        else
        {
            detailImage.sprite = lockDetailSprites;
        }

    }

    /// <summary>
    /// ���{�^�����������̏���
    /// </summary>
    public void leftButton()
    {
        if(selectNum == 1)
        {
            selectNum = oreMaxNum;
        }
        else
        {
            selectNum--;
        }
    }

    /// <summary>
    /// �E�{�^�����������̏���
    /// </summary>
    public void rightButton()
    {
        if (selectNum == oreMaxNum)
        {
            selectNum = 1;
        }
        else
        {
            selectNum++;
        }
    }
}
