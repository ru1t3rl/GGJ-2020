using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Core_Enemy : MonoBehaviour
{
    [Header("General")]
    private float maxHealth;
    [HideInInspector] public float health;

    public float damageToTarget;

    [Header("Movement")]
    public Vector3 velocity;
    public float maxSpeed;
    public float visionRange;

    [Header("Target")]
    public GameObject _target;

    private void Awake()
    {
        _target = GameManager.Player;
        damageToTarget = 1f;
        maxHealth = 100f;
    }

    private void Start()
    {
        visionRange = 25f;
        maxSpeed = 10f;
        health = maxHealth;
    }

    private void Update()
    {
        MoveObject();

        if (Input.GetKeyDown(KeyCode.D))
        {
            TakeDamage(10f);
        }
    }

    //void Truncate(ref Vector3 velocity, float maxSpeed)
    //{
    //    if(velocity.magnitude > maxSpeed)
    //    {
    //        velocity.Normalize();
    //        velocity *= maxSpeed;
    //    }
    //}

    public virtual void MoveObject()
    {
        TrackTarget(_target);
        //Truncate(ref velocity, maxSpeed);
        velocity = velocity.normalized * maxSpeed;
        this.gameObject.transform.position += velocity * Time.deltaTime;
    }

    public virtual void TakeDamage(float _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            Die();
            return;
        }
    }

    public virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void TrackTarget(GameObject _object)
    {
        float distance = Vector3.Distance(gameObject.transform.position, _object.transform.position);

        if (distance < visionRange && distance >= 1.5f)
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
}
