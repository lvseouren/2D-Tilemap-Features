using UnityEngine;
using System.Collections;
using Boo.Lang;
using System.Collections.Generic;

public class BulletPool
{
    static BulletPool instance;
    Queue<Bullet> bullets = new Queue<Bullet>();
    public static BulletPool Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new BulletPool();
            }
            return instance;
        }
    }
        
    public Bullet GetBullet()
    {
        if(bullets.Count>0)
        {
            return bullets.Dequeue();
        }
        return null;
    }

    public void Recycle(Bullet bt)
    {
        bullets.Enqueue(bt);
    }
}
