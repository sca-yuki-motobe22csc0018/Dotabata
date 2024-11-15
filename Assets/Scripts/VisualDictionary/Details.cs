using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Details : MonoBehaviour
{
    public GameObject DetailsCanvas;
    [SerializeField] public static Text numberText;
    [SerializeField] Text NumberText;
    void Start()
    {
        numberText = NumberText;
    }

    void Update()
    {
    }

    /// <summary>
    /// Ú×‚ğ•Â‚¶‚é
    /// </summary>
    public void closeDetails()
    {
        DetailsCanvas.SetActive(false);
    }

    public static void openDetails(int _selectNum)
    {
        numberText.text = "No." + (_selectNum).ToString("D3");
    }
}
