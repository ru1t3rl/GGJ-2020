using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Trojan_Enemy : Core_Enemy
{

    [SerializeField] private GameObject trojans;
    private Vector3 spawnTrojanOffset;

    private void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();

        deathParticlees.Stop();
        _target = GameManager.CPU;
        SetMaxHealth(50f);   
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnTrojanOffset = new Vector3(1, 0, 0);
        maxSpeed = 5f;
        visionRange = 100f;
        health = GetMaxhealth();
    }

    // Update is called once per frame
    void Update()
    {
        MoveObject();
        GoalReached();

        if (Input.GetKeyDown(KeyCode.A))
        {
            TakeDamage(10f);
        }
    }

    public override void MoveObject()
    {
        base.MoveObject();
    }

    public override void TakeDamage(float _damage)
    {
        base.TakeDamage(_damage);
    }

    public void GoalReached()
    {
        if (_target != null)
        {
            float distance = Vector3.Distance(gameObject.transform.position, _target.transform.position);

            if (distance <= 1.5f)
            {
                gameObject.SetActive(false);
            }
        }
    }

    public override void Die()
    {
        base.Die();

        Instantiate(trojans, gameObject.transform.localPosition + spawnTrojanOffset, Quaternion.identity);
        Instantiate(trojans, gameObject.transform.localPosition + (spawnTrojanOffset * -1), Quaternion.identity);
    }
}
