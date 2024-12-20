using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// �S��:�F�J
/// </summary>


public class MapCreate : MonoBehaviour
{

    delegate void function();
    const int height = 13;
    const int width = 13;
    [SerializeField] int mapNumber = 0;
    [SerializeField] TextAsset neutralMapCSV;
    [SerializeField] TextAsset respawnCSV;
    List<string[]> respawnPiece;
    [SerializeField] GameObject[] mapObjects;
    [SerializeField] GameObject[] noColliderMapObjects;
    [SerializeField] GameObject handBackGround;
    [SerializeField] GameObject handWindow;
    [SerializeField] GameObject cameraObject;
    [SerializeField] Canvas canvas;
    [SerializeField] GameObject[] walls;
    [SerializeField] GameObject defaultTileManagement;
    private Camera camera;
    SelectHand sh;
    public   List<string[]> pieceData;

    public List<string[]> PieceData { get { return pieceData; } }
    
    private bool makeMap;
    public bool MakeMap { get { return makeMap; } set { makeMap = value; } }
    const int handSize = 8;
    public int HandSize { get { return handSize; } }
    int pieceCount = 0;
    public int PieceCount { get{ return pieceCount; } }
    private Vector3 setPosition;
    public Vector3 SetPosition { set { setPosition = value; } } 
    [SerializeField] GameObject[] handPiecePos;
    public int MapNumber { get { return mapNumber; } set { mapNumber = value; } }

    public List<string[]> ResPawnPiece { get { return respawnPiece; } }

    [SerializeField] GameObject[] ores;

    private void Awake()
    {
        pieceData = new List<string[]>();
        respawnPiece=new List<string[]>();
        respawnPiece=PieceCsvReader(respawnCSV);
        pieceData = PieceCsvReader(neutralMapCSV);
        sh = handBackGround.GetComponent<SelectHand>();
        makeMap = false;
        camera=cameraObject.GetComponent<Camera>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject GM = GameObject.Find("GameManager").gameObject;
        PlayerManager PM=GM.GetComponent<PlayerManager>();
        PM.AddFunction(MapCreater,1);
       // MakeOuterWall();
    }

    private void Update()
    {
        for(int i = 0;i<8;i++)
        {
            handPiecePos[i].SetActive(PlayerManager.state == PlayerManager.PlayerState.MapCreate);
        }
        handWindow.SetActive(PlayerManager.state == PlayerManager.PlayerState.MapCreate);
    }

    /// <summary>
    /// CSV����S�Ẵs�[�X�f�[�^��ǂݎ��֐�
    /// </summary>
    /// <param name="csvData">�ǂݎ�肽��CSV</param>
    /// <returns></returns>
    private List<string[]> PieceCsvReader(TextAsset csvData) //�����ɓ��͂���CSV�t�@�C�������X�g�ɕϊ�����֐�
    {
        List<string[]> mapLineDatas = new List<string[]>();
        StringReader reader = new StringReader(csvData.text);
        while (reader.Peek() != -1)
        {
            string line = reader.ReadLine();
            mapLineDatas.Add(line.Split(','));
            if (line[0] == 'E')
            {
                pieceCount++;
            }
        }
        return mapLineDatas;
    }

