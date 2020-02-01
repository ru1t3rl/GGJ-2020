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
    [SerializeField] private AudioClip FiringSound;
    [SerializeField] GameManager gm;

    public Player pl;

    public virtual void Start()
    {
        Cursor.visible = false;
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
            bullets[currentBullet].position = transform.position + transform.forward * (transform.localScale.z / 2);
            bullets[currentBullet].direction = Camera.main.transform.forward;
            bullets[currentBullet].rotation = transform.rotation;

            bullets[currentBullet].gameObject.SetActive(true);

            currentBullet++;

            //AudioManager.Instance.SetSFXVolume(0.01f);
            AudioManager.Instance.PlaySFX(FiringSound, 0.01f);
        }
        else
            gm.ShowWarning("Out of Ammo");
    }

    public int Ammo { get => maxBullets - currentBullet; }

    public void Reload()
    {
        currentBullet = 0;
    }
}
