using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class WitchSpeak : MonoBehaviour
{
    private List<string> dendrogram =new List<string>
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
    private List<string> fixedLine = new List<string>
    {
        "���Ȃ��̍D���ȏĂ������҂��Ă���`�B",
        "����`��ƁA�����ċA���Ă��ĂˁB",
        " ���������˂��`�B���`���ƍ̂��ċA���ė��ĂˁB",
        " ����...�ƁB���낻�남���͏Ă���������...�B",
        "...�ӂ����B��h�����B���A�ǂ�Ȋ����H",
        " �z�΂������ς�...�B�A���ė�����p�[�e�B�[�ˁI"
    };

    private List<string> noEntryLine=new List<string>
    {
        "�Ȃ񂾂��������Ƃ���ɂ��Ȃ��H",
        "���v�H",
        "�Y���̓_����B",
        "�E�E�E�E�E�E�B"
    };
    [SerializeField] private GameObject speakWindow;
    const float sizeX=250;
    const float sizeY=25;
    RectTransform rect;

    private string specialLine =  "���`��`���`��`�Ё`���`��`�B���`����`���̂��`���`��`�B" ;
    private bool specialLineFlag;
    private bool speaking;
    private bool canAddDendrogram;

    private int noEntryCount;
    private bool newNoEntry;
    
    public int NoEntryCount { get { return noEntryCount; } set{noEntryCount=value;} }
    public bool NewNoEntry { get { return newNoEntry; } set { newNoEntry=value;} }

    public List<string> Dendrogram { get { return dendrogram ; }}

    public bool SpesialLineFlag {  get { return specialLineFlag; } set {  specialLineFlag = value; } }
    public string SpesialLine {  get { return specialLine; }  }
   
    [SerializeField]int callDendrogramLine;
    [SerializeField]int callFixedLine;
    [SerializeField]int nowLineCount;
    [SerializeField] Text line;
    [SerializeField] List<string> waitLines=new List<string>();
    private int nowLine;
    public string AddString { set { waitLines.Add(value); } }
    public List<string> FixedLine { get { return fixedLine; } }
    public int NowLine { get { return callDendrogramLine; }set { callDendrogramLine = value; } }
    public int CallFixedLineCount { get { return callFixedLine; }set { callFixedLine = value; } }
    public int NowLineCount { get { return nowLineCount; } set { nowLineCount = value; } }
    public bool CanAddDendrogram { get { return canAddDendrogram;} }



    private void Awake()
    {
        noEntryCount=0;
        NewNoEntry=false;
        callDendrogramLine = 0;
        canAddDendrogram=true;
        rect = speakWindow.transform as RectTransform;
        rect.sizeDelta=new Vector3(0,0,0);
    }
    // Start is called before the first frame update
    void Start()
    {
        specialLineFlag = false;
        callDendrogramLine = Random.Range(1, 3);
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
        if(noEntryCount<=noEntryLine.Count)
        {
            if (newNoEntry)
            {
                waitLines.Add(noEntryLine[noEntryCount-1]);
                newNoEntry = false;
            }
            Debug.Log(1);
        }
       
       
    }

    private IEnumerator LineOperator()
    {
        Debug.Log("Debug");
        canAddDendrogram=false;
        speaking = true;
        StartCoroutine(WindowOpen());
        yield return new WaitForSeconds(0.7f);
        float waitTimer = specialLineFlag || waitLines[nowLine] == dendrogram [5] ? 0.2f : 0.1f;
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
        StartCoroutine(WindowClose());
        nowLine++;
        line.text = null;
        yield return new WaitForSeconds(1f);
        speaking = false;
        canAddDendrogram =true;
    }

    private IEnumerator WindowOpen()
    {
        
        rect.sizeDelta=new Vector3(0,0,0);
        float x=30f;
        float y=0f;
    
        const float openSpeedX=1000;
        const float openSpeedY=250;
        while(rect.sizeDelta.y<sizeY|| rect.sizeDelta.x < sizeX)
        {
          
            y+=Time.deltaTime*openSpeedY;
            x += Time.deltaTime * openSpeedX;
            if (y > sizeY)
            {
                y = sizeY;
                
            }
           
            if (x > sizeX)
            {
                x = sizeX;
            }
            rect.sizeDelta = new Vector3(x, y, 0);
            yield return null;
        }
        //while(rect.sizeDelta.x<sizeX)
        //{
        //    x+=Time.deltaTime*openSpeedX;
        //    if(x > sizeX)
        //    {
        //        x=sizeX;
        //    }
        //    rect.sizeDelta=new Vector3(x,y,0);
        //    yield return null;
        //}
    }

    private IEnumerator WindowClose()
    {
        float x = rect.sizeDelta.x;
        float y = rect.sizeDelta.y;

        const float closeSpeedX = 1000;
        const float closeSpeedY = 250;

        while (rect.sizeDelta.y > 0||rect.sizeDelta.x>0)
        {
            y -= Time.deltaTime * closeSpeedY;
            if (y < 0)
            {
                y = 0;
            }
            rect.sizeDelta = new Vector3(x, y, 0);
            x -= Time.deltaTime * closeSpeedX;
            if (x < 0)
            {
                x = 0;
            }
            yield return null;
        }
        //while (rect.sizeDelta.x > 0)
        //{
            
        //    rect.sizeDelta = new Vector3(x, y, 0);
        //    yield return null;
        //}
    }
}