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
            if (line[0]=='E')
            {
                pieceCounter++;
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

    private void MakeOuterWall()//�}�b�v�̊O�ǂ𐶐�����v���O����
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
