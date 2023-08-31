using UnityEngine;

public class borderBuilder : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private ObjectPool wallPool;

    private bool drawn = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!drawn)
        {
            float leftBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin, 0,0)).x;
            float rightBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax, 0,0)).x;
            float topBorder = cam.ScreenToWorldPoint(new Vector3(0, Screen.safeArea.yMax,0)).y;

            GameObject top = wallPool.GetPooledObject();
            top.SetActive(true);

            GameObject left = wallPool.GetPooledObject();
            left.SetActive(true);
            
            GameObject right = wallPool.GetPooledObject();
            right.SetActive(true);

            left.transform.position = new Vector3(leftBorder-left.transform.localScale.x/2, 0, 0);
            right.transform.position = new Vector3(rightBorder+right.transform.localScale.x/2, 0, 0);
            top.transform.position = new Vector3(0, topBorder+top.transform.localScale.x/2, 0);
            top.transform.Rotate(0,0,90);
            drawn = true;
        }
    }
}
