using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameObject sPlayer;
    [SerializeField] GameObject player;

    static GameObject sCPU;
    [SerializeField] GameObject cPU;

    private void Awake()
    {
        sPlayer = player;
        sCPU = cPU;
    }

    public static GameObject Player { get => sPlayer; }
    public static GameObject CPU { get => sCPU; }
}
