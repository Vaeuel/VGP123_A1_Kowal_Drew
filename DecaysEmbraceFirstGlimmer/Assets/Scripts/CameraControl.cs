using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_movement : MonoBehaviour
{

    public Transform playerTrans;
    public float minXVal;
    public float maxXVal;
    public float minYVal;
    public float maxYVal;


    // Start is called before the first frame update
    void Start()
    {
        if (playerTrans == null)
            Debug.Log("Set player transform on camera script");
    }

    // Update is called once per frame
    void Update()
    {
        if (!playerTrans) return;

        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(playerTrans.position.x, minXVal, maxXVal);
        pos.y = Mathf.Clamp((playerTrans.position.y + 3), minYVal, maxYVal);
        transform.position = pos;
    }
}
