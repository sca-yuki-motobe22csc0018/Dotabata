using System.Collections;
using System.Collections.Generic;
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
            case ButtonMode.START:
                //StartCoroutine(OptionMotion());
                break;

            case ButtonMode.OPTION:
                StartCoroutine(OptionMotion());
                break;

            case ButtonMode.EXIT:
                StartCoroutine(ExitMotion());
                break;
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

    private float EaseOutBack(float t)
    {
        const float c1 = 1.70158f;
        float c2 = c1 + 1;
        float c3 = t - 1;

        return 1 + c2 * c3 * c3 * c3 + c1 * c3 * c3;
    }

    //‚¿‚å‚¤‚Ç‚¢‚¢
    private float EaseInOutCubic(float t)
    {
        float c = -2 * t + 2;

        return t < 0.5f ? 4 * t * t * t : 1 - (c * c * c) / 2;
    }
}
