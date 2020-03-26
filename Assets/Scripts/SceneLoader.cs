using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoader : MonoBehaviour
{

    public void PlayButtonOnClick() {
        SceneManager.LoadScene("Main");
    }

    public void MenuButtonOnClick() {
        SceneManager.LoadScene("Menu");
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
