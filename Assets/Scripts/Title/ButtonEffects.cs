using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    enum ButtonState
    {
        OFF_CURSOR,
        ON_CURSOR,
    }
    private Image buttonBackGroundImage;
    [SerializeField] private GameObject[] buttonDecoration;
    [SerializeField] private Sprite[] buttonSprites;
    [SerializeField] private GameObject offContents;
    [SerializeField] private GameObject onContents;
    private ButtonMode mode;
    private ButtonState state;

    private Vector3 onCursorScale;
    private Vector3 offCursorScale;
    // Start is called before the first frame update
    void Start()
    {
        switch (gameObject.name)
        {
            case "Start":
                mode = ButtonMode.START;
                break;


            case "Library":
                mode = ButtonMode.LIBRARY;
                break;

            case "Option":
                mode = ButtonMode.OPTION;
                break;

            case "Exit":
                mode = ButtonMode.EXIT;
                break;

        }
        buttonBackGroundImage = transform.GetChild(0).gameObject.GetComponent<Image>();
        buttonBackGroundImage.sprite = buttonSprites[0];
        buttonDecoration[0].SetActive(false);
        buttonDecoration[1].SetActive(false);
        onContents.SetActive(false);
        offContents.SetActive(true);
        state = ButtonState.OFF_CURSOR;

        offCursorScale = Vector3.one;
        onCursorScale = Vector3.one * 1.25f;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == ButtonState.OFF_CURSOR)
        {
            buttonBackGroundImage.sprite = buttonSprites[0];
            buttonDecoration[0].SetActive(false);
            buttonDecoration[1].SetActive(false);
            onContents.SetActive(false);
            offContents.SetActive(true);

            if (transform.localScale.x > offCursorScale.x)
                transform.localScale -= onCursorScale * Time.deltaTime * 5.0f;
            else if(transform.localScale.x < offCursorScale.x)
                transform.localScale = offCursorScale;
        }
        else
        {
            buttonBackGroundImage.sprite = buttonSprites[1];
            buttonDecoration[0].SetActive(true);
            buttonDecoration[1].SetActive(true);
            onContents.SetActive(true);
            offContents.SetActive(false);

            if (transform.localScale.x < onCursorScale.x)
                transform.localScale += onCursorScale * Time.deltaTime * 5.0f;
            else if (transform.localScale.x > onCursorScale.x)
                transform.localScale = onCursorScale;
        }
    }

    public void OnClick()
    {
        string sceneName = null;
        switch (mode)
        {
            case ButtonMode.START:
                sceneName = "MapTest";
                break;

            case ButtonMode.LIBRARY:
                sceneName = "Library";
                break;

                case ButtonMode.OPTION:
                sceneName = "Option";
                break;

                case ButtonMode.EXIT:
                Application.Quit();
                break;
        }

        if(sceneName != null)
            SceneManager.LoadScene(sceneName);
    }

   public void PointerEnter()
    {
        state = ButtonState.ON_CURSOR;
    }

    public void PointerExit()
    {
        state= ButtonState.OFF_CURSOR;
    }
}
