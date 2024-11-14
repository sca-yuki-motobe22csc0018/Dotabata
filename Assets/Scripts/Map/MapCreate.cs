using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System.Linq;

/// <summary>
/// 熊谷
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
    /// CSVから全てのピースデータを読み取る関数
    /// </summary>
    /// <param name="csvData">読み取りたいCSV</param>
    /// <returns></returns>
    private List<string[]> PieceCsvReader(TextAsset csvData) //引数に入力したCSVファイルをリストに変換する関数
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
    /// ピースを所定の位置に生成する関数
    /// </summary>
    /// <param name="piece">全てのピースのデータが格納されているデータ(読み取ったCSV)</param>
    /// <param name="x">生成するピースの横位置</param>
    /// <param name="y">生成するピースの縦位置</param>
    /// <param name="pieceNumber">生成したいピースの番号</param>
    /// <param name="pieceSize">生成したいピースのサイズ</param>
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
