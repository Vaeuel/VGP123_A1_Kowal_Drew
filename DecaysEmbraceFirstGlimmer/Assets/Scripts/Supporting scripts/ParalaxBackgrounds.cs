using UnityEngine;

public class ParalaxBackgrounds : MonoBehaviour
{
    Camera cam;
    SpriteRenderer sr;
    

    public float parallaxFactor;

    private float curXPos;
    private float prevXPos;
    private float curXOffset;
    private float prevXOffset;
    private Material mat;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        cam = Camera.main;

        mat = sr.material;
        mat.SetTexture("_MainTex", sr.sprite.texture);

    }

    // Update is called once per frame
    void Update()
    {
        curXPos = cam.transform.position.x;
        curXOffset = prevXOffset + ((prevXPos - curXPos) * parallaxFactor * -1);

        mat.SetFloat("_xOffset", curXOffset);

        prevXPos = curXPos;
        prevXOffset = curXOffset;
    }

    private void LateUpdate()
    {
        transform.position = new Vector2(cam.transform.position.x, cam.transform.position.y);
    }
}
