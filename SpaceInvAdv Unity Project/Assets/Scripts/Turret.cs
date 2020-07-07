using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    List<Projectile> projectilePool;
    public int projectilePoolCount;

    public Animator anim;

    public Projectile projectilePrefab;

    private void Awake()
    {
        InitializeTurret();
    }

    void InitializeTurret()
    {
        projectilePool = new List<Projectile>();

        anim = GetComponentInChildren<Animator>();

        for (int i = 0; i < projectilePoolCount; i++)
        {
            Projectile p = Instantiate(projectilePrefab);
            projectilePool.Add(p);
            p.gameObject.SetActive(false);
        }

    }

    public void ShootProjectile(Vector3 turretVeclocity)
    {
        for (int i = 0; i < projectilePool.Count; i++)
        {
            if(projectilePool[i].gameObject.activeInHierarchy == false)
            {
                anim.Play("pl_shot_01");

                Projectile p = projectilePool[i];
                p.gameObject.SetActive(true);
                p.transform.position = transform.position;
                p.transform.parent = transform;
                p.SetProjectileSpeed(20f, turretVeclocity);
                return;

            }
        }
    }
}
