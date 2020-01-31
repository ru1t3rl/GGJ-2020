using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] List<Bullet> bullets;
    [SerializeField] int maxBullets;
    int currentBullet;

    public virtual void Start()
    {
        for (int iBullet = 0; iBullet < maxBullets; iBullet++)
        {
            bullets.Add(Instantiate(bullet).GetComponent<Bullet>());
            bullets[bullets.Count - 1].parent = this.gameObject;
        }

        currentBullet = 0;
    }

    public virtual void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            Shoot();
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            Reload();
        }
    }

    public virtual void Shoot()
    {
        if (currentBullet < maxBullets - 1)
        {
            bullets[currentBullet].position = transform.position;
            bullets[currentBullet].direction = transform.forward;
            bullets[currentBullet].rotation = transform.parent.rotation;

            bullets[currentBullet].gameObject.SetActive(true);

            currentBullet++;
        }
        else
            Debug.LogError("Out of Ammo");
    }

    public void Reload()
    {
        currentBullet = 0;
    }
}
