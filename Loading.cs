using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;
public class Loading : MonoBehaviourPunCallbacks
{
    public GameObject loadingUI; // 로딩 화면 UI
    public Slider progressBar;   // 로딩 진행 바
    public float loadingTime = 2f; // 슬라이더가 증가하는 데 걸릴 시간 (초)

    // 버튼 클릭 시 호출
    public void StartLoadingScene(string sceneName)
    {
        if (loadingUI != null)
            loadingUI.SetActive(true); // 로딩 UI 활성화

        PhotonNetwork.LeaveRoom();
        
        StartCoroutine(LoadSceneWithProgress(sceneName)); // 로딩 코루틴 실행
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("룸을 나가고 마스터 서버와의 연결을 끊었습니다.");
        
    }
    private IEnumerator LoadSceneWithProgress(string sceneName)
    {
        float elapsedTime = 0f; // 경과 시간
        progressBar.value = 0f; // 슬라이더 초기화

        // 2초 동안 슬라이더 증가
        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / loadingTime); // 슬라이더 값 증가
            yield return null; // 다음 프레임까지 대기
        }

        // 슬라이더가 가득 찬 후 씬 전환
        SceneManager.LoadScene(sceneName);
    }
}