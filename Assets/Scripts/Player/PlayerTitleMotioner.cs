using System.Collections;
using UnityEngine;

public class PlayerTitleMotioner : MonoBehaviour
{
    private enum MotionState
    {
        FLOATING=0,
        MOVE,
        HOP,
        SIZE
    }
    private MotionState motionState;
    private bool isMotionStarted;

    [SerializeField]
    private float floatInterval;
    [SerializeField]
    private float floatHeight;

    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        motionState = MotionState.FLOATING;
        isMotionStarted = false;
    }

    // Update is called once per frame
    void Update()
    {
        switch (motionState)
        {
            case MotionState.FLOATING:
                if (!isMotionStarted)
                {

                }
                break;

            case MotionState.HOP:

                break;
        }
    }

    private void NextMotion()
    {
        int rand= Random.Range(0, (int)MotionState.SIZE);
        motionState = (MotionState)rand;
    }

    private IEnumerator Motion_Floating(float time)
    {
        float t = 0.0f;
        bool isEnd = false;
        while (!isEnd)
        {
            if (t > time) isEnd = true;
            t += Time.deltaTime;

            t += Time.deltaTime;
            float x = transform.position.x;
            float y = parent.transform.position.y + Mathf.Sin(t / floatInterval) * floatHeight;
            transform.position = new Vector2(x, y);

            yield return null;
        }
        NextMotion();
        isMotionStarted=false;
    }
    private IEnumerator Motion_Move(float time)
    {
        float t = 0.0f;
        bool isEnd = false;
        Vector3 target=new Vector3(
            Random.Range(-800.0f,200.0f),
            Random.Range(-400.0f,350.0f));
        StartCoroutine(Motion_Floating(time));
        while (!isEnd)
        {
            if (t > time) isEnd = true;
            t += Time.deltaTime;

            transform.localPosition=target*t;

            yield return null;
        }
        NextMotion();
        isMotionStarted = false;
    }

    private IEnumerator Motion_Hop(int hops,float hoppingTime)
    {
        float t = 0.0f;
        bool isEnd = false;
        for (int i = 0; i < hops; i++)
        {
            while (!isEnd)
            {
                if (t > 0.5f) isEnd = true;
                t += Time.deltaTime/hoppingTime;

                t += Time.deltaTime;
                float x = transform.position.x;
                float y = parent.transform.position.y + Mathf.Abs(Mathf.Sin(t / floatInterval)) * floatHeight;
                transform.position = new Vector2(x, y);

                yield return null;
            }
        }
        NextMotion();
        isMotionStarted = false;
    }
}
