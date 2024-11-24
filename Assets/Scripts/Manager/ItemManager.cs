# nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;

public class ItemManager : MonoBehaviour, IItemManager
{
    private static ItemManager instance = null!;

    public static ItemManager Instance
    {
        get { return instance; }
    }

    [SerializeField] GameObject platform = null!;
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] float height = 20.0f;
    [SerializeField] string prefabPath = "Prefabs/Items";
    [SerializeField] int number = 5;

    private void Awake()
    {
        if (instance is null)
            instance = this;

        prefabs = new List<GameObject>(Resources.LoadAll<GameObject>(prefabPath));
    }

    public void Initialize()
    {
        Scene scene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        GameObject[] rootObjects = scene.GetRootGameObjects();
        foreach (GameObject obj in rootObjects)
        {
            if (obj.name == "Environment")
                platform = obj.transform.Find("Platform").gameObject;
        }

        GenerateItems(number);
    }

    public void GenerateItems(int number)
    {
        Assert.IsTrue(prefabs.Count > 0);
        //List<Task> tasks = new List<Task>();
        for (int i = 0; i < number; i++)
        {
            int index = Random.Range(0, prefabs.Count);
            Vector3 position = getRandomPointOnPlatform();
            //tasks.Add(generateItem(prefabs[i], position));
            generateItem(prefabs[i], position);
        }
        //Task.WhenAll(tasks);
    }

    private Vector3 getRandomPointOnPlatform()
    {
        Bounds bounds = platform.GetComponent<Renderer>().bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        //RaycastHit hit;
        Vector3 randomPoint = new Vector3(randomX, bounds.max.y + height, randomZ);
        Debug.DrawRay(randomPoint, Vector3.down * height, UnityEngine.Color.red, 3.0f);
        //Physics.Raycast(randomPoint, Vector3.down, out hit, 100f);

        return randomPoint;
    }

    private void generateItem(GameObject prefab, Vector3 position)
    {
        Instantiate(prefab, position, Quaternion.Euler(0, 0, 0));
        //return await Task.Run(() =>
        //{
        //    return Instantiate(prefab, position, Quaternion.Euler(0, 0, 0));
        //});
    }

    public void Generate()
    {
        GenerateItems(number);
    }
}
