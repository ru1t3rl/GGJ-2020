using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class SinusSweeper : MonoBehaviour
{
    [SerializeField] Vector3 velocity;
    [SerializeField] float speed;
    [SerializeField] float maxSpeed;

    [SerializeField] private float amplitude = 1f;
    [SerializeField] private float frequentie = 0.1f;

    float angle = 0.0f;
    [SerializeField] Vector3 offset;
    [SerializeField] GameObject target;

    [SerializeField] float range;
    [SerializeField] float explosionRange;
    [SerializeField] float damage;
    [SerializeField] VisualEffect explision;
    [SerializeField] float minExploDistance;
    bool exploding = false;

    [SerializeField] AudioClip deathSound;
    [SerializeField] bool exploded;
    float health = 100;

    [SerializeField] Material[] mats;
    Renderer rend;

    public int points;

    void Start()
    {
        target = GameManager.sPlayer;

        rend = GetComponent<Renderer>();
    }

    void OnEnable()
    {
        speed = maxSpeed;
    }

    void Update()
    {
        if (InRange(target.transform, range) && !exploding)
        {
            angle += frequentie;

            velocity += transform.right * (Mathf.Sin(angle) * amplitude) + offset;
            velocity += transform.forward;
            velocity = velocity.normalized * maxSpeed;
            transform.position += velocity;

            Quaternion backup = transform.rotation;
            transform.LookAt(target.transform);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, backup.eulerAngles.z);

            if (InRange(target.transform, minExploDistance) && !exploding)
            {
                rend.material = mats[1];
                exploding = true;
                StartCoroutine(Explode());
            }

        }

        if (health <= 0 && !exploding)
        {
            exploding = true;
            rend.material = mats[1];
            StartCoroutine(Explode2());
        }
    }

    public void DoDamage(float amount)
    {
        health -= amount;
    }

    IEnumerator Explode2()
    {
        GetComponent<Collider>().enabled = false;
        GetComponent<Renderer>().enabled = false;
        
        explision.Play();

        yield return new WaitForSeconds(1);

        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;

        exploding = false;
        gameObject.SetActive(false);
    }

    IEnumerator Explode()
    {
        exploded = true;
        yield return new WaitForSeconds(1);
        GetComponent<Collider>().isTrigger = true;
        GetComponent<Renderer>().enabled = false;
        GetComponent<SphereCollider>().radius = explosionRange;
        explision.Play();

        yield return new WaitForSeconds(1);
        GetComponent<Collider>().enabled = true;
        GetComponent<Renderer>().enabled = true;

        exploding = false;
        gameObject.SetActive(false);
    }

    bool InRange(Transform obj, float range)
    {
        float dX = transform.position.x - obj.transform.position.x;
        float dY = transform.position.y - obj.transform.position.y;

        return (Vector3.Distance(obj.transform.position, this.transform.position) < range);
    }

    public void OnTriggerEnter(Collider other)
    {
        Player pl = other.GetComponent<Player>();
        if (pl != null)
        {
            pl.DoDamage(damage);
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        Player pl = collision.collider.GetComponent<Player>();
        if (pl != null)
        {
            pl.DoDamage(damage);
        }
    }
}
