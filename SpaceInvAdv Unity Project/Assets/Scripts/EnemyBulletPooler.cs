using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletPooler : MonoBehaviour
{
    public EnemyProjectile p1Prefab;
    public List<EnemyProjectile> p1List;

    private void Awake()
    {
        p1List = new List<EnemyProjectile>();

        for (int i = 0; i < 40; i++)
        {
            p1List.Add(Instantiate(p1Prefab));
            p1List[i].gameObject.SetActive(false);
        }
    }

    public EnemyProjectile ProjectileFromPool()
    {
        for (int i = 0; i < p1List.Count; i++)
        {
            if (!p1List[i].gameObject.activeInHierarchy)
            {
                p1List[i].gameObject.SetActive(true);
                return p1List[i];
            }
        }
        Debug.Log("null bullet");
        return null;
    }

}
