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

    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequentie = 0.1f;


    public bool fixX = false;
    public bool fixY = false;
    public bool fixZ = false;

    private float angle = 0.0f;
    private Vector3 localScale = Vector3.zero;
    [SerializeField] private Vector3 center = Vector3.zero;

    private void Awake()
    {
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
        Debug.Log("Basic has " + health + " Health");

        MoveObject();

        angle += frequentie;

        if (!fixX)
        {
            velocity.x = (amplitude * Mathf.Sin(angle) + center.x);
        }
        if (!fixY)
        {
            velocity.y = (amplitude * Mathf.Sin(angle) + center.y);
        }
        if (!fixZ)
        {
            velocity.z = (amplitude * Mathf.Sin(angle) + center.z);
        }

        transform.localScale = localScale;

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

        if (distance < visionRange && distance >= 2f)
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
