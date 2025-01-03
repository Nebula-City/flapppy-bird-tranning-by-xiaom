﻿using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Pipes prefab;
    public float spawnRate = 1f;      // 初始生成速度
    public float minHeight = -1f;    // 管道最低位置
    public float maxHeight = 2f;     // 管道最高位置
    public float verticalGap = 3f;   // 初始管道間距

    private float elapsedTime = 0f;  // 累計時間
    public float difficultyIncreaseRate = 10f; // 每 10 秒提高一次難度

    public void OnEnable()
    {
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);
    }

    public void OnDisable()
    {
        CancelInvoke(nameof(Spawn));
    }

    public void Update()
    {
        // 計算累計時間
        elapsedTime += Time.deltaTime;

        // 每隔指定時間提高難度
        if (elapsedTime >= difficultyIncreaseRate)
        {
            IncreaseDifficulty();
            elapsedTime = 0f; // 重置計時
        }
    }

    public void Spawn()
    {
        Pipes pipes = Instantiate(prefab, transform.position, Quaternion.identity);
        pipes.transform.position += Vector3.up * Random.Range(minHeight, maxHeight);
        pipes.gap = verticalGap;
    }

    public void IncreaseDifficulty()
    {
        // 縮短生成間隔，最小限制為 0.8 秒
        spawnRate = Mathf.Max(0.8f, spawnRate - 0.05f);
        CancelInvoke(nameof(Spawn));
        InvokeRepeating(nameof(Spawn), spawnRate, spawnRate);

        // 減少管道間距，最小限制為 2.5
        verticalGap = Mathf.Max(2.5f, verticalGap - 0.1f);

        Debug.Log($"Difficulty increased: spawnRate={spawnRate}, verticalGap={verticalGap}");
    }
}
