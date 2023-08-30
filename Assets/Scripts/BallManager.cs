using System.Collections.Generic;
using UnityEngine;

public class BallManager : MonoBehaviour
{

    [SerializeField] private ObjectPool pool;
    [SerializeField] private GameObject puddle;
    [SerializeField] private float forceScale;
    [SerializeField] private Camera cam;

    [SerializeField] private DeathController deathMenu;
    [SerializeField] private int lives = 3;
    private GameObject text;
    private GameObject puddleBall;

    private List<GameObject> active_balls;
    private int alive;
    private float bottomBorder;

    public bool gameActive = true;

    private TMPro.TextMeshProUGUI textMesh;

    private void Awake() {
        // get by tag
        text = GameObject.FindGameObjectWithTag("lives_text");
        Debug.Assert(lives>=1);
        bottomBorder = cam.ScreenToWorldPoint(new Vector3(0,Screen.safeArea.yMin,0)).y;
        textMesh = text.GetComponent<TMPro.TextMeshProUGUI>();
        deathMenu.CloseMenu();
        active_balls = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        alive = 0;
        lives++;
        textMesh.text = "lives: " + lives;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameActive)
        {
            if(alive ==0)
            {
                addBall();
                lives--;
                textMesh.text = "lives: " + lives;
                if (lives == 0)
                {
                    deathMenu.OpenMenu(true);
                }
            }

            if (puddleBall != null)
            {
                puddleBall.transform.position = new Vector3(
                    puddle.transform.position.x, 
                    puddleBall.transform.position.y,
                    puddleBall.transform.position.z);
                if(Input.anyKey)
                {
                    puddleBall.GetComponent<BallHandler>().setSpeed(getRandomDir());
                    puddleBall = null;
                }
            }
        }
    }

    public void checkBallAlive(GameObject ball)
    {
        if(ball.transform.position.y<bottomBorder)
        {
            ball.SetActive(false);
            alive--;
        }
    }

    Vector2 getRandomDir()
    {
        // random vector with negative y value
        return new Vector2(Random.Range(-5,5),
        puddle.transform.up.normalized.y).normalized;
    }

    void addBall()
    {
        puddleBall = pool.GetPooledObject();
        sendBallToPuddle(puddleBall);
        active_balls.Add(puddleBall);
        alive++;
    }

    void sendBallToPuddle(GameObject ball)
    {
        Vector3 ballPos = puddle.transform.position + 
        puddle.transform.up.normalized * (puddle.transform.localScale.y/2 + 
        7*ball.transform.localScale.y/13);
        ball.transform.position = ballPos;
        ball.SetActive(true);
    }

    public bool isPuddleBall(GameObject ball)
    {
        return puddleBall == ball;
    }

    public void destroyBalls()
    {
        active_balls.ForEach(ball => ball.SetActive(false));
    }

    public void freezePuddle()
    {
        puddle.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
    }
}
