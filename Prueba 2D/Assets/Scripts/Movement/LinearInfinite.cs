using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinearInfinite : Basic_Movement {

    public Vector2 speed = new Vector2(1,0);
    public bool hasMaxDistance = true;
    public bool destroyOnMaxDistance = true;
    public float maxDistance = 20f;

    private Vector3 lastPosition;
    private float currentDistance = 0;

    void Start () {
        rb2d = GetComponent<Rigidbody2D>();
        lastPosition = transform.position;
    }

    void FixedUpdate() {
        rb2d.AddForce(speed * 4);
        Vector2 limitedSpeed = new Vector2(
           Mathf.Clamp(rb2d.velocity.x, -1 * Mathf.Abs(speed.x), Mathf.Abs(speed.x)),
           Mathf.Clamp(rb2d.velocity.y, -1 * Mathf.Abs(speed.y), Mathf.Abs(speed.y))
           );
        rb2d.velocity = new Vector2(limitedSpeed.x, limitedSpeed.y);
    }

    private void Update()
    {
        if (hasMaxDistance)
        {
            float differenceInX = transform.position.x - lastPosition.x;
            float differenceInY = transform.position.y - lastPosition.y;
            currentDistance += Mathf.Sqrt(Mathf.Pow(differenceInX, 2) + Mathf.Pow(differenceInY, 2));
            if (currentDistance >= maxDistance)
            {
                speed = Vector2.zero;
                if (destroyOnMaxDistance)
                    Destroy(gameObject);
            }
            lastPosition = transform.position;
                
        }
    }
}
