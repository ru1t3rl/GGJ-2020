using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.VFX;

public class Bullet : MonoBehaviour
{
    float speed;
    [HideInInspector] public float force;
    [SerializeField] float drag;
    [SerializeField] float maxSpeed;
    [HideInInspector] public GameObject parent = null;

    [SerializeField] VisualEffect bulletTrail;
    [SerializeField] VisualEffect impact;

    [HideInInspector] public Vector3 position = Vector3.zero;
    [HideInInspector] public Vector3 direction = Vector3.zero;
    [HideInInspector] public Quaternion rotation = Quaternion.identity;
    [HideInInspector] public Vector3 endPoint = Vector3.zero;

    Renderer rend;
    Collider col;

    RaycastHit hit;

    bool disabling;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        col = GetComponent<Collider>();
    }

    public virtual void OnEnable()
    {
        this.transform.position = position;
        this.transform.rotation = rotation;
        impact.Stop();
        bulletTrail.Play();
        speed += force;
        disabling = false;
    }

    public virtual void FixedUpdate()
    {
        this.transform.position += direction.normalized * speed * Time.fixedDeltaTime;
        speed /= drag;

        if(speed <= 0.5f && !disabling)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet>() == null && !disabling)
        {
            speed = 0;

            disabling = true;

            col.enabled = false;
            rend.enabled = false;

            PlayImpact();
            StartCoroutine(Disable());
        }

        if (other.CompareTag("Enemy"))
        {
            if (other.GetComponent<Core_Enemy>() != null) {
                other.GetComponent<Core_Enemy>().TakeDamage(20);
            }
        }
    }

    IEnumerator Disable()
    {
        yield return new WaitForSeconds(0.3f);
        col.enabled = true;
        rend.enabled = true;
        gameObject.SetActive(false);
        disabling = false;
    }

    void PlayImpact()
    {
        bulletTrail.gameObject.SetActive(false);
        impact.Play();
    }
}
