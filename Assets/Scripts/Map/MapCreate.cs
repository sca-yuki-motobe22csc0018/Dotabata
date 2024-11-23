using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;
using UnityEditor.Build.Content;

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
    [SerializeField] TextAsset cameraMapCSV;
    [SerializeField] TextAsset consoleMapCSV;
    [SerializeField] TextAsset doorMapCSV;
    [SerializeField] GameObject[] mapObjects;
    [SerializeField] GameObject[] noColliderMapObjects;
    [SerializeField] GameObject handBackGround;
    [SerializeField] GameObject handWindow;
    [SerializeField] GameObject cameraObject;
    private Camera camera;
    SelectHand selectHand;
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


    private void Awake()
    {
        pieceData = new List<string[]>();
        pieceData = PieceCsvReader(neutralMapCSV);
        selectHand = handBackGround.GetComponent<SelectHand>();
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
        const int pieceColum = 15;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                string str = piece[i+pieceColum*pieceNumber][j][0].ToString();
                int number = int.Parse(str);
                float posX = (transform.position.x + (j + width) * pieceSize)-width-width/2+createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize)+height+height/2+createPos.y;
                GameObject obj = Instantiate(mapObjects[number], new Vector3(posX, 
                    posY,0), Quaternion.identity);
                if (number == 2)
                {
                    Ore ore = obj.GetComponent<Ore>();
                    Ore.OreInfo Info = ore.Info;
                    Info.number = number - 2;
                    ore.Info = Info;

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
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                string str = piece[i + pieceColum * pieceNumber][j][0].ToString();
                int number = int.Parse(str);
                float posX = (transform.position.x + (j + width) * pieceSize) - width - width / 2 + createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize) + height + height / 2 + createPos.y;
                if (necessityCollider)
                {
                    GameObject obj = Instantiate(mapObjects[number], new Vector3(posX,
                    posY, 0), Quaternion.identity);
                    obj.transform.parent = parent.transform;
                    //Instantiate�Őe��ݒ肷��Ɣ䗦���ēx�������Ȃ��Ƃ����Ȃ��Ȃ邽�߂��̕��@�Őe��ݒ�
                    if (number == 2)
                    {
                        Ore ore = obj.GetComponent<Ore>();
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
                }
               
            }
        }
        makeMap = false;
    }


    private void MapCreater()
    {
        camera.orthographicSize= 15;
        mapNumber = selectHand.GetMakeNumber;
        if (makeMap) 
        {
            PieceCreator(pieceData, mapNumber, 1, setPosition);
            makeMap = false;
            StartCoroutine(delay());
        }
        
    }

    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.5f);
        PlayerManager.state = PlayerManager.PlayerState.PlayerMove;
    }

    private void OreDataSet()
    {

    }
}
