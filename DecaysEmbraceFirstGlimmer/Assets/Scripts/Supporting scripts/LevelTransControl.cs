using UnityEngine;
using UnityEngine.Tilemaps;

[RequireComponent(typeof(Tilemap), typeof(TilemapCollider2D), typeof(CompositeCollider2D))]
[RequireComponent (typeof(Rigidbody2D))]
public class LevelTransControl : MonoBehaviour
{
    private Tilemap tm;
    private TilemapCollider2D tmc2d;
    private CompositeCollider2D cc2d;
    private Rigidbody2D rb;
    private Color originalColor;

    private void Start()
    {
        InitSetup();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTransparency(0.2f); // Adjust transparency as needed
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SetTransparency(1f); // Fully visible again
        }
    }

    private void SetTransparency(float alpha)
    {
        Color newColor = originalColor;
        newColor.a = alpha;
        tm.color = newColor;
    }

    void InitSetup()
    {
        tm = GetComponent<Tilemap>();
        originalColor = tm.color;

        tmc2d = GetComponent<TilemapCollider2D>();
        tmc2d.compositeOperation = Collider2D.CompositeOperation.Merge;

        cc2d = GetComponent<CompositeCollider2D>();
        cc2d.isTrigger = true;
        cc2d.geometryType = CompositeCollider2D.GeometryType.Polygons;

        rb = GetComponent<Rigidbody2D>();
        rb.bodyType = RigidbodyType2D.Static;
    }
}
