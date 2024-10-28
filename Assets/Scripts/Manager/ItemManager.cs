# nullable enable

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem.HID;

public class ItemManager : MonoBehaviour
{
    [SerializeField] GameObject platform;
    [SerializeField] List<GameObject> prefabs = new List<GameObject>();
    [SerializeField] float height = 5.0f;
    [SerializeField] string prefabPath = "Assets/Resources/Prefabs/Items";

    private void Awake()
    {
        prefabs = new List<GameObject>(Resources.LoadAll<GameObject>(prefabPath));
    }

    public async Task GenerateItems(int number)
    {
        Assert.IsTrue(prefabs.Count > 0);
        List<Task> tasks = new List<Task>();
        for (int i = 0; i < number; i++)
        {
            int index = Random.Range(0, prefabs.Count);
            Vector3 position = getRandomPointOnPlatform();
            tasks.Add(generateItem(prefabs[i], position));
        }
        await Task.WhenAll(tasks);
    }

    private Vector3 getRandomPointOnPlatform()
    {
        Bounds bounds = platform.GetComponent<Renderer>().bounds;
        float randomX = Random.Range(bounds.min.x, bounds.max.x);
        float randomZ = Random.Range(bounds.min.z, bounds.max.z);

        RaycastHit hit;
        Vector3 randomPoint = new Vector3(randomX, bounds.max.y + height, randomZ);
        while (Physics.Raycast(randomPoint, Vector3.down, out hit) != true)
        {
            randomPoint = new Vector3(randomX, bounds.max.y + height, randomZ);
        }

        return hit.point;
    }

    private async Task<GameObject> generateItem(GameObject prefab, Vector3 position)
    {
        return await Task.Run(() =>
        {
            return Instantiate(prefab, position, Quaternion.Euler(0, 0, 0));
        });
    }
}
