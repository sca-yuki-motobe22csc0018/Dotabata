using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//�����Player��State���Ǘ�����N���X�ł�
//�S���F�F�J
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
    private Camera playerCamera;


    private void Awake()
    {
        StateFunctions = new StateFunction[(int)PlayerState.SIZE];
    }
    // Start is called before the first frame update
    void Start()
    {
        playerCamera=cameraObject.GetComponent<Camera>();
        state = PlayerState.PlayerMove ;
    }

// Update is called once per frame
void Update()
{
        StateFunctions[(int)state]();
        Debug.Log(state);
        if(Input.GetMouseButtonDown(1))
        {
            state++;
        }
        if((int)state==(int)PlayerState.SIZE)
        {
            state=0;
        }
    
}   


    /// <summary>
    /// �X�e�[�g�Ǘ����s���z��Ɉ����Ƃ��ēn�����֐���������֐��ł�
    /// </summary>
    /// <param name="function">����������֐�</param>
    public void AddFunction(StateFunction function)
    {
        if (stateCount >= (int)PlayerState.SIZE)
        {
            Debug.Log("PlayerState�ɑΉ�����֐����S�ē����Ă��܂��BStateFunctions�ւ̑�����m�F���Ă��������BPlayerManager");
            return;
        }
        StateFunctions[stateCount++] = function;
    }

    /// <summary>
    /// �X�e�[�g�Ǘ����s���z��Ɉ����Ƃ��ēn�����֐���������֐��ł�
    /// </summary>
    /// <param name="function">����������֐�</param>
    public void AddFunction(StateFunction function,int number)
    {
        StateFunctions[number] = function; 
    }
    private void One()
    {
        playerCamera.fieldOfView = 10;
        Debug.Log("WwwwW");
    }
    private void Two()
    {
    }

}
�@�@