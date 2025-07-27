using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    private BoxHandle[] boxes;
    private Bottle[] bottles;
    private BaseSlot[] bases;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        int unLockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
    }

    void Update()
    {
        UpdateObject();
    }

    public void LevelClear()
    {
        UpdateObject();
        Debug.Log("Box length, Bottle length: " + boxes.Length + ", " + bottles.Length);
        if (boxes.Length == 0 && bottles.Length == 0)
        {
            Debug.Log("Level Clear");
            PlayerPrefs.SetInt("UnlockedLevel", SceneManager.GetActiveScene().buildIndex + 1);
            UIManager ui = FindObjectOfType<UIManager>();
            ui.ShowLevelClear();

        }

    }

    public void GameOver()
    {
        int baseOccupied = 0;
        for (int i = 0; i < bases.Length; i++)
        {
            if (bases[i].isOccupied)
            {
                baseOccupied++;
                Debug.Log("Base occupied: " + baseOccupied);
            }
        }
        if (baseOccupied == bases.Length && boxes.Length > 0 && bottles.Length > 0)
        {
            Debug.Log("Game Over");
            UIManager ui = FindObjectOfType<UIManager>();
            ui.ShowGameOver();
            Time.timeScale = 0f;
        }
    }

    public void LoadLevel()
    {
        Time.timeScale = 1f;
        string sceneName = "Level" + PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (Application.CanStreamedLevelBeLoaded(sceneName))
        {
            SceneManager.LoadScene(sceneName);
        }
    }

    //Update Box, Bottle, and BaseSlot references
    private void UpdateObject()
    {
        boxes = FindObjectsOfType<BoxHandle>();
        bottles = FindObjectsOfType<Bottle>();
        bases = FindObjectsOfType<BaseSlot>();
    }
}
