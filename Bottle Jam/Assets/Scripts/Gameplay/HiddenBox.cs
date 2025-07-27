using UnityEngine;

public class HiddenBox : MonoBehaviour
{
    private Color color;
    private SpriteRenderer spriteRenderer;
    private BoxHandle box;
    [SerializeField] private LayerMask layer;

    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        box = GetComponentInParent<BoxHandle>();
        color = spriteRenderer.color;
    }
    void Update()
    {
        //Debug.DrawRay(transform.position - new Vector3(0, 0.3f, 0), Vector2.down * 0.5f, Color.red);
        if (!IsFrontEmpty(0.5f))
        {
            spriteRenderer.color = Color.black;
        }
        else if (IsFrontEmpty(0.5f))
        {
            spriteRenderer.color = color;
        }
    }

    private bool IsFrontEmpty(float checkDistance)
    {
        if (!box.isMoving)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, 0.8f, 0), Vector2.up, checkDistance, layer);
            if (hit.collider != null)
            {
                return false;
            }
        }
        return true;
    }
}
