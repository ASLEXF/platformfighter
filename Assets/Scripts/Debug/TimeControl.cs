using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float slowMotionFactor = 0.5f; // ����ʱ�������
    private float originalTimeScale; // ԭʼʱ������
    void Start()
    {
        originalTimeScale = Time.timeScale; // ����ԭʼ��ʱ������
    }
    void Update()
    {
        // ��� Q ���Ƿ񱻰���
        if (Input.GetKey(KeyCode.Q))
        {
            // ��ʱ�����ŵ�����״̬
            Time.timeScale = slowMotionFactor;
        }
        else
        {
            // �ָ���ԭʼʱ������
            Time.timeScale = originalTimeScale;
        }
    }
}
