using UnityEngine;
using UnityEngine.UI;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Player player;
    [SerializeField] private Spawner spawner;
    [SerializeField] private Text scoreText;
    [SerializeField] private GameObject playButton;
    [SerializeField] private GameObject gameOver;
    [SerializeField] private GameObject getReady;
    [SerializeField] private Text deathReasonText; // 在 Unity 編輯器中指定
    private string deathReason = "未知原因";
    public int score { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // 使用 Destroy 替代 DestroyImmediate
            return;
        }

        Instance = this;
    }

    public void SetDeathReason(string reason)
    {
        deathReason = reason;
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause();
        deathReasonText.gameObject.SetActive(false);
        getReady.SetActive(true);
        HideCursor(); // 隱藏鼠標指針
    }

    public void Pause()
    {

        Time.timeScale = 0f;
        player.enabled = false;
    }

    public void Play()
    {
        HideCursor(); // 開始遊戲時隱藏鼠標
        score = 0;
        scoreText.text = score.ToString();
        deathReasonText.gameObject.SetActive(false);
        playButton.SetActive(false);
        gameOver.SetActive(false);
        getReady.SetActive(false);
        Time.timeScale = 1f;
        player.enabled = true;
        
         new-branch
        Pipes[] pipes = Object.FindObjectsByType<Pipes>(FindObjectsSortMode.None);
        for (int i = 0; i < pipes.Length; i++)
        {
            Destroy(pipes[i].gameObject);
        }

        main
    }


    public void GameOver()
    {
        playButton.SetActive(true);
        gameOver.SetActive(true);
        deathReasonText.text = $"死亡原因:\n{deathReason}";
        deathReasonText.gameObject.SetActive(true);
        ShowCursor(); // 顯示鼠標指針
        Pause();
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
        // 每 10 分提高一次難度
        if (score % 10 == 0)
        {
            // 找到場景中所有的 Spawner
            Spawner[] spawners = Object.FindObjectsByType<Spawner>(FindObjectsSortMode.None);
            if (spawners.Length > 0)
            {
                foreach (Spawner spawner in spawners)
                {
                    spawner.IncreaseDifficulty(); // 提高難度
                }
            }
            else
            {
                Debug.LogWarning("No Spawners found in the scene!");
            }

            // 增加 Pipes 的速度
            Pipes.IncreaseSpeed();
        }
    }



    private void HideCursor()
    {
        Cursor.visible = false; // 隱藏鼠標指針
        Cursor.lockState = CursorLockMode.Confined; // 限制鼠標在遊戲窗口
    }

    private void ShowCursor()
    {
        Cursor.visible = true; // 顯示鼠標指針
        Cursor.lockState = CursorLockMode.None; // 解除鼠標限制
    }
}
