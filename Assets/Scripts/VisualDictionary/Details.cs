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
    /// 詳細を閉じる
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
    /// 詳細を更新
    /// </summary>
    public void closeUpdate()
    {
        //鉱石番号テキスト
        NumberText.text = "No." + (selectNum).ToString("D3");

        if(selectNum == 1)
        {
            //名前テキスト
            NameText.text = "アメジスト";
            //効果テキスト
            EffectText[0].text = "・aaaa";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 2)
        {
            //名前テキスト
            NameText.text = "ルビー";
            //効果テキスト
            EffectText[0].text = "・iiii";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 3)
        {
            //名前テキスト
            NameText.text = "サファイア";
            //効果テキスト
            EffectText[0].text = "・iiii";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 4)
        {
            //名前テキスト
            NameText.text = "トパーズ";
            //効果テキスト
            EffectText[0].text = "・iiii";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else if(selectNum == 5)
        {
            //名前テキスト
            NameText.text = "黒曜石";
            //効果テキスト
            EffectText[0].text = "・iiii";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
        else
        {
            //名前テキスト
            NameText.text = "鉱石名";
            //効果テキスト
            EffectText[0].text = "・iiii";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "a";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
    }
}
