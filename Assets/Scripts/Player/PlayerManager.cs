using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//これはPlayerのStateを管理するクラスです
//担当：熊谷
public class PlayerManager : MonoBehaviour
{

    public enum PlayerState
    {
        PlayerMove = 0,
        MapCreate=1,
        SIZE=2,
    }

    public static PlayerState state;
    public delegate void StateFunction();
    public StateFunction[] StateFunctions;
    int stateCount = 0;
    [SerializeField] private GameObject cameraObject;
    private Camera camera;


    private void Awake()
    {
        StateFunctions = new StateFunction[(int)PlayerState.SIZE];
        AddFunction(One);
    }
    // Start is called before the first frame update
    void Start()
{
    camera=cameraObject.GetComponent<Camera>();
    state = PlayerState.PlayerMove ;
   
    if (stateCount < (int)PlayerState.SIZE-1)
    {
        Debug.Log("PlayerStateに対応する関数の代入が足りていません。GameManager");
    }
}

// Update is called once per frame
void Update()
{
    StateFunctions[(int)state]();
        Debug.Log(state);
        if(state==PlayerState.PlayerMove)
        {
            if(Input.GetMouseButton(1))
            {
                state= PlayerState.MapCreate;
            }
        }
    
}   


    /// <summary>
    /// ステート管理を行う配列に引数として渡した関数を代入する関数です
    /// </summary>
    /// <param name="function">代入したい関数</param>
    public void AddFunction(StateFunction function)
    {
        if (stateCount >= (int)PlayerState.SIZE)
        {
            Debug.Log("PlayerStateに対応する関数が全て入っています。StateFunctionsへの代入を確認してください。PlayerManager");
            return;
        }
        StateFunctions[stateCount++] = function;
    }

    private void One()
    {
        camera.fieldOfView = 10;
        Debug.Log("WwwwW");
    }
    private void Two()
    {
    }

}
　　