using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeControl : MonoBehaviour
{
    public float slowMotionFactor = 0.5f; // 放慢时间的因子
    private float originalTimeScale; // 原始时间缩放
    void Start()
    {
        originalTimeScale = Time.timeScale; // 保存原始的时间缩放
    }
    void Update()
    {
        // 检测 Q 键是否被按下
        if (Input.GetKeyDown(KeyCode.Q))
        {
            // 将时间缩放到慢速状态
            Time.timeScale = slowMotionFactor;
        }
        else if (Input.GetKeyUp(KeyCode.Q))
        {
            // 恢复到原始时间缩放
            Time.timeScale = originalTimeScale;
        }
    }
}
