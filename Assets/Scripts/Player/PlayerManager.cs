using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����Player��State���Ǘ�����N���X�ł�
//�S���F�F�J
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
        Debug.Log("PlayerState�ɑΉ�����֐��̑��������Ă��܂���BGameManager");
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
    /// �X�e�[�g�Ǘ����s���z��Ɉ����Ƃ��ēn�����֐���������֐��ł�
    /// </summary>
    /// <param name="function">����������֐�</param>
    void AddFunction(StateFunction function)
    {
        if (stateCount >= (int)PlayerState.SIZE)
        {
            Debug.Log("PlayerState�ɑΉ�����֐����S�ē����Ă��܂��BStateFunctions�ւ̑�����m�F���Ă��������BPlayerManager");
        }
        StateFunctions[stateCount++] = function;
    }

    private void One()
    {
        Debug.Log(1);
    }
    private void Two()
    {
        Debug.Log(2);
    }

}
�@�@