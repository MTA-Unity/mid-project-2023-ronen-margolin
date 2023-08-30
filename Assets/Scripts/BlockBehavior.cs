using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour
{
    private BlockPoolManager manager;

    public int index{ get; set; }

    private void Awake() {
        manager = GetComponentInParent<BlockPoolManager>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        manager.destroyBlock(this);
    }
}
