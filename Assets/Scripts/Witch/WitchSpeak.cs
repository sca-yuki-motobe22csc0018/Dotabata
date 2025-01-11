using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WitchSpeak : MonoBehaviour
{
    private List<string> str=new List<string>
    {
        " ",
        "�́`���B����񂶂��W�߂Ă�",
        "������`�B���̒��q�ŏW�߂ĂˁB",
        "���̑���ɂ�������Ɠ����ĂˁB",
        "�����A�����`���ƁB�撣���āI",
        "����΂��Ă�...�B�킽���͂˂Ă邩�炟...�B",
        "�ǂ����ɗ��Ƃ��Ă��Ȃ��悤�ɋC��t���ĂˁB", 
        "�݂�Ȃ����Ȃ��̋A���҂��Ă���`�B",
        "���v�����ˁB����A���͖{��ǂ�ł邩���낵���`�B" 
    };
    private string specialLine =  "���`��`���`��`�Ё`���`��`�B���`����`���̂��`���`��`�B" ;
    private bool specialLineFlag;
    private bool speaking;
    public List<string> Strings { get { return str; }}

    public bool SpesialLineFlag {  get { return specialLineFlag; } set {  specialLineFlag = value; } }
    public string SpesialLine {  get { return specialLine; }  }
   
    [SerializeField]int callLine;
    [SerializeField]int nowLineCount;
    [SerializeField] Text line;
    [SerializeField] List<string> waitLines=new List<string>();
    private int nowLine;
    public string AddString { set { waitLines.Add(value); } }
    public int NowLine { get { return callLine; }set { callLine = value; } }
    public int NowLineCount { get { return nowLineCount; } set { nowLineCount = value; } }

    private void Awake()
    {
        callLine = 0;
    }
    // Start is called before the first frame update
    void Start()
    {
        specialLineFlag = false;
        callLine = Random.Range(1, 3);
    }

     

    // Update is called once per frame
    void Update()
    {
        if(waitLines.Count>0)
        {
            if(!speaking&&waitLines.Count>nowLine)
            {
                StartCoroutine(LineOperator());
            }
        }
       
    }

    private IEnumerator LineOperator()
    {
        speaking = true;
        float waitTimer = specialLineFlag || waitLines[nowLine] == str[5] ? 0.2f : 0.1f;
        for (int i = 0; i < waitLines[nowLine].Length; i++)
        {
            yield return new WaitForSeconds(waitTimer);
            line.text += waitLines[nowLine][i];
        }
        yield return new WaitForSeconds(5f);
        if (waitLines[nowLine]==specialLine)
        {
            specialLineFlag = false;
        }
        nowLine++;
        speaking = false;
        line.text = null;
    }

}
