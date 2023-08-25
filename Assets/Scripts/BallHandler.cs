using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallHandler : MonoBehaviour
{

    private Vector2 speed;

    // Start is called before the first frame update
    void Awake()
    {
        speed = Vector2.zero;
    }

    private void Update() {
        
    }

    public void setSpeed(Vector2 speed)
    {
        this.speed = speed;
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if(other.contactCount > 0)
        {
            // get other object normal vector
            Vector2 normal = other.contacts[0].normal;
            // calculate refelction of speed
            Vector2 reflection = Vector2.Reflect(speed, normal);
            // add random change in speed angle
            reflection = new Vector2(reflection.x 
            + Random.Range(-0.1f, 0.1f), reflection.y 
            + Random.Range(-0.1f, 0.1f)).normalized;
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
