using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    public void Quit() {
        Application.Quit();
    }

    public void LoadSinglePlayer() {
        SceneManager.LoadScene("SinglePlayer");
    }

    public void LoadCoop()
    {
        SceneManager.LoadScene("Co-Op");
    }
}
