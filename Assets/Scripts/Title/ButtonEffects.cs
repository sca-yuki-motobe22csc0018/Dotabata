using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffects : MonoBehaviour
{
    enum ButtonMode
    {
        START,
        LIBRARY,
        OPTION,
        EXIT
    }

    [SerializeField] private GameObject buttonObject;
    private Image[] buttonImage;
    [SerializeField] private GameObject[] buttonDecoration;
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private Sprite[] buttonEffectSprites;
    [SerializeField] private ButtonMode mode;
    private bool isMotionEnd = false;
    
    // Start is called before the first frame update
    void Start()
    {
        buttonImage[0] = transform.GetChild(0).gameObject.GetComponent<Image>();
        buttonImage[1] = transform.GetChild(1).gameObject.GetComponent<Image>();
        buttonDecoration[0].SetActive(false);
        buttonDecoration[1].SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnterStartButton()
    {
        buttonImage[0].sprite = buttonSprites[1];
        buttonImage[1].sprite = buttonEffectSprites[1];
        buttonDecoration[0].SetActive(true);
        buttonDecoration[1].SetActive(true);

        bool isEnd = false;
        float t = 0.0f;

        while (!isEnd)
        {

        }
        yield return null;
    }
}
