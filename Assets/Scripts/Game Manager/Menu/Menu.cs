using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public GameObject gameMenuUI;

    private void Start()
    {
        gameMenuUI.SetActive(false);
    }
    public void Retry() 
    {
        gameMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause() 
    {
        gameMenuUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void NewGame() 
    {
        SceneManager.LoadScene("Game");
    }

    public void MineMenu() 
    {
        SceneManager.LoadScene("MineMenu");
    }

    public void Exit() 
    {
        Application.Quit();
    }
}
