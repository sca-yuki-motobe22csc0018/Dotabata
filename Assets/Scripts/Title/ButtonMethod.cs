using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ButtonMethod : MonoBehaviour
{
    [SerializeField] private string mainSceneName;
    [SerializeField] private string librarySceneName;

    public void OnClickStartButton()
    {
        SceneManager.LoadScene(mainSceneName);
    }

    public void OnClickLibraryButton()
    {
        SceneManager.LoadScene(librarySceneName);
    }

    public void OnClickExitButton()
    {
        Application.Quit();
    }
}
