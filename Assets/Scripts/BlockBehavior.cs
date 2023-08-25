using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        other.otherCollider.gameObject.SetActive(false);
    }
}
