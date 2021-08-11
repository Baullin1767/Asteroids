using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MineMenu : MonoBehaviour
{
    public void NewGame() 
    {
        SceneManager.LoadScene("Game");
    }
    public void Exit() 
    {
        Application.Quit();
    }
}
