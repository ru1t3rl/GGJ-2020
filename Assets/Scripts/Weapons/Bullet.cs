using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Bullet : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float force;
    [SerializeField] float drag;
    [SerializeField] float maxSpeed;
    [HideInInspector] public GameObject parent = null;

    [SerializeField] VisualEffect bulletTrail;
    [SerializeField] VisualEffect impact;

    public Vector3 position = Vector3.zero;
    public Vector3 direction = Vector3.zero;
    public Quaternion rotation = Quaternion.identity;

    public virtual void OnEnable()
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        bulletTrail.Play();
        speed += force;
    }

    public virtual void Update()
    {
        this.transform.position += direction.normalized * speed * Time.deltaTime;
        speed /= drag;
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        bulletTrail.Stop();
        impact.Play();
    }
}
