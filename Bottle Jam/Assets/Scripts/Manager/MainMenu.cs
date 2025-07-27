using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void StartGame()
    {
        int level = PlayerPrefs.GetInt("UnlockedLevel", 1);
        SceneManager.LoadScene("Level" + level);
    }

    public void NewGame()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);
        PlayerPrefs.Save();
        SceneManager.LoadScene("Level1");
    }
}
