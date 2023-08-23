using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class BlockPoolManager : MonoBehaviour
{

    [SerializeField] private ObjectPool pool;
    [SerializeField] private Camera cam;
    [SerializeField] private int blocksInRow;
    [SerializeField] private int blocksInCol;
    private float rowWidth;
    private float colWidth;

    private float leftBorder;
    private float rightBorder;
    private float topBorder;
    private float bottomBorder;

    List<Vector2> blockPositions;
    List<bool> alive = new List<bool>();

    private void Awake() {
        leftBorder = cam.ScreenToWorldPoint(new Vector3(0,0,0)).x;
        topBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.height,0)).y;
        bottomBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.height/2,0)).y;
        rightBorder = cam.ScreenToWorldPoint(new Vector3(Screen.width,0,0)).x;
    }

    void Start()
    {
        Assert.AreEqual(blocksInCol*blocksInRow, pool.amountToPool);
        blockPositions = calculateBlocks();
    }

    private List<Vector2> calculateBlocks()
    {
        List<Vector2> result = new List<Vector2>(pool.amountToPool);
        return result;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }
}
