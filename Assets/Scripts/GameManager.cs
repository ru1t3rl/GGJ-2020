using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameObject sPlayer;
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
    [SerializeField] GameObject warnObject;

    public static Player pl;

    void Awake()
    {
        sPlayer = player;
        pl = player.GetComponent<Player>();
        sCPU = cPU;
        AudioManager.Instance.PlayMusic(GameMusic);
        AudioManager.Instance.SetMusicVolume(gameMusicLoudness);

        for (int iType = 0; iType < enemyTypes.Count; iType++)
        {
            for (int iEnemy = 0; iEnemy < enemyAmount[iType]; iEnemy++)
            {
                enemies.Add(Instantiate(enemyTypes[iType]));
                enemies[enemies.Count - 1].SetActive(false);
                enemies[enemies.Count - 1].hideFlags = HideFlags.HideInHierarchy;
            }
        }

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        LoadEnemies();
    }

    void Update()
    {
        for (int iGobj = activeEnemy.Count; iGobj-- > 0;)
        {
            if (!activeEnemy[iGobj].activeSelf)
                activeEnemy.RemoveAt(iGobj);
        }

        if (activeEnemy.Count <= 0)
            allDead = true;

        doSomething();
    }

    void doSomething()
    {
        if (allDead)
        {        
            // Open the new Room
            rooms[currentRoom].DissolveDoor(0);

            if (currentRoom < rooms.Count - 1)
                currentRoom++;
            else
                currentRoom = 0;

            rooms[currentRoom].TurnOff(1);

            // Close a hall way
            int prev = currentRoom - 2;
            if (prev < 0)
                prev = rooms.Count + prev;
            rooms[prev].Activate(0);
            rooms[prev].Activate(1);
            

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
        warnObject.SetActive(true);
        this.warning.text = warning;

        try { StopCoroutine(warn); } catch (System.NullReferenceException) { }
        warn = StartCoroutine(Warn());
    }

    public IEnumerator Warn()
    {
        yield return new WaitForSeconds(warningDuration);
        warnObject.gameObject.SetActive(false);
    }

    public static GameObject Player { get => sPlayer; }
    public static GameObject CPU { get => sCPU; }
}
