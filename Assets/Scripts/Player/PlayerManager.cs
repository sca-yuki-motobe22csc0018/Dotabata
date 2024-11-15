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
        Debug.Log("PlayerState�ɑΉ�����֐��̑��������Ă��܂���BGameManager");
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

    private void One()
    {
        camera.fieldOfView = 10;
        Debug.Log("WwwwW");
    }
    private void Two()
    {
    }

}
�@�@