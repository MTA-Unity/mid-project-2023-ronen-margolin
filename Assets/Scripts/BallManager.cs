using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class BallManager : MonoBehaviour
{

    [SerializeField] private ObjectPool pool;
    [SerializeField] private GameObject puddle;

    private void Awake() {
        
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void sendBallToPuddle()
    {
        GameObject ball = pool.GetPooledObject();
        Vector3 ballPos = puddle.transform.position + 
        puddle.transform.up.normalized * (puddle.transform.localScale.y/2 + 
        ball.transform.localScale.y/2);
        ball.transform.position = ballPos;
        ball.SetActive(true);
        /* activeBalls.Add(ball);
        activeBallsCount++;
        puddleBall = ball; */
    }
}
