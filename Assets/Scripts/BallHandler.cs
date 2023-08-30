using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BallHandler : MonoBehaviour
{

    private float speedMagnitude;
    private Vector2 speed;

    private bool coliding;

    private BallManager manager;

    private AudioSource audioSource;

    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Awake()
    {
        speedMagnitude = 3;
        manager = FindAnyObjectByType<BallManager>();
        speed = Vector2.zero;
        coliding = false;
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
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
            audioSource.Play();
            if(!coliding)
            {
                StartCoroutine(makeBallPulse());
                coliding = true;
            }
        }
    }

    IEnumerator makeBallPulse()
    {
        float baseRadius = Mathf.Sqrt(Mathf.Pow(transform.localScale.x,2)+Mathf.Pow(transform.localScale.y,2));
        float targetRadius = baseRadius*1.5f;
        Color start = sr.color;
        Color target = Color.yellow;
        for(float radius=baseRadius;radius<targetRadius;radius+=0.05f)
        {
            sr.color = Color.Lerp(start,target,(radius-baseRadius)/(targetRadius-baseRadius));
            setRadius(radius);
            yield return radius;
        }
        for(float radius=targetRadius;radius>baseRadius;radius-=0.05f)
        {
            sr.color = Color.Lerp(target,start,(baseRadius-radius)/(baseRadius-targetRadius));
            setRadius(radius);
            yield return radius;
        }
        sr.color = start;
        setRadius(baseRadius);
        coliding = false;
        yield return baseRadius;
    }

    private void setRadius(float radius)
    {
        float x = Mathf.Sqrt(Mathf.Pow(radius,2)/2);
        float y = x;
        transform.localScale = new Vector3(x,y, transform.localScale.z);
    }

    private Vector2 randomizeCollision(Vector2 reflection,Vector2 normal)
    {
        Vector2 par = (speed+normal).normalized;
        par = par*Random.Range(0f,5f);
        return (reflection + par).normalized;
    }

}
