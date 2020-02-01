using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Gun : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    List<Bullet> bullets;
    [SerializeField] int maxBullets;
    int currentBullet;

    [SerializeField] float force;

    public virtual void Start()
    {
        bullets = new List<Bullet>();
        for (int iBullet = 0; iBullet < maxBullets; iBullet++)
        {
            bullets.Add(Instantiate(bullet).GetComponent<Bullet>());
            bullets[bullets.Count - 1].parent = this.gameObject;
            bullets[bullets.Count - 1].force = force;
            bullets[bullets.Count - 1].gameObject.hideFlags = HideFlags.HideInHierarchy;
            bullets[bullets.Count - 1].gameObject.SetActive(false);
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
        if (currentBullet < maxBullets)
        {
            bullets[currentBullet].position = transform.position;
            bullets[currentBullet].direction = transform.forward;
            bullets[currentBullet].rotation = transform.rotation;

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
