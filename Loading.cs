using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using Photon.Pun;
public class Loading : MonoBehaviourPunCallbacks
{
    public GameObject loadingUI; // �ε� ȭ�� UI
    public Slider progressBar;   // �ε� ���� ��
    public float loadingTime = 2f; // �����̴��� �����ϴ� �� �ɸ� �ð� (��)

    // ��ư Ŭ�� �� ȣ��
    public void StartLoadingScene(string sceneName)
    {
        if (loadingUI != null)
            loadingUI.SetActive(true); // �ε� UI Ȱ��ȭ

        PhotonNetwork.LeaveRoom();
        
        StartCoroutine(LoadSceneWithProgress(sceneName)); // �ε� �ڷ�ƾ ����
    }
    public override void OnLeftRoom()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("���� ������ ������ �������� ������ �������ϴ�.");
        
    }
    private IEnumerator LoadSceneWithProgress(string sceneName)
    {
        float elapsedTime = 0f; // ��� �ð�
        progressBar.value = 0f; // �����̴� �ʱ�ȭ

        // 2�� ���� �����̴� ����
        while (elapsedTime < loadingTime)
        {
            elapsedTime += Time.deltaTime;
            progressBar.value = Mathf.Clamp01(elapsedTime / loadingTime); // �����̴� �� ����
            yield return null; // ���� �����ӱ��� ���
        }

        // �����̴��� ���� �� �� �� ��ȯ
        SceneManager.LoadScene(sceneName);
    }
}