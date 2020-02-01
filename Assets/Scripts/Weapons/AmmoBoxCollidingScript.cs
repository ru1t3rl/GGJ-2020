using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxCollidingScript : MonoBehaviour
{
    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("AMMMMOOOOOO");
            other.GetComponent<Player>().gun.Reload();
            gameObject.SetActive(false);
        }
    }
}
