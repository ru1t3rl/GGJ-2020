using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBoxCollidingScript : MonoBehaviour
{
    [SerializeField] private AudioClip PickupSFX;

    private void Start()
    {
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        { 
            AudioManager.Instance.PlaySFX(PickupSFX);
            other.GetComponent<Player>().gun.Reload();
            gameObject.SetActive(false);
        }
    }
}
