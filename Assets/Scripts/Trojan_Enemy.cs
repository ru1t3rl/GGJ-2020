using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trojan_Enemy : Core_Enemy
{

    private void Awake()
    {
        SetMaxHealth(50f);    
    }

    // Start is called before the first frame update
    void Start()
    {
        maxSpeed = 5f;
        visionRange = 100f;

        health = GetMaxhealth();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Virus has: " + health + " Health left");

        MoveObject();

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(10f);
        }
    }

    public override void MoveObject()
    {
        base.MoveObject();
    }

    public override void TrackTarget(GameObject _object)
    {
        base.TrackTarget(_object);
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
    }

    public override void Die()
    {
        base.Die();
    }
}
