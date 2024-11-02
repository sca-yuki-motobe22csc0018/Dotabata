using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//これはPlayerのStateを管理するクラスです
//担当：熊谷
public class PlayerManager : MonoBehaviour
{

    enum PlayerState
    {
        START,
        END,
        SIZE,
    }

PlayerState state;
delegate void StateFunction();
StateFunction[] StateFunctions;
int stateCount = 0;

// Start is called before the first frame update
void Start()
{
    state = PlayerState.START;
    StateFunctions = new StateFunction[(int)PlayerState.SIZE];
    AddFunction(One);
    AddFunction(Two);
    if (stateCount < (int)PlayerState.SIZE)
    {
        Debug.Log("PlayerStateに対応する関数の代入が足りてないよ。GameManager");
    }
}

// Update is called once per frame
void Update()
{
    StateFunctions[(int)state]();
    if (Input.GetKeyDown(KeyCode.Alpha1)) { state = PlayerState.START; }
    if (Input.GetKeyDown(KeyCode.Alpha2)) { state = PlayerState.END; }
}


    /// <summary>
    /// 
    /// </summary>
    /// <param name="function"></param>
    void AddFunction(StateFunction function)
    {
        if (stateCount >= (int)PlayerState.SIZE)
        {
            Debug.Log("PlayerStateに対応する関数が全て入っています。StateFunctionsへの代入を確認してください。PlayerManager");
        }
        StateFunctions[stateCount++] = function;
    }

    private void One()
    {
        
    }
    private void Two()
    {

    }

}
