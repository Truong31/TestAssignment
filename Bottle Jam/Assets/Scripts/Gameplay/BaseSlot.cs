using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseSlot : MonoBehaviour
{
    public bool isOccupied = false;

    private void Start()
    {
        isOccupied = false;
    }

    // If a box enters the slot, check if the game is over
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isOccupied = true;
            Invoke(nameof(CallGameOver), 1f);
        }
    }

    //If a box exits the slot, check if the level is clear
    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Box"))
        {
            isOccupied = false;
            Debug.Log("Is occupied: " + isOccupied);
            CancelInvoke(nameof(CallGameOver));
            Invoke(nameof(CallLevelClear), 1f);
        }
    }

    void CallGameOver()
    {
        GameManager.Instance.GameOver();
    }

    void CallLevelClear()
    {
        GameManager.Instance.LevelClear();
    }
}
