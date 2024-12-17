using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MultiPlayerVCam : MonoBehaviour
{
    private static MultiPlayerVCam instance;

    public static MultiPlayerVCam Instance
    {
        get { return instance; }
    }

    CinemachineVirtualCamera vcam;
    Transform originalTransform;
    Transform players;

    [SerializeField] List<GameObject> targets = new List<GameObject>();

    [SerializeField] float speed = 1.0f;
    [SerializeField] float focusSpeed = 5.0f;
    [SerializeField] float minDistance = 20.0f;
    [SerializeField] float maxDistance = 33.9f;

    private void Awake()
    {
        if (instance == null)
            instance = this;

        vcam = GetComponent<CinemachineVirtualCamera>();
        originalTransform = transform;
        players = GameObject.Find("Players").transform;
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(45, 0, 0);

        for (int i = 0; i < players.childCount; i++)
        {
            targets.Add(players.GetChild(i).gameObject);
        }
    }

    private void LateUpdate()
    {
        if (targets.Count == 0) return;

        Vector3 averagePosition = Vector3.zero;
        List<float> positionX = new List<float>();
        foreach(GameObject obj in targets)
        {
            averagePosition += obj.transform.position;
            positionX.Add(transform.TransformPoint(obj.transform.position).x);
        }
        averagePosition /= targets.Count;

        float distance = minDistance + getMaxDistance(positionX) / 68 * (maxDistance - minDistance);
        Vector3 targetPosition = averagePosition + new Vector3(0, distance / Mathf.Cos(Mathf.PI / 4), -distance / Mathf.Cos(Mathf.PI / 4));

        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    float getMaxDistance(List<float> input)
    {
        float min = Mathf.Infinity, max = -min;
        foreach (float item in input)
        {
            if (item > max) max = item;
            if (item < min) min = item;
        }
        return max - min;
    }

    public void FocusOn(int playerId)
    {
        speed = focusSpeed;
        minDistance = 8.5f;

        targets.RemoveAt(playerId - 1);
    }
}
