using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public int health = 1;
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        GetHit();
    }

    void GetHit()
    {
        health--;
        if (health < 1)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
