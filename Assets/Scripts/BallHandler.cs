using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{
    private float leftBorder, rightBorder, topBorder, bottomBorder;
    private Camera cam;

    // Start is called before the first frame update
    void Awake()
    {
        cam = FindFirstObjectByType<Camera>();
        
        leftBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin,0,0)).x;
        topBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.safeArea.yMax,0)).y;
        bottomBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.safeArea.center.y,0)).y;
        rightBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax,0,0)).x;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x <= leftBorder + transform.localScale.x/2)
        {
            //GetComponent<Rigidbody2D>().AddForce();
        }
    }
}
