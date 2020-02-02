using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public GameObject[] doors;
    
    public void DissolveDoor(int i)
    {
        doors[i].GetComponent<Wall>().Dissolve();
    }

    public void Activate(int i)
    {
        doors[i].GetComponent<Wall>().Activate();
    }

    public void TurnOff(int i)
    {
        doors[i].GetComponent<Wall>().TurnOff();
    }
}
