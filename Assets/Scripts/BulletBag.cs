using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBag : MonoBehaviour {
    public int bulletCount = 10; //包的子弹

    public ParticleSystem collectEffect; //拾取特效

    void OnTriggerEnter2D(Collider2D other)
    {
        PlayerController pc = other.GetComponent<PlayerController>();
        if (pc != null)
        {
            if (pc.MyCurBulletCount < pc.MyMaxBulletCount)
            {
                pc.ChangeBulletCount(bulletCount);
                Destroy(this.gameObject);
                Instantiate(collectEffect, transform.position, Quaternion.identity);
                
            }
        }
    }
}
