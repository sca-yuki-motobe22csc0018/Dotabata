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
    const int height = 9;
    const int width = 9;
    const int mapHeight =4;
    const int mapWidth = 4;
    float massSizeY;
    float massSizeX;
    int mapNumber = 0;
    [SerializeField] TextAsset neutralMapCSV;
    [SerializeField] TextAsset cameraMapCSV;
    [SerializeField] TextAsset consoleMapCSV;
    [SerializeField] TextAsset doorMapCSV;
    [SerializeField] GameObject[] mapObjects;
    List<int> pieceNumbers = new List<int>();
   [SerializeField] int pieceCounter = 0;



    // Start is called before the first frame update
    void Start()
    {
        massSizeY = mapObjects[0].transform.localScale.y;
        massSizeX = mapObjects[0].transform.localScale.x;
        List<string[]> piece = new List<string[]>();
        piece = PieceCsvReader(neutralMapCSV);
        for(int i=0;i<pieceCounter;i++)
        {
            pieceNumbers.Add(i);
        }
        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                int pieceNumber=Random.Range(0, pieceNumbers.Count);
                PieceCreater(piece, j, i,pieceNumbers[pieceNumber]);
                pieceNumbers.RemoveAt(pieceNumber);
            }
        }
        MakeOuterWall();
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
            if (line[0]=='E')
            {
                pieceCounter++;
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
    private void PieceCreater(List<string[]> piece,int x, int y,int pieceNumber) 
    {
        const int pieceColum = 11;
        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                string str = piece[i+pieceColum*pieceNumber][j][0].ToString();
                int number = int.Parse(str);
                Instantiate(mapObjects[number], new Vector3(transform.position.x + (j + width * x)*massSizeX, transform.position.y - (i + height * y)*massSizeY, 0), Quaternion.identity);
                
               
            }
        }
    }

    private void MakeOuterWall()//マップの外壁を生成するプログラム
    {
        const int wall = 1;
        for(int x=0;x<width*mapWidth;x++)
        {
            Instantiate(mapObjects[wall], new Vector3(x,-height,0), Quaternion.identity);
            Instantiate(mapObjects[wall],new Vector3(x,height*mapHeight-height+1,0), Quaternion.identity);
        }

        for(int y=0;y<height*mapHeight+2;y++)
        {
            Instantiate(mapObjects[wall], new Vector3(-1, y-height, 0), Quaternion.identity);
            Instantiate(mapObjects[wall], new Vector3(width*mapWidth, y-height, 0), Quaternion.identity);
        }
    }
}
