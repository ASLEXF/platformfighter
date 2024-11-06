using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.ShaderGraph.Internal.KeywordDependentCollection;

public class MultiPlayerVCam : MonoBehaviour
{
    CinemachineVirtualCamera vcam;
    Transform originalTransform;
    Transform players;

    [SerializeField] float speed = 1.0f;
    [SerializeField] float minDistance = 20.0f;
    [SerializeField] float maxDistance = 33.9f;

    private void Awake()
    {
        vcam = GetComponent<CinemachineVirtualCamera>();
        originalTransform = transform;
        players = GameObject.Find("Players").transform;
    }

    void Start()
    {
        transform.rotation = Quaternion.Euler(45, 0, 0);
    }

    private void LateUpdate()
    {
        if (players.childCount == 0) return;

        Vector3 averagePosition = Vector3.zero;
        List<float> positionX = new List<float>();
        for (int i = 0; i < players.childCount; i++)
        {
            averagePosition += players.GetChild(i).GetChild(0).transform.position;
            positionX.Add(transform.TransformPoint(players.GetChild(i).GetChild(0).transform.position).x);
        }
        averagePosition /= players.childCount;

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
}
