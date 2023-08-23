using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Rendering;

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
        leftBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMin,0,0)).x;
        topBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.safeArea.yMax,0)).y;
        bottomBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.safeArea.center.y,0)).y;
        rightBorder = cam.ScreenToWorldPoint(new Vector3(Screen.safeArea.xMax,0,0)).x;
        colWidth = (rightBorder - leftBorder)/blocksInRow;
        rowWidth = (topBorder - bottomBorder)/blocksInCol;
    }

    void Start()
    {
        Assert.AreEqual(blocksInCol*blocksInRow, pool.amountToPool);
        blockPositions = calculateBlocks();
    }

    private List<Vector2> calculateBlocks()
    {
        List<Vector2> result = new List<Vector2>(pool.amountToPool);
        for(int i=0; i<blocksInCol; i++)
        {
            for(int j=0;j<blocksInRow;j++)
            {
                result.Add(new Vector2(
                    colWidth/2+ leftBorder + j*colWidth,
                    topBorder - rowWidth/2 - i*rowWidth
                ));
                alive.Add(false);
            }
        }
        return result;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        for(int i=0;i<pool.amountToPool;i++)
        {
            drawBlock(i);
        }
    }

    void drawBlock(int i)
    {
        if(!alive[i])
        {
            GameObject block = pool.GetPooledObject();
            block.SetActive(true);
            block.transform.position = blockPositions[i];
            block.transform.localScale = new Vector3(colWidth*0.7f, rowWidth*0.7f, 1);
            alive[i] = true;
        }
    }
}
