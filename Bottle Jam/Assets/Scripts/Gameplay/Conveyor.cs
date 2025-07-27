using System.Collections.Generic;
using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [SerializeField] private Bottle bottle;
    [SerializeField] private Transform containerTransform;
    [SerializeField] private float bottleSpacing = 0.5f;

    private List<Bottle> bottles = new List<Bottle>();

    void Awake()
    {
        foreach(Transform child in containerTransform)
        {
            Bottle newBottle = child.GetComponent<Bottle>();
            if (newBottle != null)
            {
                bottles.Add(newBottle);
            }
        }
    }

    //Update bottle position if there is any change on the conveyor
    private void UpdateBottlePositions()
    {
        if (bottles.Count > 0)
        {
            for (int i = 0; i < bottles.Count; i++)
            {
                Vector3 pos = containerTransform.position + new Vector3(0, -2.25f, 0) + Vector3.up * bottleSpacing * i;
                bottles[i].transform.position = pos;
            }
        }
    }

    public void MatchBottle()
    {
        if (bottles.Count > 0)
        {
            Bottle bottle = bottles[0];
            if (bottle != null)
            {
                bottle.FindBox();
                if (bottle.isMoved)
                {
                    bottles.Remove(bottle);
                }
                UpdateBottlePositions();
            }
        }
    }
}
