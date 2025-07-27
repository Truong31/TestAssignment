using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject levelClearPanel;

    public void ShowGameOver()
    {
        AudioManager.Instance.PlayLoseSound();
        gameOverPanel.SetActive(true);
    }

    public void ShowLevelClear()
    {
        AudioManager.Instance.PlayWinSound();
        levelClearPanel.SetActive(true);
    }

    public void CallNextLevel()
    {
        levelClearPanel.SetActive(false);
        GameManager.Instance.LoadLevel();
    }

    public void RestartLevel()
    {
        gameOverPanel.SetActive(false);
        GameManager.Instance.LoadLevel();
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
