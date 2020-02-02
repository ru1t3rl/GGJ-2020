using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    [SerializeField] float dissolveSpeed;
    bool dissolving;
    Renderer rend;

    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    void Update()
    {
        if (dissolving && rend.material.GetFloat("_DissolveValue") < 1)
        {
            rend.material.SetFloat("_DissolveValue", rend.material.GetFloat("_DissolveValue") + dissolveSpeed);
        }
        else if (rend.material.GetFloat("_DissolveValue") >= 1)
        {
            TurnOff();
        }
    }

    public void Dissolve()
    {
        dissolving = true;
    }

    public void TurnOff()
    {
        dissolving = false;
        rend.material.SetFloat("_DissolveValue", 0);
        gameObject.SetActive(false);
    }

    public void Activate()
    {
        dissolving = false;
        gameObject.SetActive(true);
    }
}
