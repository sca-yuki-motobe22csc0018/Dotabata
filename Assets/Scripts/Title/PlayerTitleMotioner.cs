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
        FLOATING = 0,
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

    private float flappingTimer;
    private float flappingRate;
    private float sleptTimer;

    // Start is called before the first frame update
    void Start()
    {
        parent = transform.parent.gameObject;
        motionState = MotionState.FLOATING;
        lastMotionNumber = -1;
        isMotionStarted = false;
        bgs = GameObject.Find("BackGroundObject").GetComponent<BackGroundScroller>();
        fallSpeed = bgs.ScrollSpeed;
        flappingTimer = 0.0f;
        flappingRate = 0.0f;
        sleptTimer = 0.0f;
    }

    //Update is called once per frame
    void Update()
    {
        Motion_Flapping(flappingRate);
        parent.transform.localPosition =
            parent.transform.localPosition + new Vector3(0, fallSpeed) * Time.deltaTime;
        sleptTimer += Time.deltaTime;

        if (!isMotionStarted)
        {
            NextMotion();
            flappingRate = 1.0f;
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
                        float randTime = Random.Range(0.3f, 0.75f);
                        int randHops = Random.Range(1, 3);
                        StartCoroutine(Motion_Hop(randHops, randTime));
                    }
                    break;

                case MotionState.SLEEP:
                    {
                        flappingRate = 0.75f;
                        float rand = Random.Range(3.0f, 9.0f);
                        StartCoroutine(Motion_Sleep(rand));
                    }
                    break;
            }
        }
    }

    private void NextMotion()
    {
        if (parent.transform.localPosition.y >= 300.0f)
        {
            motionState = MotionState.MOVE;
            lastMotionNumber = (int)MotionState.MOVE;
            return;
        }

        bool isEnd = false;
        int rand = 0;
        while (!isEnd)
        {
            rand = Random.Range(0, (int)MotionState.SIZE);
            if (rand == lastMotionNumber) continue;
            if (rand == (int)MotionState.SLEEP && sleptTimer <= 30.0f && lastMotionNumber != (int)MotionState.FLOATING) continue;
            if (rand == (int)MotionState.HOP && Random.Range(0, 100) >= 33) continue;
            isEnd = true;
        }
        motionState = (MotionState)rand;
        lastMotionNumber = rand;
    }

    private void Motion_Flapping(float rate)
    {
        flappingTimer = flappingTimer > 1.0f ? flappingTimer -= 1.0f : flappingTimer += Time.deltaTime * flappingRate;

        List<float> defRotZ = new List<float>
        {
            25.0f,
            -30.0f
        };
        List<float> motionRotZ = new List<float>
        {
            -15.0f,
            15.0f
        };

        for(int i=0;i<defRotZ.Count;i++)
        {
            float z = motionRotZ[i] - defRotZ[i];
            wings[i].transform.GetChild(0).localRotation =
                Quaternion.Euler(new Vector3(0, 0, defRotZ[i]) + new Vector3(0, 0, z) * (1.0f - EaseInReturn(flappingTimer)) * flappingRate);
        }
    }

    private IEnumerator Motion_Sleep(float time)
    {
        isMotionStarted = true;
        float t = 0.0f;
        bool isEnd = false;
        Vector3 defScale = eyes[0].transform.localScale;

        List<GameObject[]> bodyParts = new List<GameObject[]>
        {
            ears,
            elbows,
            hands,
            wings,
            tail
        };
        List<float> motionRotZ = new List<float>
        {
            //ç∂âEé®
            50.0f,
            -35.0f,
            //óºïI
            16.0f,
            -30.0f,
            //óºéË
            12.5f,
            -30.0f,
            //óºóÉç™ñ{
            30.0f,
            -60.0f,
            //ÇµÇ¡Ç€
            -65.0f
        };

        List<float> defRotZ = new List<float>();
        for (int i = 0; i < bodyParts.Count; i++)
        {
            for (int j = 0; j < bodyParts[i].Length; j++)
            {
                defRotZ.Add(bodyParts[i][j].transform.localRotation.z);
            }
        }

        //Ç±Ç≠Ç±Ç≠Ç∑ÇÈÇ∆Ç±ÇÎ
        for (int n = 0; n < 2; n++)
        {
            yield return new WaitForSeconds(0.5f);
            t = 0.0f;
            isEnd = false;


            while (!isEnd)
            {
                if (t >= 1.0f) isEnd = true;

                t += Time.deltaTime;
                transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 10.0f) * EaseInReturn(t));
                eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInReturn(t)));
                eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInReturn(t)));

                int count = 0;
                for (int i = 0; i < bodyParts.Count; i++)
                {
                    for (int j = 0; j < bodyParts[i].Length; j++)
                    {
                        bodyParts[i][j].transform.localRotation =
                            Quaternion.Euler(new Vector3(0, 0, motionRotZ[count] * 0.25f) * EaseInReturn(t));
                        count++;
                    }
                }
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);

        //êQÇÈèuä‘
        t = 0.0f;
        isEnd = false;

        List<Quaternion> defRots = new List<Quaternion>();
        for (int i = 0; i < bodyParts.Count; i++)
        {
            for (int j = 0; j < bodyParts[i].Length; j++)
            {
                defRots.Add(bodyParts[i][j].transform.localRotation);
            }
        }

        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;

            t += Time.deltaTime;
            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 35.0f) * EaseInExpo(t));

            eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInExpo(t)));
            eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInExpo(t)));

            int count = 0;
            for (int i = 0; i < bodyParts.Count; i++)
            {
                for (int j = 0; j < bodyParts[i].Length; j++)
                {
                    bodyParts[i][j].transform.localRotation =
                        Quaternion.Euler(new Vector3(0, 0, motionRotZ[count]) * EaseInExpo(t));
                    count++;
                }
            }

            yield return null;
        }

        //êQÇƒÇ©ÇÁóéÇøÇƒçsÇ≠Ç∆Ç±ÇÎ
        t = 0.0f;
        isEnd = false;

        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;
            t += Time.deltaTime / time;

            parent.transform.localScale = new Vector3(1.0f + t / 3, 1.0f - t / 2, 1);
            fallSpeed = -150.0f * t;
            flappingRate = 0.5f - (0.4f * t);
            yield return null;
        }
        fallSpeed = bgs.ScrollSpeed;

        //îÚÇ—ãNÇ´ÇƒíµÇÀÇÈÇ∆Ç±ÇÎ
        t = 0.0f;
        isEnd = false;

        StartCoroutine(Motion_Hop(1, 0.2f));
        eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y);
        eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y);
        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;
            t += Time.deltaTime / 0.25f;

            transform.localRotation = Quaternion.Euler(new Vector3(0, 0, 35.0f) * (1.0f - EaseInOutBack(t)));
            int count = 0;
            for (int i = 0; i < bodyParts.Count; i++)
            {
                for (int j = 0; j < bodyParts[i].Length; j++)
                {
                    bodyParts[i][j].transform.localRotation =
                        Quaternion.Euler(new Vector3(0, 0, defRotZ[count] * 0.5f) * EaseInOutBack(t));
                    count++;
                }
            }
            yield return null;
        }
        yield return new WaitForSeconds(0.5f);
        sleptTimer = 0.0f;
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
        isMotionStarted = false;
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

    private IEnumerator Motion_Hop(int hops, float hoppingTime)
    {
        isMotionStarted = true;

        for(int i = 0; i < hops; i++)
        {
            float t = 0.0f;
            bool isEnd = false;
            Vector3 defScale = parent.transform.localScale;
            Vector3 motionScale = new Vector3(1.75f, 0.25f, 1) - defScale;

            while (!isEnd)
            {
                if (t > 1.0f) isEnd = true;

                t += Time.deltaTime / 0.05f;
                parent.transform.localScale = defScale + motionScale * t;

                yield return null;
            }

            t = 0.0f;
            isEnd = false;
            defScale = parent.transform.localScale;
            motionScale = new Vector3(0.5f, 1.5f, 1) - defScale;

            while (!isEnd)
            {
                if (t > 1.0f) isEnd = true;

                parent.transform.localScale = defScale + motionScale * EaseOutCubic(t);
                t += Time.deltaTime * hops / hoppingTime;
                float x = transform.position.x;
                float y = parent.transform.position.y +
                    Mathf.Abs(Mathf.Sin(t * Mathf.PI)) * hoppingHeight;
                transform.position = new Vector2(x, y);

                yield return null;
            }

            t = 0.0f;
            isEnd = false;
            defScale = parent.transform.localScale;
            motionScale = new Vector3(1, 1, 1) - defScale;

            while (!isEnd)
            {
                if (t > 1.0f) isEnd = true;

                t += Time.deltaTime / 0.05f;
                parent.transform.localScale = defScale + motionScale * t;

                yield return null;
            }
            parent.transform.localScale = defScale + motionScale;
        }
        isMotionStarted = false;
    }

    private float EaseInReturn(float t)
    {
        float constValue1 = 2 * t;
        float constValue2 = 2 * (t - 1.0f);

        return t < 0.5f ?
            constValue1 * constValue1 :
            constValue2 * constValue2;
    }
    private float EaseInExpo(float t)
    {
        return t == 0 ?
            0 :
            t < 1.0f ?
            Mathf.Pow(2, 10 * t - 10) :
            1.0f;
    }

    private float EaseOutCubic(float t)
    {
        float c = 1 - t;
        return 1 - c * c * c;
    }
    private float EaseInOutBack(float t)
    {
        const float c1 = 1.70158f;
        const float c2 = c1 * 1.525f;

        float x1 = 2 * t;
        float x2 = 2 * t - 2;

        return t < 0.5f ?
            (x1 * x1 * ((c2 + 1) * 2 * t - c2)) / 2 :
            t < 1.0f ?
            (x2 * x2 * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2 :
            1.0f;
    }
}
