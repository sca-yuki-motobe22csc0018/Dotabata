using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

/// <summary>
/// �S��:�F�J
/// </summary>
public class MapCreate : MonoBehaviour
{
    const int height = 13;
    const int width = 13;
    [SerializeField] int mapNumber = 0;
    [SerializeField] TextAsset neutralMapCSV;
    [SerializeField] TextAsset cameraMapCSV;
    [SerializeField] TextAsset consoleMapCSV;
    [SerializeField] TextAsset doorMapCSV;
    [SerializeField] GameObject[] mapObjects;
    [SerializeField] GameObject[] noColliderMapObjects;
    [SerializeField] GameObject handWindow;
    SelectHand selectHand;
    private List<string[]> pieceData;

    public List<string[]> PieceData { get { return pieceData; } }
    
    private bool makeMap;
    public bool MakeMap { set { makeMap = value; } }
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
        
    }

    // Start is called before the first frame update
    void Start()
    {
        selectHand=handWindow.GetComponent<SelectHand>();
        makeMap = false;
        pieceData = new List<string[]>();
        pieceData = PieceCsvReader(neutralMapCSV);

       // MakeOuterWall();
    }

    private void Update()
    {
        mapNumber = selectHand.GetMakeNumber;
        if(makeMap)
        {
            PieceCreator(pieceData, mapNumber, 1,setPosition);
        }
       
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
    /// <param name="x">��������s�[�X�̉��ʒu</param>
    /// <param name="y">��������s�[�X�̏c�ʒu</param>
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
                Debug.Log(str);
                int number = int.Parse(str);
                float posX = (transform.position.x + (j + width) * pieceSize)-width-width/2+createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize)+height+height/2+createPos.y;
                Instantiate(mapObjects[number], new Vector3(posX, 
                    posY,0), Quaternion.identity);
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
                Debug.Log(str);
                int number = int.Parse(str);
                float posX = (transform.position.x + (j + width) * pieceSize) - width - width / 2 +createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize) + height + height / 2 + createPos.y;
                if (necessityCollider)
                {
                    GameObject obj = Instantiate(mapObjects[number], new Vector3(posX,
                    posY, 0), Quaternion.identity);
                    obj.transform.parent = parent.transform;
                    //Instantiate�Őe��ݒ肷��Ɣ䗦���ēx�������Ȃ��Ƃ����Ȃ��Ȃ邽�߂��̕��@�Őe��ݒ�
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
}
