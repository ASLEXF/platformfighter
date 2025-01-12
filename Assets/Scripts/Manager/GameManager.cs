using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.TextCore.Text;

public class GameManager : MonoBehaviour
{
    private static GameManager instance = null!;

    public static GameManager Instance
    { get { return instance; } }

    [Header("Players")]
    public List<string> characters = new List<string>();  // from UI
    [SerializeField] List<GameObject> characterPrefabs = new List<GameObject>();
    [SerializeField] GameObject playerPrefab = null;
    [SerializeField] List<GameObject> playerObjs = new List<GameObject>();

    [Header("UI")]
    public UIEnum UIEnum = UIEnum.Normal;

    Transform players = null!;
    Transform spawnPoints = null!;  // 5

    private void Awake()
    {
        if (instance is null)
            instance = this;
    }

    private void Start()
    {
#if !DEVELOPMENT_BUILD
        Application.logMessageReceived += (condition, stackTrace, type) => { };
#endif
        GameEvents.Instance.OnLevelExit += ClearAll;
    }

    public void ClearAll()
    {
        characters.Clear();
        characterPrefabs.Clear();
        playerObjs.Clear();
    }

    public void InitializeLevel()
    {
        Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        GameObject[] rootObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == "Players")
                players = obj.transform;
            else if (obj.name == "SpawnPoints")
                spawnPoints = obj.transform;
        }

        InitializePlayers();

        ItemManager.Instance.Initialize();
    }

    void InitializePlayers()
    {
        // prefab
        for (int i = 0; i < 2; i++)
        {
            characterPrefabs.Add(Resources.Load<GameObject>(characters[i]));
        }

        playerPrefab = Resources.Load<GameObject>("Prefabs/Player");

        Assert.IsTrue(characterPrefabs.Count == 2);

        // gameobject
        for (int i = 0; i < 2; i++)
        {
            GameObject playerObj = Instantiate(playerPrefab, spawnPoints.GetChild(i).position, spawnPoints.GetChild(i).rotation);
            playerObj.transform.SetParent(players);
            //playerObj.transform.SetSiblingIndex(i);
            playerObj.GetComponentInChildren<PlayerRespawn>().SpawnPoint = spawnPoints.GetChild(i);

            GameObject characterObj = Instantiate(characterPrefabs[i], playerObj.transform);
            characterObj.transform.SetSiblingIndex(0);

            playerObjs.Add(playerObj);

            PlayerController playerController = playerObj.transform.Find("ControlPoint").GetComponent<PlayerController>();
            playerController.Initialize(i + 1);
        }

        //playerObjs[0].GetComponentInChildren<PlayerInput>().actions = playerLInput;
        //playerObjs[1].GetComponentInChildren<PlayerInput>().actions = playerRInput;
    }
}
