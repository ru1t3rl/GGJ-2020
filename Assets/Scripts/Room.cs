using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    [HideInInspector] public bool passed;
    [SerializeField] GameObject[] door;
    [SerializeField] float dissolveSpeed;
    bool dissolving;
    Renderer rend;

    void Start()
    {
        rend = door[1].GetComponent<Renderer>();
    }

    void Update()
    {
        if (passed)
        {
            dissolving = true;
        }
        else
        {
            door[0].SetActive(false);
            door[1].SetActive(true);
        }

        if (dissolving)
        {
            Dissolve();
            if(rend.material.GetFloat("_DissolveValue") >= 1)
            {
                dissolving = false;
                rend.material.SetFloat("_DissolveValue", 0);
                door[0].SetActive(true);
                door[1].SetActive(false);
            }
        }
    }

    void Dissolve()
    {
        rend.material.SetFloat("_DissolveValue", rend.material.GetFloat("_DissolveValue") + dissolveSpeed);
    }
}
