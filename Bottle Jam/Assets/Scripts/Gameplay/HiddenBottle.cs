using UnityEngine;

public class HiddenBottle : MonoBehaviour
{
    private Color color;
    private SpriteRenderer spriteRenderer;
    private Bottle bottle;
    [SerializeField] private LayerMask layer;

    void Start()
    {
        spriteRenderer = GetComponentInParent<SpriteRenderer>();
        bottle = GetComponentInParent<Bottle>();
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
        if (!bottle.isMoving)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position - new Vector3(0, 0.3f, 0), Vector2.down, checkDistance, layer);
            if (hit.collider != null)
            {
                return false;
            }
        }
        return true;
    }
}
