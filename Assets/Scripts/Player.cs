﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    int score;
    float health;

    [SerializeField] TextMeshProUGUI ammo;
    [SerializeField] Gun gun;

    [SerializeField] TextMeshProUGUI scoreObject;

    [SerializeField] Image healthObjectRed;
    [SerializeField] Image healthObjectGreen;
    [SerializeField] TextMeshProUGUI healthText;

    void Update()
    {
        healthObjectRed.transform.localScale = new Vector3(healthObjectGreen.transform.localScale.x * (health/100), healthObjectRed.transform.localScale.y, healthObjectRed.transform.localScale.z);
        ammo.text = gun.Ammo.ToString();
    }

    public void DoDamage(float damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Debug.Log("Open nieuw scene");
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