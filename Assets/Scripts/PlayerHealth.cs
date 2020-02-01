using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [Header("General")]
    [SerializeField]
    private float maxHealth = 100f;
    [HideInInspector] public float health;

    public float damageAmount = 20f;

    [Header("Brightness")]
    float healthLight;
    public Material ElectronMaterial;

    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            health -= damageAmount;
        }

        if (health <= 0f)
        {

        }
    }
}
