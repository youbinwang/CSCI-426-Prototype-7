using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowController : MonoBehaviour
{
    private Rigidbody2D rb;
    private bool hasHit;
    private ShakeObject shaker;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shaker = GetComponent<ShakeObject>();
    }

    private void Update()
    {
        if (!hasHit)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag != "CanHit")
        {
            hasHit = true;
            rb.velocity = Vector2.zero;
            rb.isKinematic = true;
            rb.gravityScale = 0.0f;
            shaker.Shake(0.3f,0.1f);
        }
    }
}
