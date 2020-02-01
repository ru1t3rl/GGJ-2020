using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameObject sPlayer;
    [SerializeField] GameObject player;

    static GameObject sCPU;
    [SerializeField] GameObject cPU;

    [SerializeField] private AudioClip GameMusic;

    private void Awake()
    {
        sPlayer = player;
        sCPU = cPU;
        AudioManager.Instance.SetMusicVolume(0.05f);
        AudioManager.Instance.PlayMusic(GameMusic);
    }

    public static GameObject Player { get => sPlayer; }
    public static GameObject CPU { get => sCPU; }
}
