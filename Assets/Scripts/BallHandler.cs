using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallHandler : MonoBehaviour
{

    private Vector2 speed;

    private BallManager manager;

    // Start is called before the first frame update
    void Awake()
    {
        manager = FindAnyObjectByType<BallManager>();
        speed = Vector2.zero;
    }

    public void setSpeed(Vector2 speed)
    {
        this.speed = speed;
    }

    private void Update() {
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
            reflection = new Vector2(reflection.x 
            + Random.Range(-0.3f, 0.3f), reflection.y 
            + Random.Range(-0.3f, 0.3f)).normalized;
            // adjust to same magnitude
            reflection = reflection * speed.magnitude;
            // set new speed
            other.otherRigidbody.velocity = reflection;
            setSpeed(reflection);

            if(other.collider.CompareTag("Puddle"))
            {
                other.collider.attachedRigidbody.velocity = Vector2.zero;
            }
        }
    }

}
