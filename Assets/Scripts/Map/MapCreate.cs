using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

/// <summary>
/// 担当:熊谷
/// </summary>


public class MapCreate : MonoBehaviour
{

    delegate void function();
    const int height = 13;
    const int width = 13;
    [SerializeField] int mapNumber = 0;
    [SerializeField] TextAsset neutralMapCSV;
    [SerializeField] GameObject[] mapObjects;
    [SerializeField] GameObject[] noColliderMapObjects;
    [SerializeField] GameObject handBackGround;
    [SerializeField] GameObject handWindow;
    [SerializeField] GameObject cameraObject;
    [SerializeField] Canvas canvas;
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

    [SerializeField] GameObject[] ores;

    private void Awake()
    {
        pieceData = new List<string[]>();
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
    /// <param name="pieceNumber">生成したいピースの番号</param>
    /// <param name="pieceSize">生成したいピースのサイズ</param>
    ///  <param name="createPos">生成したいピースの座標</param>
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
                if (number == 2)
                {
                    int random=Random.Range(0, ores.Length);//鉱石の中からランダムに生成
                    GameObject obj = Instantiate(ores[random],new Vector3(posX,posY,0),Quaternion.identity);
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
    /// ピースを所定の位置に生成する関数
    /// </summary>
    /// <param name="piece">全てのピースのデータが格納されているデータ(読み取ったCSV)</param>
    /// <param name="x">生成するピースの横位置</param>
    /// <param name="y">生成するピースの縦位置</param>
    /// <param name="pieceNumber">生成したいピースの番号</param>
    ///  <param name="createPos">生成したいピースの座標</param>
    /// <param name="parent">生成したオブジェクトの親</param>
    /// <param name="necessityCollider">colliderが必要かどうかTrueで必要</param>
    public void PieceCreator(List<string[]> piece, int pieceNumber, float pieceSize,Vector3 createPos, GameObject parent,bool necessityCollider)
    {
        const int pieceColum = 15;
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
                    string str = piece[i + pieceColum * pieceNumber][j][0].ToString();
                    number = int.Parse(str);
                }
               
                float posX = (transform.position.x + (j + width) * pieceSize) - width - width / 2 + createPos.x;
                float posY = (transform.position.y - (i + height) * pieceSize) + height + height / 2 + createPos.y;
              
                if (necessityCollider)
                {
                    GameObject obj = Instantiate(mapObjects[number], new Vector3(posX,
                    posY, 0), Quaternion.identity);
                    obj.transform.parent = parent.transform;
                    //Instantiateで親を設定すると比率を再度調整しないといけなくなるためこの方法で親を設定
                    if (number == 2)
                    {
                        Ore ore = obj.GetComponent<Ore>();
                        Ore.OreInfo Info=ore.Info;
                        Info.number = number-2;//鉱石のCSVの番号が2~だが、鉱石番号は0からにしたいので2を引く
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
