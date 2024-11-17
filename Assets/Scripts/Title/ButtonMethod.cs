using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMethod : MonoBehaviour
{
    [SerializeField] private Scene scenes;

    public void OnClickStartButton()
    {
        SceneManager.LoadScene("MapTest");
    }

}
