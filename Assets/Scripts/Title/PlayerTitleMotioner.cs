using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditorInternal;
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
    private GameObject[] eyes;

    [SerializeField]
    private GameObject[] ears;

    [SerializeField]
    private GameObject[] elbows;

    [SerializeField]
    private GameObject[] hands;

    [SerializeField]
    private GameObject[] wings;
    
    [SerializeField]
    private GameObject[] tail;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        motionState = MotionState.FLOATING;
        lastMotionNumber = 0;
        isMotionStarted = false;
        bgs = GameObject.Find("BackGroundObject").GetComponent<BackGroundScroller>();
        fallSpeed = bgs.ScrollSpeed;
    }

    //Update is called once per frame
    void Update()
    {
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
        float t = 0.0f;
        bool isEnd = false;
        Vector3 defScale = eyes[0].transform.localScale;

        for(int i = 0; i < 2; i++)
        {
            yield return new WaitForSeconds(0.5f);
            t = 0.0f;
            isEnd = false;
            while (!isEnd)
            {
                if (t >= 1.0f) isEnd = true;

                t += Time.deltaTime;
                parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 25.0f) * EaseInReturn(t));
                eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y * (1-EaseInReturn(t)));
                eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y * (1-EaseInReturn(t)));
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);
        t = 0.0f;
        isEnd = false;

        List<GameObject[]> bodyParts = new List<GameObject[]>
        {
            ears,
            elbows,
            hands,
            wings,
            tail
        };

        List<Quaternion> defRots = new List<Quaternion>();
        for(int i=0; i < bodyParts.Count; i++)
        {
            for(int j = 0; j < bodyParts[i].Length; j++)
            {
                defRots.Add(bodyParts[i][j].transform.localRotation);
            }
        }

        List<float> motionRot = new List<float>
        {
            50.0f,
            -35.0f,
            16.0f,
            -30.0f,
            12.5f,
            -30.0f,
            45.0f,
            -65.0f,
            -65.0f
        };


        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;

            t += Time.deltaTime;
            parent.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 35.0f) * EaseInExpo(t));

            eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInExpo(t)));
            eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInExpo(t)));

            int count = 0;
            for (int i = 0; i < bodyParts.Count; i++)
            {
                for (int j = 0; j < bodyParts[i].Length; j++)
                {
                    bodyParts[i][j].transform.localRotation =
                        Quaternion.Euler(new Vector3(0, 0, motionRot[count]) * EaseInExpo(t));
                        count++;
                }
            }

            yield return null;
        }

        t = 0.0f;
        isEnd = false;

        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;
            t += Time.deltaTime / time;

            fallSpeed = -50.0f * t;
            yield return null;
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

    private float EaseInReturn(float t)
    {
        float constValue1 = 2 * t;
        float constValue2 = 2 * (t - 1.0f);

        return t < 0.5f ? constValue1 * constValue1 : constValue2 * constValue2;
    }
    private float EaseInExpo(float t)
    {
        return t == 0 ? 0 : t < 1.0f ? Mathf.Pow(2, 10 * t - 10) : 1;
    }

}
