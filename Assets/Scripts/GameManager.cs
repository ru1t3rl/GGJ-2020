using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameObject sPlayer;
    [SerializeField] GameObject player;

    static GameObject sCPU;
    [SerializeField] GameObject cPU;

    [SerializeField] List<Room> rooms = new List<Room>();
    int currentRoom = 0;

    bool allDead;

    void Awake()
    {
        sPlayer = player;
        sCPU = cPU;
    }

    void Update()
    {
        rooms[currentRoom].passed = allDead;
    }

    public static GameObject Player { get => sPlayer; }
    public static GameObject CPU { get => sCPU; }
}
