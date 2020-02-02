using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Suicide_Bomber : Core_Enemy
{
    [SerializeField] float speed;

    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequentie = 0.1f;

    float angle = 0.0f;
    [SerializeField] Vector3 offset;
    [SerializeField] float explosionRad = 25;

    [SerializeField] float minDistanceTarget = 3f;
    bool exploding;

    Renderer rend;
    Collider col;

    public override void Awake()
    {
        base.Awake();
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    void OnEnable()
    {
        speed = maxSpeed;
    }

    public override void Update()
    {
        float distance = Vector3.Distance(gameObject.transform.position, _target.transform.position);
        if (distance < visionRange)
        {
            angle += frequentie;

            velocity += transform.right * (Mathf.Sin(angle) * amplitude) + offset;
            velocity += transform.forward;
            velocity = velocity.normalized * speed;
            transform.position += velocity;

            Quaternion backup = transform.rotation;
            transform.LookAt(_target.transform);
            transform.rotation = Quaternion.Euler(backup.eulerAngles.x, transform.rotation.eulerAngles.y, backup.eulerAngles.z);
        }

        float dX = _target.transform.position.x - transform.position.x;
        float dY = _target.transform.position.y - transform.position.y;

        if (dY * dY + dX * dX < minDistanceTarget * minDistanceTarget && !exploding)
        {
            speed = 0;
            col.enabled = false;
            rend.enabled = false;
            exploding = true;

            StartCoroutine(Explode());
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForSeconds(0.75f);
        if (InRange(_target))
        {
            _target.GetComponent<Player>().DoDamage(damageToTarget);
        }
        deathParticlees.Play();
        yield return new WaitForSeconds(0.5f);
        col.enabled = true;
        rend.enabled = true;
        exploding = false;
        gameObject.SetActive(false);
    }

    bool InRange(GameObject gobj)
    {
        return (Vector3.Distance(gobj.transform.position, this.transform.position) <= explosionRad);
    }
}
