using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TimeManager : MonoBehaviour
{
    public Text timerText;
    public float totalTime;
    private float remainingTime;
    public GameObject GameEndUI;

    // TaskUIManager 참조를 위한 변수 추가
    private TaskUIManager taskUIManager;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        remainingTime = totalTime;
        GameEndUI.SetActive(false);

        // TaskUIManager 찾기
        taskUIManager = FindObjectOfType<TaskUIManager>();
    }

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
            UpdateTimerDisplay();
        }
        else
        {
            remainingTime = 0;
            timerText.color = Color.red;
            ShowGameEndUI();
        }
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:D2}:{1:D2}", minutes, seconds);
    }

    void ShowGameEndUI()
    {
        GameEndUI.SetActive(true);
        StopPlayerMovement();
    }

    void StopPlayerMovement()
    {
        // 게임 종료 시 데이터 저장
        if (taskUIManager != null)
        {
            taskUIManager.EndGame();
            Debug.Log("Game data saved before ending.");
        }
        else
        {
            Debug.LogError("TaskUIManager not found!");
        }

        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            FirstPersonController controller = player.GetComponent<FirstPersonController>();
            if (controller != null)
            {
                controller.enabled = false;
                Debug.Log($"Stopped movement for player: {player.name}");
            }
        }
        StartCoroutine(LoadEndingSceneAfterDelay(2f));
    }

    private IEnumerator LoadEndingSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        EndingScene endingScene = FindObjectOfType<EndingScene>();
        if (endingScene != null)
        {
            endingScene.LoadEndingScene();
        }
        else
        {
            Debug.LogWarning("EndingScene script not found in the scene.");
        }
    }
}