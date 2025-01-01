using UnityEngine;

public class Pipes : MonoBehaviour
{
    public Transform top;
    public Transform bottom;
    public float speed = 5f; // 初始速度
    public float gap = 3f;   // 初始間距

    private float leftEdge;
    private static float speedMultiplier = 1f; // 全局速度倍數

    public void Start()
    {
        leftEdge = Camera.main.ScreenToWorldPoint(Vector3.zero).x - 1f;
        top.position += Vector3.up * gap / 2;
        bottom.position += Vector3.down * gap / 2;
    }

    public void Update()
    {
        transform.position += speed * speedMultiplier * Time.deltaTime * Vector3.left;

        if (transform.position.x < leftEdge)
        {
            Destroy(gameObject);
        }
    }

    // 公共方法，用於調整全局速度
    public static void IncreaseSpeed()
    {
        speedMultiplier += 0.1f; // 每次難度提升增加速度倍數
        Debug.Log($"Speed increased: speedMultiplier={speedMultiplier}");
    }
}
