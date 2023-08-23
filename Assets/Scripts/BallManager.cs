using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BallManager : MonoBehaviour
{

    [SerializeField] private ObjectPool pool;
    [SerializeField] private GameObject puddle;
    [SerializeField] private float forceScale;
    private GameObject puddleBall;
    private int alive;

    private void Awake() {
    }

    // Start is called before the first frame update
    void Start()
    {
        alive = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(alive ==0)
        {
            addBall();
        }

        if (puddleBall != null)
        {
            puddleBall.transform.position = new Vector3(
                puddle.transform.position.x, 
                puddleBall.transform.position.y,
                puddleBall.transform.position.z);
            if(Input.anyKey)
            {
                puddleBall.GetComponent<Rigidbody2D>().AddForce(
                    getRandomDir()*forceScale,
                    ForceMode2D.Impulse);
                puddleBall = null;
            }
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
        alive++;
        sendBallToPuddle(puddleBall);
    }

    void sendBallToPuddle(GameObject ball)
    {
        Vector3 ballPos = puddle.transform.position + 
        puddle.transform.up.normalized * (puddle.transform.localScale.y/2 + 
        ball.transform.localScale.y/2);
        ball.transform.position = ballPos;
        ball.SetActive(true);
    }
}
