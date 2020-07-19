using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : LivingEntity
{
    public float speed;
    int lifeTime = 3;
    float startTime = 0f;

    private void OnEnable()
    {
        startTime = 0f;
    }

    private void Update()
    {
        MoveDownward();
        startTime += Time.deltaTime;

        if(startTime > lifeTime)
        {
            Die();
        }
    }

    public override void Die()
    {
        gameObject.SetActive(false);
    }



    void MoveDownward()
    {
        transform.position = transform.position - new Vector3(0, speed, 0) * Time.deltaTime;
    }


}
