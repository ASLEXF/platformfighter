# nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    { get { return instance; } }

    public List<string> characters = new List<string>();  // from UI
    [SerializeField] List<GameObject> characterPrefabs = new List<GameObject>();
    [SerializeField] List<GameObject> playerObjs = new List<GameObject>();

    Transform players;

    Transform spawnPoints;

    private void Awake()
    {
        instance = this;
        players = GameObject.Find("Players").transform;
        spawnPoints = GameObject.Find("SpawnPoints").transform;
    }

    private void Start()
    {
        InitializePlayers();
    }

    void InitializePlayers()
    {
        for (int i = 0; i < 2; i++)
        {
            characterPrefabs.Add(Resources.Load<GameObject>(characters[i]));
        }

        Assert.IsTrue(characterPrefabs.Count == 2);

        for (int i = 0; i < 2; i++)
        {
            GameObject playerObj = Instantiate(characterPrefabs[i], spawnPoints.GetChild(i).position, spawnPoints.GetChild(i).rotation);
            playerObj.transform.SetParent(players);
            playerObjs.Add(playerObj);
        }
    }

    public void PlayerRespawn()
    {

    }
}
