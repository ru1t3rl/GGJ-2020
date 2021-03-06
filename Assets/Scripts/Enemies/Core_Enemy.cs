﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Core_Enemy : MonoBehaviour
{
    [Header("General")]
    protected float maxHealth;
    [HideInInspector] public float health;
    public int scoreWorth;

    public float damageToTarget;

    [Header("Movement")]
    public Vector3 velocity;
    public float maxSpeed;
    public float visionRange;

    [Header("Target")]
    public GameObject _target;

    public Renderer rend;
    public Collider col;
    public VisualEffect deathParticlees;

    public bool disabled;

    [SerializeField] private AudioClip DeathSound;

    public virtual void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        deathParticlees.Stop();
        _target = GameManager.Player;
        maxHealth = 100f;
    }

    public virtual void Update()
    {
        MoveObject();
    }


    void OnCollisionEnter(Collision collision)
    {
        Player pl = collision.collider.GetComponent<Player>();
        if(pl != null)
        {
            pl.DoDamage(10);
        }
    }

    public virtual void MoveObject()
    {
        if (_target != null)
        {
            TrackTarget(_target);
            //Truncate(ref velocity, maxSpeed);
            velocity = velocity.normalized * maxSpeed;
            this.gameObject.transform.position += velocity * Time.deltaTime;
        }
    }

    public virtual void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            AudioManager.Instance.PlaySFX(DeathSound, 0.2f);
            deathParticlees.Play();
            Die();
            return;
        }
    }

    public virtual void Die()
    {
        col.enabled = false;
        rend.enabled = false;
        disabled = true;
        StartCoroutine(Disable(3));
    }

    public virtual void TrackTarget(GameObject _object)
    {
        float distance = Vector3.Distance(gameObject.transform.position, _object.transform.position);

        if (distance < visionRange && distance >= 1.5f && !disabled)
        {
            velocity = _object.transform.position - gameObject.transform.position;
        }
        else
        {
            velocity = Vector3.zero;
        }
    }

    public float GetMaxhealth()
    {
        return maxHealth;
    }

    public float SetMaxHealth(float _maxHealth)
    {
        maxHealth = _maxHealth;
        return maxHealth;
    }

    IEnumerator Disable(float time)
    {
        yield return new WaitForSeconds(time);
        col.enabled = true;
        rend.enabled = true;
        gameObject.SetActive(false);
    }
}
