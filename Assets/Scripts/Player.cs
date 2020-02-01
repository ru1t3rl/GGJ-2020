using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int score = 0;
    float health = 100;

    [SerializeField] TextMeshProUGUI ammo;
    public Gun gun;

    [SerializeField] TextMeshProUGUI scoreObject;

    [SerializeField] Image healthObjectRed;
    [SerializeField] Image healthObjectGreen;
    [SerializeField] TextMeshProUGUI healthText;

    void Update()
    {
        healthObjectGreen.transform.localScale = new Vector3(healthObjectRed.transform.localScale.x * (health / 100), healthObjectGreen.transform.localScale.y, healthObjectGreen.transform.localScale.z);
        healthText.text = health.ToString();
        ammo.text = gun.Ammo.ToString();
    }

    public void DoDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(0);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        string iets = score.ToString();
        int d = scoreObject.text.Length - iets.Length;
        for (int i = 0; i < d; i++)
        {
            iets = "0" + iets;
        }
        scoreObject.text = iets;
    }
}
