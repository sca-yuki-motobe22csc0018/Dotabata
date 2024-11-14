using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

/// <summary>
/// �F�J
/// </summary>
public class MapCreate : MonoBehaviour
{
    const int height = 13;
    const int width = 13;
    const int mapHeight = 20;
    const int mapWidth = 10;
    float massSizeY;
    float massSizeX;
    int mapNumber = 0;
    [SerializeField] TextAsset neutralMapCSV;
    [SerializeField] TextAsset cameraMapCSV;
    [SerializeField] TextAsset consoleMapCSV;
    [SerializeField] TextAsset doorMapCSV;
    [SerializeField] GameObject[] mapObjects;
    [SerializeField] GameObject handWindow;
    SelectHand selectHand;
    private List<string[]> pieceData;
    
    private bool makeMap;
    public bool MakeMap { set { makeMap = value; } }
    const int handSize = 8;
    public int HandSize { get { return handSize; } }
    int pieceCount = 0;
    public int PieceCount { get{ return pieceCount; } }
    private Vector3 setPosition;
    public Vector3 SetPosition { set { setPosition = value; } } 
    [SerializeField] GameObject[] handPiecePos;
 


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
        if(makeMap)
        {
            PieceCreator(pieceData, mapNumber, 1);
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
    private void PieceCreator(List<string[]> piece,int pieceNumber,float pieceSize) 
    {
        const int pieceColum = 15;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                string str = piece[i+pieceColum*pieceNumber][j][0].ToString();
                int number = int.Parse(str);
                float posX = (transform.position.x + (j + width) * pieceSize)-width-width/2+setPosition.x;
                float posY = (transform.position.y - (i + height) * pieceSize)+height+height/2+setPosition.y;
                Instantiate(mapObjects[number], new Vector3(posX, 
                    posY,0), Quaternion.identity);
            }
        }
        makeMap = false;
    }
}
