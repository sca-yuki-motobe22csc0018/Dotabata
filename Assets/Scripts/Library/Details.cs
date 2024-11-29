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
            //名前テキスト
            NameText.text = "アイソレートアジュール";
            //効果テキスト
            EffectText[0].text = "・移動クールタイム減少";
            EffectText[1].text = "・常時チャージ移動";
            EffectText[2].text = "・移動クールタイム増加";
            EffectText[3].text = "・ノックバック増加";
            //説明テキスト
            ExplanationText[0].text = "洞窟の深奥で静かに輝きを放つ神秘の鉱石。";
            ExplanationText[1].text = "澄んだ青色は他の鉱石の追随を許さず、";
            ExplanationText[2].text = "触れた者は運命に祝福されるとか。";
            ExplanationText[3].text = "";
        }
        else if(selectNum == 2)
        {
            //名前テキスト
            NameText.text = "クリムゾンヒム";
            //効果テキスト
            EffectText[0].text = "・溶岩速度低下";
            EffectText[1].text = "・鉱石一撃破壊";
            EffectText[2].text = "・溶岩速度上昇";
            EffectText[3].text = "・所持可能マップ数低下";
            //説明テキスト
            ExplanationText[0].text = "地の鼓動が生んだと言われる情熱の鉱石。";
            ExplanationText[1].text = "その輝きは燃える炎のようで、";
            ExplanationText[2].text = "見た者の逆境を乗り越える力を";
            ExplanationText[3].text = "授けると伝えられている。";
        }
        else if(selectNum == 3)
        {
            //名前テキスト
            NameText.text = "アンバーエッグ";
            //効果テキスト
            EffectText[0].text = "・短期間獲得スコア上昇";
            EffectText[1].text = "・視界範囲上昇";
            EffectText[2].text = "・3回獲得スコア減少";
            EffectText[3].text = "・視界範囲減少";
            //説明テキスト
            ExplanationText[0].text = "命が宿るとされている卵型の鉱石。";
            ExplanationText[1].text = "鉱石の中には微かにゆらめく光が宿り、";
            ExplanationText[2].text = "光が満ち溢れると世界を灯すと";
            ExplanationText[3].text = "伝説に語られている。";
        }
        else if(selectNum == 4)
        {
            //名前テキスト
            NameText.text = "サプリングドラシル";
            //効果テキスト
            EffectText[0].text = "・移動距離アップ";
            EffectText[1].text = "・ノックバック軽減";
            EffectText[2].text = "・暗黙構想ピース化";
            EffectText[3].text = "・ピースドロップ率低下";
            //説明テキスト
            ExplanationText[0].text = "世界樹の恩恵が地脈を通して結晶化したと";
            ExplanationText[1].text = "伝えられる鉱石。";
            ExplanationText[2].text = "その輝きは瑞々しい大樹の葉を思わせ、";
            ExplanationText[3].text = "生命力を分け与えるとされている。";
        }
        else if(selectNum == 5)
        {
            //名前テキスト
            NameText.text = "フラングフレア";
            //効果テキスト
            EffectText[0].text = "・視界範囲上昇";
            EffectText[1].text = "・短期間獲得スコア上昇";
            EffectText[2].text = "・溶岩速度低下";
            EffectText[3].text = "・溶岩速度上昇";
            //説明テキスト
            ExplanationText[0].text = "愛と情熱を象徴する薔薇の鉱石。";
            ExplanationText[1].text = "恋人への贈り物として重宝されているが、";
            ExplanationText[2].text = "扱いを誤るととんでもないことになるとか…？";
            ExplanationText[3].text = "";
        }
        else
        {
            //名前テキスト
            NameText.text = "鉱石名";
            //効果テキスト
            EffectText[0].text = "・効果";
            EffectText[1].text = "・";
            EffectText[2].text = "・";
            EffectText[3].text = "・";
            //説明テキスト
            ExplanationText[0].text = "説明";
            ExplanationText[1].text = "a";
            ExplanationText[2].text = "a";
            ExplanationText[3].text = "a";
        }
    }
}
