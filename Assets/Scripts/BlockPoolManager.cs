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
    private bool firstDraw;

    [SerializeField] private DeathController deathController;

    private int totalAlive = 0;

    List<Vector2> blockPositions;
    List<bool> alive = new List<bool>();

    private void Awake() {
        blocksInRow = SceneDto.instance.blocksInRow;
        blocksInCol = SceneDto.instance.blocksInCol;
        pool.amountToPool = blocksInCol*blocksInRow;
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
        firstDraw = true;
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
        if (firstDraw)
        {
            for(int i=0;i<pool.amountToPool;i++)
            {
                drawBlock(i);
            }
            firstDraw = false;
        }
    }

    void Update()
    {
        if(!firstDraw && totalAlive == 0)
        {
            deathController.OpenMenu(false);
        }
    }

    public void destroyBlock(BlockBehavior block)
    {
        block.gameObject.SetActive(false);
        totalAlive--;
        alive[block.index] = false;
    }

    void drawBlock(int i)
    {
        if(!alive[i])
        {
            GameObject block = pool.GetPooledObject();
            block.SetActive(true);
            block.transform.position = blockPositions[i];
            block.transform.localScale = new Vector3(colWidth * 0.99f, rowWidth *0.99f, 1);
            block.GetComponent<Renderer>().material.color = Random.ColorHSV();
            alive[i] = true;
            totalAlive++;
            block.GetComponent<BlockBehavior>().index = i;
        }
    }

    public void setCols(int cols)
    {
        blocksInCol = cols;
    }

    public void setRows(int rows)
    {
        blocksInRow = rows;
    }
}
