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
    private GameObject[] handParents;

    [SerializeField]
    private GameObject[] hands;

    [SerializeField]
    private GameObject[] wingParents;
    [SerializeField]
    private GameObject[] wings;

    [SerializeField]
    private GameObject[] tail;

    private float flappingTimer;
    private float flappingMagnitude;
    private float flappingSpeed;
    private float sleptTimer;
    [SerializeField]
    private float sleptInterval;
    [SerializeField]
    private float movementHeight;

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
        flappingMagnitude = 0.0f;
        flappingSpeed = 0.0f;
        sleptTimer = 0.0f;
    }

    //Update is called once per frame
    void Update()
    {
        Motion_Flapping(flappingMagnitude,flappingSpeed);
        parent.transform.localPosition =
            parent.transform.localPosition + new Vector3(0, fallSpeed) * Time.deltaTime;
        sleptTimer += Time.deltaTime;

        if (!isMotionStarted)
        {
            NextMotion();
            flappingMagnitude = 1.0f;
            flappingSpeed = 1.0f;
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
                        const float movementTimeMin = 1.0f;
                        const float movementTimeMax = 4.0f;
                        float randTime = Random.Range(movementTimeMin, movementTimeMax);

                        const float xMin = -800.0f;
                        const float xMax = 200.0f;
                        const float yMin = -600.0f;
                        const float yMax = 150.0f;
                        Vector3 randTarget = new Vector3(
                            Random.Range(xMin, xMax),
                            Random.Range(yMin, yMax));
                        StartCoroutine(Motion_Move(randTime,randTarget));
                    }
                    break;

                case MotionState.HOP:
                    {
                        const int hopsMin = 1;
                        const int hopsMax = 4;
                        int randHops = Random.Range(hopsMin, hopsMax);

                        const float hoppingTimeMin = 0.3f;
                        const float hoppingTimeMax = 0.6f;
                        float randTime = Random.Range(hoppingTimeMin, hoppingTimeMax);
                        StartCoroutine(Motion_Hop(randHops, randTime));
                    }
                    break;

                case MotionState.SLEEP:
                    {
                        //�����̂ŉH�΂����͎キ�Ȃ�܂�
                        flappingMagnitude = 0.75f;
                        flappingSpeed = 0.75f;
                        const float sleepTimeMin = 4.0f;
                        const float sleepTImeMax = 9.0f;
                        float rand = Random.Range(sleepTimeMin, sleepTImeMax);
                        StartCoroutine(Motion_Sleep(rand));
                    }
                    break;
            }
        }
    }

    /// <summary>
    /// ���̃��[�V���������肷��֐��B
    /// </summary>
    private void NextMotion()
    {

        if (parent.transform.localPosition.y >= movementHeight)
        {
            //�������K������ゾ�����玟�͕K���ړ����[�V����
            motionState = MotionState.MOVE;
            lastMotionNumber = (int)MotionState.MOVE;
            return;
        }

        bool isEnd = false;
        int rand = 0;
        while (!isEnd)//�z��͈͓̔��̌��ʂɂȂ�܂ŌJ��Ԃ�
        {
            rand = Random.Range(0, (int)MotionState.SIZE);

            //�O��Ɠ������[�V�����̓L�����Z��
            if (rand == lastMotionNumber) continue;
            //���˂郂�[�V�����͕p�x�����炵�����̂�1/3�̊m���ŃL�����Z��
            if (rand == (int)MotionState.HOP && Random.Range(0, 100) >= 33) continue;
            //�Q�郂�[�V�����͑O�񂪕Y�����[�V�����ȊO�ŁA���K��̎��Ԍo�߂��Ă��Ȃ�������L�����Z��
            if (rand == (int)MotionState.SLEEP && (sleptTimer <= sleptInterval || lastMotionNumber != (int)MotionState.FLOATING)) continue;

            //�S�Ă̏����Ɉ���������Ȃ�������I��
            isEnd = true;
        }
        motionState = (MotionState)rand;
        lastMotionNumber = rand;
    }

    /// <summary>
    /// �H�΂���������֐��B
    /// �H�΂��������������Ԃ͂����Ƃ��̊֐����Ăё�����B
    /// </summary>
    /// <param name="speed">�H�΂����̑��x�Ɋ|����l</param>
    /// <param name="magnitude">�H�΂����̕��Ɋ|����l</param>
    private void Motion_Flapping(float magnitude,float speed)
    {
        flappingTimer = 
            flappingTimer > 1.0f ?                   //�^�C�}�[��1.0�Ɣ�r
            flappingTimer -= 1.0f :�@�@�@�@�@�@�@�@�@//1.0���傫��������1.0������
            flappingTimer += Time.deltaTime * speed;�@//1.0��菬����������deltaTime*speed�𑫂�

        //��{�ɂȂ闃�̊p�x������
        List<float> defRotZ = new List<float>
        {
            25.0f,
            -30.0f
        };
        //�H�΂������������̊p�x������
        List<float> motionRotZ = new List<float>
        {
            -15.0f,
            15.0f
        };

        for (int i = 0; i < defRotZ.Count; i++)
        {
            //��{�̊p�x�ɑ΂��čő�łǂꂭ�炢�������̂��v�Z
            float z = motionRotZ[i] - defRotZ[i];
            //�v�Z���ʂɗh���^���̎��Ԏ����|���āA��{�̊p�x�ɑ���
            wings[i].transform.localRotation =
                Quaternion.Euler(new Vector3(0, 0, defRotZ[i]) +
                new Vector3(0, 0, z) * (1.0f - EaseInReturn(flappingTimer)) * magnitude);
        }
    }

    /// <summary>
    /// �Q�郂�[�V���������s����R���[�`���B
    /// </summary>
    /// <param name="time">�������ԁB���[�V�����S�̂̎��Ԃ͂����2.25�b�������b���B</param>
    /// <returns></returns>
    private IEnumerator Motion_Sleep(float time)
    {
        isMotionStarted = true;
        float t = 0.0f;
        bool isEnd = false;
        Vector3 defScale = eyes[0].transform.localScale;

        List<GameObject[]> bodyParts = new List<GameObject[]>
        {
            ears,
            handParents,
            hands,
            wingParents,
            tail
        };
        List<float> motionRotZ = new List<float>
        {
            //���E��
            50.0f,
            -35.0f,
            //���I
            16.0f,
            -30.0f,
            //����
            12.5f,
            -30.0f,
            //�������{
            30.0f,
            -60.0f,
            //������
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

        //���̊p�x�Ɏg���ϐ�
        Vector3 leaning = new Vector3(0, 0, 35.0f);

        //������������Ƃ���
        for (int n = 0; n < 2; n++)
        {
            yield return new WaitForSeconds(0.5f);
            t = 0.0f;
            isEnd = false;

            //���[�V�����̕�������������̂Ɏg���ϐ�
            float weakenRate = 0.25f;

            while (!isEnd)
            {
                if (t >= 1.0f) isEnd = true;

                t += Time.deltaTime;
                transform.localRotation = Quaternion.Euler(leaning * weakenRate * EaseInReturn(t));
                eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInReturn(t)));
                eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y * (1 - EaseInReturn(t)));

                int count = 0;
                for (int i = 0; i < bodyParts.Count; i++)
                {
                    for (int j = 0; j < bodyParts[i].Length; j++)
                    {
                        bodyParts[i][j].transform.localRotation =
                            Quaternion.Euler(new Vector3(0, 0, motionRotZ[count] * weakenRate) * EaseInReturn(t));
                        count++;
                    }
                }
                yield return null;
            }
        }

        yield return new WaitForSeconds(0.5f);

        //�Q��u��
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
            transform.localRotation = Quaternion.Euler(leaning * EaseInExpo(t));

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

        //�Q�Ă��痎���čs�����[�V����
        t = 0.0f;
        isEnd = false;

        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;
            t += Time.deltaTime / time;

            parent.transform.localScale = new Vector3(1.0f + t / 3, 1.0f - t / 2, 1);
            fallSpeed = -bgs.ScrollSpeed * 1.5f * t;
            //magnitude��speed�Ɏg�������l������
            float ft = 0.5f - (0.4f * t);
            flappingMagnitude = ft;
            flappingSpeed = ft;
            yield return null;
        }
        fallSpeed = bgs.ScrollSpeed;

        //��ыN���Ē��˂�Ƃ���
        t = 0.0f;
        isEnd = false;

        //��ыN�������̒Z���W�����v
        StartCoroutine(Motion_Hop(1, 0.2f));
        eyes[0].transform.localScale = new Vector3(defScale.x, defScale.y);
        eyes[1].transform.localScale = new Vector3(defScale.x, defScale.y);
        while (!isEnd)
        {
            if (t >= 1.0f) isEnd = true;
            t += Time.deltaTime / 0.25f;

            transform.localRotation = Quaternion.Euler(leaning * (1.0f - EaseInOutBack(t)));
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

    /// <summary>
    /// 
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
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

    /// <summary>
    /// �ړ��̃��[�V���������s����R���[�`��
    /// </summary>
    /// <param name="time">�ړ��ɂ����鎞��</param>
    /// <returns></returns>
    private IEnumerator Motion_Move(float time,Vector3 movePos)
    {
        isMotionStarted = true;

        float t = 0.0f;
        bool isEnd = false;

        Vector3 defPos = parent.transform.localPosition;
        Vector3 targetVec = movePos - parent.transform.localPosition;

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
        parent.transform.localPosition = movePos;
        isMotionStarted = false;
    }

    /// <summary>
    /// ���˂郂�[�V���������s����R���[�`���B���[�V�����̑S�̎��Ԃ�hops*time+0.1�b�B
    /// </summary>
    /// <param name="hops">���˂��</param>
    /// <param name="time">���̒��˂Ɋ|���鎞��</param>
    /// <returns></returns>
    private IEnumerator Motion_Hop(int hops, float time)
    {
        isMotionStarted = true;

        for(int i = 0; i < hops; i++)
        {
            float t = 0.0f;
            bool isEnd = false;
            Vector3 defScale = parent.transform.localScale;
            Vector3 motionScale = new Vector3(1.25f, 0.75f, 1) - defScale;

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
            motionScale = new Vector3(0.8f, 1.5f, 1) - defScale;

            while (!isEnd)
            {
                if (t > 1.0f) isEnd = true;

                parent.transform.localScale = defScale + motionScale * EaseOutCubic(t);
                t += Time.deltaTime / time;
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

    /// <summary>
    /// ����̃C�[�W���O�֐��B�񎟊֐���܂�Ԃ��������̃O���t�B
    /// </summary>
    /// <param name="t">�O���t�ɑ΂��鎞�Ԏ��B�߂�l��0.5��1�A1.0��0�ɖ߂�B</param>
    /// <returns></returns>
    private float EaseInReturn(float t)
    {
        float constValue1 = 2 * t;
        float constValue2 = 2 * (t - 1.0f);

        return t < 0.5f ?
            constValue1 * constValue1 :
            constValue2 * constValue2;
    }

    /// <summary>
    /// �C�[�W���O�֐��B�Ȃ��炩�ɏ㏸���㔼�ɂ����ċ}���ɏ㏸����O���t�B
    /// </summary>
    /// <param name="t">�O���t�ɑ΂��鎞�Ԏ��B1.0��1���Ԃ��Ă���B</param>
    /// <returns></returns>
    private float EaseInExpo(float t)
    {
        return t == 0 ?
            0 :
            t < 1.0f ?
            Mathf.Pow(2, 10 * t - 10) :
            1.0f;
    }

    /// <summary>
    /// �C�[�W���O�֐��B�}���ɏ㏸���㔼�ɂ����ĂȂ��炩�ɂȂ��Ă����O���t�B
    /// </summary>
    /// <param name="t">�O���t�ɑ΂��鎞�Ԏ��B1.0��1���Ԃ��Ă���B</param>
    /// <returns></returns>
    private float EaseOutCubic(float t)
    {
        float c = 1 - t;
        return t < 1.0 ? 1 - c * c * c : 1.0f;
    }

    /// <summary>
    /// �C�[�W���O�֐��B�㉺�ɔ�яo���r���݂����ȃO���t�B
    /// </summary>
    /// <param name="t">�O���t�ɑ΂��鎞�Ԏ��B1.0��1���Ԃ��Ă���B</param>
    /// <returns></returns>
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
