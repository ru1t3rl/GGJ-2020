using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AmmoBoxCollidingScript : MonoBehaviour
{
    [SerializeField] private AudioClip PickupSFX;
    [SerializeField] private GameObject AmmoPickupAmount;
    private int FeedbackDuration = 120;
    private int FeedbackCounter;

    private void Start()
    {
        gameObject.SetActive(true);
        AmmoPickupAmount.SetActive(false);

        FeedbackCounter = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(PickupSFX);
            other.GetComponent<Player>().gun.Reload();

            Feedback();

            gameObject.SetActive(false);
        }
    }

    private void Feedback()
    {
        AmmoPickupAmount.SetActive(true);
        FeedbackCounter++;
        Debug.Log(FeedbackCounter);

        if (FeedbackCounter >= FeedbackDuration)
        {
            Debug.Log("Feedback Completed");
            AmmoPickupAmount.SetActive(false);
        }
    }
}
