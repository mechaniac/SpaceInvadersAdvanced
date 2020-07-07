using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LivingEntity : MonoBehaviour
{
    public int health = 1;
    public GameManager gm;

    public GameObject hitPrefab;

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

        if(hitPrefab != null)
        {
            GameObject hit = Instantiate(hitPrefab);
            hit.transform.position = transform.position;
        }
        
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }
}
