using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallHandler : MonoBehaviour
{

    private float speedMagnitude;
    private Vector2 speed;

    private BallManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        speedMagnitude = 3;
        manager = FindAnyObjectByType<BallManager>();
        speed = Vector2.zero;
    }

    public void setSpeed(Vector2 speed)
    {
        this.speed = speed;
    }

    private void Update() {
        if (!manager.isPuddleBall(this.gameObject))
        {
            GetComponent<Rigidbody2D>().velocity = speed*speedMagnitude;
        }
        manager.checkBallAlive(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.contactCount > 0)
        {
            // get other object normal vector
            Vector2 normal = other.contacts[0].normal;
            // calculate refelction of speed
            Vector2 reflection = Vector2.Reflect(speed, normal).normalized;
            // add random change in speed angle
            reflection = randomizeCollision(reflection,normal);
            // set new speed
            other.otherRigidbody.velocity = reflection * speedMagnitude;
            speed = reflection;

            if(other.collider.CompareTag("Puddle"))
            {
                other.collider.attachedRigidbody.velocity = Vector2.zero;
            }
        }
    }

    private Vector2 randomizeCollision(Vector2 reflection,Vector2 normal)
    {
        Vector2 par = (speed+normal).normalized;
        par = par*Random.Range(0f,5f);
        return (reflection + par).normalized;
    }

}
