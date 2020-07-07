using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : LivingEntity
{
    float speed;
    Vector3 inheritedVelocity;

    private void Update()
    {
        MoveForward();
    }

    public void SetProjectileSpeed(float speed, Vector3 velocityFromParent)
    {
        this.speed = speed;
        inheritedVelocity = velocityFromParent / 3f;
    }

    void MoveForward()
    {
        if (transform.localPosition.y <= 1)
        {
            transform.localPosition = transform.localPosition + new Vector3(0, speed * Time.deltaTime, 0);
        }
        

        if(transform.localPosition.y > 1)
        {
            transform.parent = null;
            transform.localPosition = transform.localPosition + new Vector3(0, speed * Time.deltaTime, 0) + inheritedVelocity * Time.deltaTime;
        }

        if(transform.localPosition.y > 40)
        {
            gameObject.SetActive(false);
        }
    }
}
