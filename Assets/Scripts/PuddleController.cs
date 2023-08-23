using UnityEngine;

public class PuddleController : MonoBehaviour
{

    [SerializeField] private GameObject puddle;
    [SerializeField] private float speed;
    private float leftBorder;
    private float rightBorder;
    [SerializeField] private float middleClamp;
    [SerializeField] private Camera cam;
    private bool gyro;
    // Start is called before the first frame update
    void Start()
    {
        if(SystemInfo.supportsGyroscope)
        {
            Input.gyro.enabled =false;
            Input.gyro.enabled = true;
            gyro = true;
        }
        else
        {
            gyro = false;
        }

        Debug.Log(gyro?"gyro on":"gyro off");
        // get left border in world units
        leftBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin,0,0)).x 
        + puddle.transform.localScale.x/2;
        rightBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax,0,0)).x 
        - puddle.transform.localScale.x/2;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal;
        if (!gyro)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else{
            // get horizontal tilt relative to gravity
            horizontal = Input.gyro.gravity.x;
        }

        if(gyro && Mathf.Abs(horizontal)<middleClamp)
        {
            horizontal = 0;
        }

        if(horizontal!=0)
        {
            Vector3 newPos = puddle.transform.position + 
            new Vector3(horizontal, 0, 0).normalized * speed * Time.deltaTime;
            puddle.transform.position = new Vector3(Mathf.Clamp(newPos.x,leftBorder,rightBorder), 
            newPos.y, 
            newPos.z);
        }
    }
}
