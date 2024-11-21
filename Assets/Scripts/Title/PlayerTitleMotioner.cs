using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerTitleMotioner : MonoBehaviour
{
    private enum MotionState
    {
        FLOATING=0,
        MOVE,
        HOP,
        SLEEP,
        SIZE
    }
    private MotionState motionState;
    private bool isMotionStarted;

    [SerializeField]
    private float floatHeight;
    [SerializeField]
    private float hoppingHeight;

    private GameObject parent;

    private int lastMotionNumber;

    private BackGroundScroller bgs;
    private float fallSpeed;

    Quaternion direntionRight = Quaternion.Euler(new Vector3(0, 180, 0));
    Quaternion directionLeft = Quaternion.Euler(new Vector3(0, 0, 0));

    [SerializeField]
    private GameObject[] syomome;
    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        motionState = MotionState.FLOATING;
        lastMotionNumber = 0;
        isMotionStarted = false;
        bgs = GameObject.Find("BackGroundObject").GetComponent<BackGroundScroller>();
    }

    //Update is called once per frame
    void Update()
    {
        fallSpeed = bgs.ScrollSpeed;
        parent.transform.localPosition =
            parent.transform.localPosition + new Vector3(0, fallSpeed) * Time.deltaTime;

        if (!isMotionStarted)
        {
            NextMotion();
            switch (motionState)
            {
                case MotionState.FLOATING:
                    {
                        float rand = Random.Range(1, 5);
                        StartCoroutine(Motion_Floating(rand));
                    }
                    break;

                case MotionState.MOVE:
                    {
                        float rand = Random.Range(1, 4);
                        StartCoroutine(Motion_Move(rand));
                    }
                    break;

                case MotionState.HOP:
                    {
                        float randTime = Random.Range(0.5f, 1.0f);
                        int randHops = Random.Range(1, 3);
                        StartCoroutine(Motion_Hop(randHops, randTime));
                    }
                    break;

                case MotionState.SLEEP:
                    {
                        float rand = Random.Range(1.0f, 4.0f);
                        StartCoroutine (Motion_Sleep(rand));
                    }
                    break;
            }
        }
    }

    private void NextMotion()
    {
        bool isEnd = false;
        int rand = 0;
        while (!isEnd)
        {
            rand = Random.Range(0, (int)MotionState.SIZE);
            if (rand != lastMotionNumber) isEnd = true;
        }
        motionState = (MotionState)rand;
        lastMotionNumber = rand;
    }

    private IEnumerator Motion_Sleep(float time)
    {
        isMotionStarted = true;

        for(int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            float t = 0.0f;
            bool isEnd = false;
            while (!isEnd)
            {
                if (t >= 1.0f) isEnd = true;

                t += Time.deltaTime;
                parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 25.0f / Mathf.PI) * EaseInOutCirc(t));
                syomome[0].transform.localScale = new Vector3(0,0.5f,0) * EaseInOutCirc(t);
                syomome[1].transform.localScale = new Vector3(0, 0.5f, 0) * EaseInOutCirc(t);
                yield return null;
            }
        }
    }
    private IEnumerator Motion_Floating(float time)
    {
        isMotionStarted = true;

        float t = 0.0f;
        bool isEnd = false;
        while (!isEnd)
        {
            if (t >= time) isEnd = true;

            t += Time.deltaTime;
            float x = transform.position.x;
            float y = parent.transform.position.y + Mathf.Sin(t * Mathf.PI) * floatHeight;
            transform.position = new Vector2(x, y);

            yield return null;
        }
        transform.position = parent.transform.position;
        isMotionStarted=false;
    }
    private IEnumerator Motion_Move(float time)
    {
        isMotionStarted = true;

        float t = 0.0f;
        bool isEnd = false;

        Vector3 defPos = parent.transform.localPosition;
        Vector3 target = new Vector3(
            Random.Range(-800.0f, 200.0f),
            Random.Range(-600.0f, 150.0f));
        Vector3 targetVec = target - parent.transform.localPosition;

        if (targetVec.x < 0) parent.transform.rotation = directionLeft;
        else parent.transform.rotation = direntionRight;

        StartCoroutine(Motion_Floating(time));
        while (!isEnd)
        {
            if (t >= time) isEnd = true;
            t += Time.deltaTime;

            //target.y += fallSpeed * Time.deltaTime;
            parent.transform.localPosition = defPos + targetVec * t / time;

            yield return null;
        }
        parent.transform.localPosition = target;
        isMotionStarted = false;
    }

    private IEnumerator Motion_Hop(int hops,float hoppingTime)
    {
        isMotionStarted = true;

        float t = 0.0f;
        bool isEnd = false;
        while (!isEnd)
        {
            if (t > 0.5f) isEnd = true;

            t += Time.deltaTime * hops / hoppingTime;
            float x = transform.position.x;
            float y = parent.transform.position.y +
                Mathf.Abs(Mathf.Sin(t * 2 * Mathf.PI)) * hoppingHeight;
            transform.position = new Vector2(x, y);

            yield return null;
        }
        isMotionStarted = false;
    }

    private float EaseInOutCirc(float t)
    {
        float c1 = 4.0f * t;
        float c2 = 4.0f * (t - 1.0f);

        return t < 0.5f ? c1 * c1 : c2 * c2;
    }
}
