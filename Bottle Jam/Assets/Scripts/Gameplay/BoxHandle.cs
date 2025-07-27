using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxHandle : MonoBehaviour
{
    private bool isLocked;
    private bool isBehind;
    private bool canMoveForward = true;
    public bool isMoving = false;
    private BoxCollider2D boxCollider;
    public bool isBusy = false;
    public Transform[] boxSlots;
    public int occupiedCount = 0;
    private bool hasPlayedClearSound = false;

    private float lastClickTime = -1f;
    private float clickDelay = 1f;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Matching();

        if (IsFrontEmpty(1f))
        {
            isLocked = false;
        }
        else if (!IsFrontEmpty(1f))
        {
            isLocked = true;
            isBehind = true;
        }

        if(isBehind && !isLocked && canMoveForward && !isMoving)
        {
            canMoveForward = false;
            MoveTo(new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
        }
    }


    // When the box is clicked, it will find an available BaseSlot and move there
    void OnMouseDown()
    {
        if (Time.time - lastClickTime < clickDelay) return;
        lastClickTime = Time.time;

        if (isBusy || isLocked) return;

        BaseSlot[] slot = FindObjectsOfType<BaseSlot>();
        foreach (BaseSlot s in slot)
        {
            if (!s.isOccupied)
            {
                isBusy = true;
                MoveTo(s.transform.position);
                break;
            }
        }

        if (!isBusy || isLocked)
        {
            AudioManager.Instance.PlayShakeSound();
            StartCoroutine(Shake());
        }
    }

    public void MoveTo(Vector3 targetPosition)
    {
        AudioManager.Instance.PlayMoveBoxSound();
        isMoving = true;
        StartCoroutine(SmoothMovement(targetPosition));
        StartCoroutine(ColliderSwitch());
    }

    // Smoothly move the box to the target
    private IEnumerator SmoothMovement(Vector3 targetPosition)
    {
        float elapsed = 0;
        float duration = 1f;

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            transform.position = Vector3.Lerp(transform.position, targetPosition, elapsed / duration);
            yield return null;
        }
        transform.position = targetPosition;
    }

    //Turn off the collider for a short time to prevent collision issues while moving
    IEnumerator ColliderSwitch()
    {
        boxCollider.enabled = false;
        yield return new WaitForSeconds(0.3f);
        boxCollider.enabled = true;
    }

    //Shake the box if there is no more available Base
    IEnumerator Shake()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;
        float duration = 0.5f;
        float magnitude = 0.05f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float xOffset = Random.Range(-magnitude, magnitude);
            float yOffset = Random.Range(-magnitude, magnitude);
            transform.position = new Vector3(originalPosition.x + xOffset, originalPosition.y + yOffset, originalPosition.z);
            yield return null;
        }

        transform.position = originalPosition;
    }

    bool FullOfBox()
    {
        if (occupiedCount == boxSlots.Length)
        {
            if (!hasPlayedClearSound)
            {
                AudioManager.Instance.PlayClearBoxSound();
                hasPlayedClearSound = true;
            }
            return true;
        }
        hasPlayedClearSound = false;
        return false;
    }

    //Match bottles in the conveyor to boxes
    void Matching()
    {
        Conveyor[] conveyors = FindObjectsOfType<Conveyor>();
        foreach (Conveyor con in conveyors)
        {
            con.MatchBottle();
        }

        if (FullOfBox())
        {
            StartCoroutine(DestroyBox());
            Destroy(gameObject, 0.8f);
        }
    }

    //Check if there is a box in front of the current box
    private bool IsFrontEmpty(float checkDistance)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector2.up, checkDistance);
        if (hit.collider != null && hit.collider.CompareTag("Box"))
        {
            return false;
        }
        return true;
    }

    IEnumerator DestroyBox()
    {
        yield return new WaitForSeconds(0.5f);
        boxCollider.enabled = false;
        float elapsed = 0f;
        float duration = 0.5f;
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = new Vector3(0.05f, 0.05f, 1f);

        while (elapsed <= duration)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            transform.localScale = Vector3.Lerp(originalScale, targetScale, t);
            yield return null;
        }
        transform.localScale = targetScale;
    }
}
