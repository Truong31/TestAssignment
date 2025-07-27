using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public bool isMoved = false;
    public bool isMoving = false;

    //Move box smoothly to the box slot
    IEnumerator MoveToBox(Transform targetPosition)
    {
        isMoving = true;
        float elapsed = 0f;
        float duration = 2f;

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition.position, elapsed / duration);
            yield return null;
        }
        transform.position = targetPosition.position;
    }


    //Find the box slot that matches the bottle color and move to it
    public void FindBox()
    {
        BoxHandle[] slots = FindObjectsOfType<BoxHandle>();
        foreach (BoxHandle slot in slots)
        {
            if (slot.isBusy && slot.GetComponent<SpriteRenderer>().color == GetComponent<SpriteRenderer>().color)
            {
                for (int i = 0; i < slot.boxSlots.Length; i++)
                {
                    if (slot.boxSlots[i].GetComponent<BoxSlot>().isOccupied == false)
                    {
                        slot.boxSlots[i].GetComponent<BoxSlot>().isOccupied = true;
                        slot.occupiedCount++;
                        Debug.Log(slot.occupiedCount + " occupied slots in this box");
                        StartCoroutine(MoveToBox(slot.boxSlots[i].transform));
                        AudioManager.Instance.PlayBottleFilledSound();
                        gameObject.transform.SetParent(slot.boxSlots[i]);
                        isMoved = true;
                        return;
                    }
                }
            }
        }
    }
}
