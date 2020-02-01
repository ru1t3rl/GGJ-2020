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

    [SerializeField] List<Room> rooms = new List<Room>();
    int currentRoom = 0;

    bool allDead;
    [SerializeField] List<GameObject> enemyTypes = new List<GameObject>();
    [SerializeField] List<int> enemyAmount = new List<int>();
    List<GameObject> enemies = new List<GameObject>();

    List<GameObject> activeEnemy = new List<GameObject>();

    [SerializeField] List<int> enemiesInWave = new List<int>();
    int currentWave = 0;

    void Awake()
    {
        sPlayer = player;
        sCPU = cPU;
        AudioManager.Instance.SetMusicVolume(0.05f);
        AudioManager.Instance.PlayMusic(GameMusic);

        for (int iType = 0; iType < enemyTypes.Count; iType++)
        {
            for (int iEnemy = 0; iEnemy < enemyAmount[iType]; iEnemy++)
            {
                enemies.Add(Instantiate(enemyTypes[iType]));
                enemies[enemies.Count - 1].SetActive(false);
            }
        }

        LoadEnemies();
    }

    void Update()
    {
        rooms[currentRoom].passed = allDead;
        doSomething();

        for(int iGobj = activeEnemy.Count; iGobj-- > 0;)
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
            currentWave++;
            LoadEnemies();
            allDead = false;
        }
    }

    void LoadEnemies()
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

    void PickEnemy()
    {
        int rIndex = Random.Range(0, enemies.Count);
        if (enemies[rIndex].activeSelf)
            PickEnemy();
        else
            activeEnemy.Add(enemies[rIndex]);
    }

    public static GameObject Player { get => sPlayer; }
    public static GameObject CPU { get => sCPU; }
}
