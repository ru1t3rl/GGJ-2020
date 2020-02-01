using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    static GameObject sPlayer;
    [SerializeField] GameObject player;

    static GameObject sCPU;
    [SerializeField] GameObject cPU;

    [SerializeField] private float gameMusicLoudness = 0.01f;
    [SerializeField] private AudioClip GameMusic;
    [SerializeField] private AudioClip DoorOpeningSFX;

    [SerializeField] List<Room> rooms = new List<Room>();
    int currentRoom = 0;

    bool allDead;
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField] List<int> enemyAmount = new List<int>();
    List<GameObject> enemies = new List<GameObject>();

    List<GameObject> activeEnemy = new List<GameObject>();

    [SerializeField] List<int> enemiesInWave = new List<int>();
    int currentWave = 0;

    [SerializeField] TextMeshProUGUI warning;
    Coroutine warn;
    [SerializeField] float warningDuration;

    void Awake()
    {
        sPlayer = player;
        sCPU = cPU;
        AudioManager.Instance.PlayMusic(GameMusic);
        AudioManager.Instance.SetMusicVolume(gameMusicLoudness);

        for (int iType = 0; iType < enemyTypes.Count; iType++)
        {
            for (int iEnemy = 0; iEnemy < enemyAmount[iType]; iEnemy++)
            {
                enemies.Add(Instantiate(enemyTypes[iType]));
                enemies[enemies.Count - 1].SetActive(false);
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        LoadEnemies();
    }

    void Update()
    {
        rooms[currentRoom].passed = allDead;
        doSomething();

        for (int iGobj = activeEnemy.Count; iGobj-- > 0;)
        {
            if (!activeEnemy[iGobj].activeSelf)
                activeEnemy.RemoveAt(iGobj);
        }

        if (activeEnemy.Count <= 0)
            allDead = true;
    }

    void doSomething()
    {
        if (allDead)
        {
            currentRoom = (currentRoom < rooms.Count - 1) ? currentRoom + 1 : 0;
            int prevRoom = currentRoom - 2;
            if (prevRoom < 0)
                prevRoom = prevRoom + (rooms.Count - 1);

            rooms[prevRoom].door[0].SetActive(true);
            rooms[prevRoom].door[1].SetActive(true);

            AudioManager.Instance.PlaySFX(DoorOpeningSFX, 2);

            currentWave++;
            LoadEnemies();
            allDead = false;
        }
    }

    void LoadEnemies()
    {
        if (currentWave < enemiesInWave.Count)
        {
            for (int iEnemy = 0; iEnemy < enemiesInWave[currentWave]; iEnemy++)
            {
                PickEnemy();
                BoxCollider collider = rooms[currentRoom].GetComponent<BoxCollider>();
                activeEnemy[activeEnemy.Count - 1].transform.position = new Vector3(Random.Range(collider.transform.position.x - collider.size.x / 2, collider.transform.position.x + collider.size.x / 2),
                                                                                    Random.Range(collider.transform.position.y - collider.size.y / 2, collider.transform.position.y + collider.size.y / 2),
                                                                                    Random.Range(collider.transform.position.z - collider.size.z / 2, collider.transform.position.z + collider.size.z / 2));
                activeEnemy[activeEnemy.Count - 1].SetActive(true);
            }
        }
    }

    void PickEnemy()
    {
        int rIndex = Random.Range(0, enemies.Count);
        if (enemies[rIndex].activeSelf)
            PickEnemy();
        else
            activeEnemy.Add(enemies[rIndex]);
    }

    public void ShowWarning(string warning)
    {
        this.warning.gameObject.SetActive(true);
        this.warning.text = warning;

        try { StopCoroutine(warn); } catch (System.NullReferenceException) { }
        warn = StartCoroutine(Warn());
    }

    public IEnumerator Warn()
    {
        yield return new WaitForSeconds(warningDuration);
        warning.gameObject.SetActive(false);
    }

    public static GameObject Player { get => sPlayer; }
    public static GameObject CPU { get => sCPU; }
}
