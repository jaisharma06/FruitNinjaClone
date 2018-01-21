using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ApplicationManager : MonoBehaviour
{
    public static ApplicationManager instance;
    [SerializeField]
    private Blade blade;
    [SerializeField]
    private CameraShake cameraShake;
    [SerializeField]
    private Text scoreText;
    [SerializeField]
    private Text livesText;
    [SerializeField]
    private Text timerText;
    [SerializeField]
    private GameObject gameoverUI;
    [SerializeField]
    private Text finalScoreText;

    private ApplicationData applicationData;

    [SerializeField]
    private FruitSpawner fruitSpawner;

    public int playerScore { get; set; }
    private int livesLeft { get; set; }
    private int timeLeft { get; set; }

    private Coroutine StartTimeCoroutine;

    private void OnEnable()
    {
        if (!instance)
            instance = this;
    }

    private void Start()
    {
        InitializeGame();
    }

    private void OnDisable()
    {
        instance = null;
    }

    public void InitializeGame()
    {
        GetApplicationData();
        livesLeft = applicationData.lives;
        livesText.text = "Lives " + livesLeft;
        playerScore = 0;
        SetScore(0);
        ActivateGameOverUI(false);
        timeLeft = applicationData.timer;
        SetTime(timeLeft);
        StartTimeCoroutine = StartCoroutine(StartTime());
        blade.StartCutting();
    }

    public void SetScore(int value)
    {
        if (value >= 0)
        {
            playerScore += value;
            scoreText.text = "Score " + playerScore;
        }
        else
        {
            DeduceLive();
        }
    }

    public void RestartGame()
    {
        InitializeGame();
    }

    private void DeduceLive()
    {
        livesLeft--;
        livesLeft = (livesLeft < 0) ? 0 : livesLeft;
        livesText.text = "Lives " + livesLeft;
        cameraShake.ShakeCamera(2, 0.003f);
        if (livesLeft <= 0)
        {
            ActivateGameOverUI();
        }
    }

    private void ActivateGameOverUI(bool flag = true)
    {
        gameoverUI.SetActive(flag);
        if (flag)
        {
            fruitSpawner.StopSpawning();
            finalScoreText.text = playerScore.ToString();
            if (StartTimeCoroutine != null)
            {
                StopCoroutine(StartTimeCoroutine);
                blade.StopCutting();
            }
        }
        else
        {
            fruitSpawner.StartSpawning();
        }
    }

    private IEnumerator StartTime()
    {
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(1f);
            timeLeft--;
            SetTime(timeLeft);
        }

        ActivateGameOverUI();
    }

    private void SetTime(int time)
    {
        timerText.text = "Time " + timeLeft;
    }

    private void GetApplicationData()
    {
        var dataFile = Resources.Load(Statics.APP_DATA_FILE_NAME) as TextAsset;
        var data = dataFile.text;
        applicationData = JsonManager.GetData<ApplicationData>(data);
    }
}
