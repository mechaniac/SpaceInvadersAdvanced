using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX_base : MonoBehaviour
{
    public int lifeSpan;

    float lifeTime = 0f;
    private void Update()
    {
        lifeTime += Time.deltaTime;

        if(lifeTime > lifeSpan)
        {
            Destroy(this);
        }
    }
}
