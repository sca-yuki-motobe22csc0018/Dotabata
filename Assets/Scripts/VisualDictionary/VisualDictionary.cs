using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VisualDictionary : MonoBehaviour
{
    public GameObject pageCanvas;
    public GameObject detailsCanvas;
    public GameObject backButton;
    public GameObject nextButton;
    public GameObject[] oreSelectButton;
    public Image[] oreImage;
    public Sprite[] oreSprite;
    public Text[] oreNameText;
    public Text[] oreNumberText;
    public Text NowPageText;

    const int page_Max = 10;
    const int page_Min = 1;
    int page_Now = 1;

    void Start()
    {
        pageCanvas.SetActive(true);
        detailsCanvas.SetActive(false);

        page_Now = 1;
        pageUpdate();
    }

    void Update()
    {
        
    }

    /// <summary>
    /// 次のページに移動
    /// </summary>
    public void goNextPage()
    {
        page_Now++;
        pageUpdate();
    }
    /// <summary>
    /// 前のページに移動
    /// </summary>
    public void goBackPage()
    {
        page_Now--;
        pageUpdate();
    }

    public void pushOreButton_1()
    {
        detailsCanvas.SetActive(true);
        Details.openDetails((page_Now * 3) - 2);
    }
    public void pushOreButton_2()
    {
        Details.openDetails((page_Now * 3) - 1);
        detailsCanvas.SetActive(true);
    }
    public void pushOreButton_3()
    {
        Details.openDetails(page_Now * 3);
        detailsCanvas.SetActive(true);
    }

    public void pageUpdate()
    {
        NowPageText.text = page_Now + " / " + page_Max;

        if(page_Now == page_Min)
        {
            backButton.SetActive(false);
            nextButton.SetActive(true);

            oreNumberText[0].text = "No." + ((page_Now * 3) - 2).ToString("D3");
            oreNumberText[1].text = "No." + ((page_Now * 3) - 1).ToString("D3");
            oreNumberText[2].text = "No." + (page_Now * 3).ToString("D3");
        }
        else if(page_Now == page_Max)
        {
            backButton.SetActive(true);
            nextButton.SetActive(false);

            oreNumberText[0].text = "No." + ((page_Now * 3) - 2).ToString("D3");
            oreNumberText[1].text = "No." + ((page_Now * 3) - 1).ToString("D3");
            oreNumberText[2].text = "No." + (page_Now * 3).ToString("D3");
        }
        else
        {
            backButton.SetActive(true);
            nextButton.SetActive(true);

            oreNumberText[0].text = "No." + ((page_Now * 3) - 2).ToString("D3");
            oreNumberText[1].text = "No." + ((page_Now * 3) - 1).ToString("D3");
            oreNumberText[2].text = "No." + (page_Now * 3).ToString("D3");
        }
    }
}