    /// <summary>
    /// �s�[�X������̈ʒu�ɐ�������֐�
    /// </summary>
    /// <param name="piece">�S�Ẵs�[�X�̃f�[�^���i�[����Ă���f�[�^(�ǂݎ����CSV)</param>
    /// <param name="pieceNumber">�����������s�[�X�̔ԍ�</param>
    /// <param name="pieceSize">�����������s�[�X�̃T�C�Y</param>
    ///  <param name="createPos">�����������s�[�X�̍��W</param>
    public void PieceCreator(List<string[]> piece,int pieceNumber,float pieceSize,Vector3 createPos) 
    {
        float angle = 0;
        string str = " ";
        string angleS=" ";
        const int pieceColum = 15;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                str = piece[i+pieceColum*pieceNumber][j][0].ToString();
                angleS = piece[i + pieceColum * pieceNumber][j].ToString();
              
                int number = int.Parse(str);
                float posX = (transform.position.x + (j + width) * pieceSize)-width-width/2+createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize)+height+height/2+createPos.y;
                if (number == 2)
                {
                    
                    angleS=angleS.Substring(2,angleS.Length-2);
                    angle=int.Parse(angleS);
                    int random=Random.Range(0, ores.Length);//�z�΂̒����烉���_���ɐ���
                    GameObject obj = Instantiate(ores[random],new Vector3(posX,posY,0),Quaternion.AngleAxis(angle-ores[random].transform.rotation.z, Vector3.forward));
                    GameObject floor = Instantiate(mapObjects[0],new Vector3(posX, posY,0), Quaternion.identity);
                    Ore ore = obj.GetComponent<Ore>();
                    Ore.OreInfo Info = ore.Info;
                    Info.number = number - 2;
                    ore.Info = Info;
                    obj.transform.parent=canvas.transform;

                }
                else
                {
                       GameObject obj = Instantiate(mapObjects[number], new Vector3(posX,
                       posY, 0), Quaternion.identity);
                }
            }
        }
        makeMap = false;
    }

    /// <summary>
    /// �s�[�X������̈ʒu�ɐ�������֐�
    /// </summary>
    /// <param name="piece">�S�Ẵs�[�X�̃f�[�^���i�[����Ă���f�[�^(�ǂݎ����CSV)</param>
    /// <param name="x">��������s�[�X�̉��ʒu</param>
    /// <param name="y">��������s�[�X�̏c�ʒu</param>
    /// <param name="pieceNumber">�����������s�[�X�̔ԍ�</param>
    ///  <param name="createPos">�����������s�[�X�̍��W</param>
    /// <param name="parent">���������I�u�W�F�N�g�̐e</param>
    /// <param name="necessityCollider">collider���K�v���ǂ���True�ŕK�v</param>
    public void PieceCreator(List<string[]> piece, int pieceNumber, float pieceSize,Vector3 createPos, GameObject parent,bool necessityCollider)
    {
        const int pieceColum = 15;
        float angle=0;
        string str=" ";
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int number = 0;
                if (pieceNumber == 100)
                {
                    number = 1;
                }
                else
                {
                    str = piece[i + pieceColum * pieceNumber][j][0].ToString();
                    number = int.Parse(str);
                }
               
                float posX = (transform.position.x + (j + width) * pieceSize) - width - width / 2 + createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize) + height + height / 2 + createPos.y;
              
                if (necessityCollider)
                {
                    int cor = 1;
                    for (int ang = 0; i < str.Length - 2; ang++)
                    {
                        angle = str[str.Length - 1] * cor;
                        cor *= 10;
                    }
                    GameObject obj = Instantiate(mapObjects[number], new Vector3(posX,
                    posY, 0), Quaternion.AngleAxis(120,Vector3.forward));
                    obj.transform.parent = parent.transform;
                    
                    //Instantiate�Őe��ݒ肷��Ɣ䗦���ēx�������Ȃ��Ƃ����Ȃ��Ȃ邽�߂��̕��@�Őe��ݒ�
                    if (number == 2)
                    {
                        Ore ore = obj.GetComponent<Ore>();
                      
                       
                        obj.transform.Rotate(new Vector3(0,0,obj.transform.rotation.z+angle));
                        Ore.OreInfo Info=ore.Info;
                        Info.number = number-2;//�z�΂�CSV�̔ԍ���2~�����A�z�Δԍ���0����ɂ������̂�2������
                        ore.Info = Info;

                    }
                }
                else
                {
                    GameObject obj =Instantiate(noColliderMapObjects[number], new Vector3(posX,
                    posY, 0), Quaternion.identity);
                    obj.transform.parent = parent.transform;
                    if(pieceNumber!=100)
                    {
                        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                        sr.sortingOrder = 20;
                    }
                    
                   
                }
               
            }
        }
        makeMap = false;
    }

    public void FirstPieceCreator(List<string[]> piece, int pieceNumber, float pieceSize, Vector3 createPos, GameObject parent)
    {
        const int pieceColum = 15;
        float angle = 0;
        string str = " ";
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                int number = 0;
                if (pieceNumber == 100)
                {
                    number = 1;
                }
                else
                {
                    str = piece[i + pieceColum * pieceNumber][j][0].ToString();
                    number = int.Parse(str);
                }

                float posX = (transform.position.x + (j + width) * pieceSize) - width - width / 2 + createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize) + height + height / 2 + createPos.y;


                int rand = Random.Range(0, 20);
                if(i==0||i==height-1||j==0||j==width-1)
                {
                    rand = 1;
                }
                GameObject obj;
                
                if(rand == 0)
                {
                   int random=Random.Range(0, 2);
                   obj = Instantiate(walls[random], new Vector3(posX,
                   posY, 0), Quaternion.identity);
                }
                else
                {
                  obj = Instantiate(noColliderMapObjects[number], new Vector3(posX,
                  posY, 0), Quaternion.identity);
                }
                   
                    obj.transform.parent = parent.transform;
                    if (pieceNumber != 100)
                    {
                        SpriteRenderer sr = obj.GetComponent<SpriteRenderer>();
                        sr.sortingOrder = 20;
                    }


                }

            }
        }


    private void MapCreater()
    {
        camera.orthographicSize= 15;
       

        if (makeMap) 
        {
           
            if(sh.HandNumber.Count>0)
            {
                mapNumber = sh.HandNumber[sh.SelectNumber];
                PieceCreator(pieceData, mapNumber, 1, setPosition);
                sh.HandNumber.Remove(mapNumber);
            }
            else
            {
                PieceCreator(pieceData,0,1,setPosition);
            }
           
            Debug.Log(mapNumber);
            makeMap = false;
            StartCoroutine(delay());
        }
        
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        sh.SelectNumber = -1;
    }

}
