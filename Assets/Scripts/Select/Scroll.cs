using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    [SerializeField]
    private float waitTime;
    [SerializeField]
    private float floatingCenter;
    private float floatingHeight;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(StartFall());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator StartFall()
    {
        bool isEnd = false;
        float t = 0.0f;
        float defPosX = transform.localPosition.x;
        float defPosY = transform.localPosition.y;
        yield return new WaitForSeconds(waitTime);
        while (!isEnd)
        {
            transform.localPosition = new Vector3(defPosX, defPosY + (floatingCenter - defPosY) * EaseOutCubic(t), 0);
            t += Time.deltaTime;
            yield return null;
            if (t >= 1.0f) isEnd = true;
        }
    }

    private float EaseOutCubic(float t)
    {
        float c = 1.0f - t;

        return t <= 1.0f ? 1 - c * c * c : 1.0f;
    }
}
