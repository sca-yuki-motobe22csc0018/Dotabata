using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class ButtonContentsMotion : MonoBehaviour
{
    [SerializeField] private GameObject[] contents;
    private enum ButtonMode
    {
        NONE=0,
        START,
        LIBRARY,
        OPTION,
        EXIT
    }
    [SerializeField] ButtonMode mode;
    private void OnEnable()
    {
        switch (mode)
        {
            case ButtonMode.LIBRARY:
                StartCoroutine(LibraryMotion());
                break;

            case ButtonMode.OPTION:
                StartCoroutine(OptionMotion());
                break;

            case ButtonMode.EXIT:
                StartCoroutine(ExitMotion());
                break;
        }
    }

    private IEnumerator LibraryMotion()
    {
        bool isEnd = false;
        float t = 0.0f;

        Vector3 defPos = new Vector3(-375, -25, 0);
        Vector3 motPos = new Vector3(-390, 0, 0);
        Vector3 addDist = motPos - defPos;
        float motionSpeed = 2.5f;

        Vector3 defRot = Vector3.zero;
        Vector3 motRot = Vector3.one;
        Vector3 addRot = motRot - defRot;
        contents[1].transform.localScale = Vector3.zero;

        while (!isEnd)
        {
            float x = addDist.x * EaseOutCirc(t);
            float y = addDist.y * EaseOutBack(t);
            contents[0].transform.localPosition= defPos+ new Vector3(x,y);

            t+=Time.deltaTime*motionSpeed;
            if (t >= 1.0f) isEnd = true;
            yield return null;
        }

        isEnd = false;
        t=0.0f;
        motionSpeed = 5.0f;

        while (!isEnd)
        {
            float x = addRot.x * EaseInCubic(t);
            float y = addRot.y * EaseOutQuad(t);
            contents[1].transform.localScale = defRot + new Vector3(x, y);

            t += Time.deltaTime * motionSpeed;
            if (t >= 1.0f) isEnd = true;
            yield return null;
        }

    }

    private IEnumerator OptionMotion()
    {
        bool isEnd = false;
        float t = 0.0f;

        Canvas handLayer = contents[2].transform.GetComponent<Canvas>();
        Vector3 defPos = new Vector3(-345, 0, 0);
        Vector3 motPos = new Vector3(-335, 0, 0);
        Vector3 addDist = motPos - defPos;
        const float motionSpeed = 3.0f;

        Vector3 defRot = new Vector3(0, 0, 30.0f);
        Vector3 motRot = new Vector3(0, 0, -5.0f);
        Vector3 addRot = motRot - defRot;

        contents[0].transform.localRotation = Quaternion.Euler(defRot);

        while (!isEnd)
        {
            contents[2].transform.localPosition = defPos + addDist * Mathf.Sin(t *  Mathf.PI);
            t += Time.deltaTime * motionSpeed;
            handLayer.sortingOrder = t <= 0.5f ? 1 : 3;
            if (t >= 1.0f) isEnd = true;
            yield return null;
        }

        isEnd = false;
        t = 0.0f;

        while (!isEnd)
        {
            contents[0].transform.localRotation = Quaternion.Euler(defRot + addRot * EaseInOutCubic(t));
            t += Time.deltaTime;
            if (t >= 1.0f) isEnd = true;
            yield return null;
        }

        while (true)
        {
            contents[0].transform.localRotation = Quaternion.Euler(motRot + Vector3.one * Mathf.Sin(t * Mathf.PI));
            t += Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator ExitMotion()
    {
        bool isEnd = false;
        float t = 0.0f;

        Vector3 defPos = new Vector3(-130, -70.0f, 0);
        Vector3 motPos = new Vector3(-130, -20.0f, 0);
        Vector3 addDist = motPos - defPos;
        Vector3 defRot_Body = new Vector3(0,0,-10.0f);
        Vector3 motRot_Body = Vector3.zero;
        Vector3 addRot_Body = motRot_Body - defRot_Body;

        Vector3 defRot_Hand =Vector3.zero;
        Vector3 motRot_Hand = new Vector3(0.0f, 0.0f, 15.0f);
        Vector3 addRot_Hand = motRot_Hand - defRot_Hand;

        contents[1].transform.localRotation = Quaternion.Euler(defRot_Hand);
        while (!isEnd)
        {
            contents[0].transform.localPosition = defPos + addDist * EaseOutBack(t);
            contents[0].transform.localRotation= Quaternion.Euler(defRot_Body+addRot_Body*EaseOutBack(t));
            if (t >= 1.0f) isEnd = true;
            yield return t <= 0.0f ? new WaitForSeconds(0.25f) : null;
            t += Time.deltaTime * 2;
        }

        isEnd = false;
        t = 0.0f;
        while (!isEnd)
        {
            contents[1].transform.localRotation = Quaternion.Euler(defRot_Hand + addRot_Hand * Mathf.Sin(t * Mathf.PI*3));
            t += Time.deltaTime;
            yield return null;
        }
    }

    /// <summary>
    /// Äˆğ‚ªo‚Ä‚­‚é‚â‚Â
    /// </summary>
    /// <returns></returns>
    private float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        float c2 = c1 + 1;
        float c3 = t - 1;

        return 1 + c2 * c3 * c3 * c3 + c1 * c3 * c3;
    }

    /// <summary>
    /// –‚—‚ªo‚Ä‚­‚é‚â‚Â
    /// </summary>
    /// <returns></returns>
    private float EaseInOutCubic(float t)
    {
        float c = -2 * t + 2;

        return t < 0.5f ? 4 * t * t * t : 1 - (c * c * c) / 2;
    }

    /// <summary>
    /// –{‚Ì‰¡ˆÚ“®
    /// </summary>
    /// <returns></returns>
    private float EaseOutCirc(float t)
    {
        float c = t - 1;
        return t <= 1.0f ? Mathf.Sqrt(1 - c * c) : 1.0f;
    }

    private float EaseOutQuad(float t)
    {
        float c=1-t;
            return 1 - c*c;
    }

    private float EaseInCubic(float t)
    {
        return t*t*t;
    }
}
