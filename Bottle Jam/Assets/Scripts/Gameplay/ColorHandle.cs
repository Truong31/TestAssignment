using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorHandle : MonoBehaviour
{
    public enum ColorType
    {
        Red,
        Green,
        Purple,
        Yellow,
        Blue,
        Orange,
        Pink
    }

    private SpriteRenderer spriteRenderer;

    public ColorType color;

    void OnValidate()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        PickColor();
    }

    void PickColor()
    {
        if (color == ColorType.Red)
        {
            spriteRenderer.color = Color.red;
        }
        else if (color == ColorType.Green)
        {
            spriteRenderer.color = Color.green;
        }
        else if (color == ColorType.Purple)
        {
            spriteRenderer.color = new Color(0.5f, 0f, 0.5f); // Purple color
        }
        else if (color == ColorType.Yellow)
        {
            spriteRenderer.color = Color.yellow;
        }
        else if (color == ColorType.Blue)
        {
            spriteRenderer.color = Color.blue;
        }
        else if (color == ColorType.Orange)
        {
            spriteRenderer.color = new Color(1f, 0.5f, 0f);
        }
        else if (color == ColorType.Pink)
        {
            spriteRenderer.color = new Color(1f, 0.75f, 0.8f);
        }
    }
}
