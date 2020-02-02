using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AmmoBoxCollidingScript : MonoBehaviour
{
    [SerializeField] AudioClip PickupSFX;
    [SerializeField] TextMeshProUGUI AmmoPickupAmount;
    private int FeedbackDuration = 120;
    private int FeedbackCounter;

    private bool looting;
    public float upTime;
    public float speed;

    Renderer rend;
    Collider col;

    [SerializeField] GameObject child;

    [SerializeField] private GameObject[] AmmoBox;

    private void Start()
    {
        rend = child.GetComponent<Renderer>();
        col = GetComponent<Collider>();

        gameObject.SetActive(true);
        AmmoPickupAmount.gameObject.SetActive(false);

        FeedbackCounter = 0;
        looting = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            child.SetActive(false);

            foreach (GameObject components in AmmoBox)
            {
                components.SetActive(false);
            }

            AudioManager.Instance.PlaySFX(PickupSFX);
            other.GetComponent<Player>().gun.Reload();

            if (!looting)
                StartCoroutine(FeedbackTimer(upTime));
            else
            {
                AmmoPickupAmount.transform.position += new Vector3(0, speed, 0);
            }            
        }
    }

    IEnumerator FeedbackTimer(float time)
    {
        looting = true;
        AmmoPickupAmount.gameObject.SetActive(true);
        yield return new WaitForSeconds(time);
        AmmoPickupAmount.gameObject.SetActive(false);
        looting = false;
        gameObject.SetActive(false);
    }
}
