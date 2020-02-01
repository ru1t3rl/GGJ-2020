using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public bool passed;
    [SerializeField] GameObject[] door;

    void Update()
    {
        if (passed)
        {
            door[0].SetActive(true);
            door[1].SetActive(false);
        }
    }
}
