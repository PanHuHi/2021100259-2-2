using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TaskUIManager : MonoBehaviourPunCallbacks
{
    [Header("쓰레기 주운 갯수")]
    public Text circleText;
    public Text cylinderText;
    public Text squareText;
    public Text beerCanText;
    public Text petBottleText;
    public Text trashBagText;

    private int circleCount = 0;
    private int cylinderCount = 0;
    private int squareCount = 0;
    private int beerCanCount = 0;
    private int petBottleCount = 0;
    private int trashBagCount = 0;

    private int storedCircleCount = 0;
    private int storedCylinderCount = 0;
    private int storedSquareCount = 0;
    private int storedBeerCanCount = 0;
    private int storedPetBottleCount = 0;
    private int storedTrashBagCount = 0;

    private PlayerInfo[] playerInfos = new PlayerInfo[2];

    public override void OnJoinedRoom()
    {
        Start();
    }

    void Start()
    {
        int playerCount = PhotonNetwork.PlayerList.Length;
        playerInfos = new PlayerInfo[playerCount];

        for (int i = 0; i < playerCount; i++)
        {
            playerInfos[i] = new PlayerInfo(PhotonNetwork.PlayerList[i].NickName, 0);
        }

        UpdateUI();
    }

    public void StoreCircleCount()
    {
        storedCircleCount++;
        UpdateUI();
        ActivateUIIfNeeded();
    }

    public void StoreCylinderCount()
    {
        storedCylinderCount++;
        UpdateUI();
        ActivateUIIfNeeded();
    }

    public void StoreSquareCount()
    {
        storedSquareCount++;
        UpdateUI();
        ActivateUIIfNeeded();
    }

    public void StoreBeerCanCount()
    {
        storedBeerCanCount++;
        UpdateUI();
        ActivateUIIfNeeded();
    }

    public void StorePetBottleCount()
    {
        storedPetBottleCount++;
        UpdateUI();
        ActivateUIIfNeeded();
    }

    public void StoreTrashBagCount()
    {
        storedTrashBagCount++;
        UpdateUI();
        ActivateUIIfNeeded();
    }

    public void ConfirmCollection()
    {
        int playerIndex = PhotonNetwork.LocalPlayer.ActorNumber - 1;

        if (playerIndex < 0 || playerIndex >= playerInfos.Length)
        {
            Debug.LogError($"Invalid player index: {playerIndex}");
            return;
        }

        // 저장된 카운트의 총합을 계산
        int totalToAdd = (storedCircleCount + storedCylinderCount + storedSquareCount + storedBeerCanCount + storedPetBottleCount + storedTrashBagCount);

        // 플레이어의 총 쓰레기 개수 업데이트
        playerInfos[playerIndex].totalTrashCount += totalToAdd;

        // Photon Custom Properties 업데이트
        ExitGames.Client.Photon.Hashtable playerProperties = new ExitGames.Client.Photon.Hashtable
    {
        { "TotalTrashCount", playerInfos[playerIndex].totalTrashCount }
    };
        PhotonNetwork.LocalPlayer.SetCustomProperties(playerProperties);

        // 실제 카운트를 업데이트
        circleCount += storedCircleCount;
        cylinderCount += storedCylinderCount;
        squareCount += storedSquareCount;
        beerCanCount += storedBeerCanCount;
        petBottleCount += storedPetBottleCount;
        trashBagCount += storedTrashBagCount;

        // 저장된 카운트 초기화
        storedCircleCount = 0;
        storedCylinderCount = 0;
        storedSquareCount = 0;
        storedBeerCanCount = 0;
        storedPetBottleCount = 0;
        storedTrashBagCount = 0;

        // UI 업데이트
        UpdateUI(); // 여기에서 UI를 업데이트
    }


    public void UpdateUI()
    {
        if (circleText != null) circleText.text = $"{circleCount}/5";
        if (cylinderText != null) cylinderText.text = $"{cylinderCount}/10";
        if (squareText != null) squareText.text = $"{squareCount}/5";
        if (beerCanText != null) beerCanText.text = $"{beerCanCount}/10";
        if (petBottleText != null) petBottleText.text = $"{petBottleCount}/10";
        if (trashBagText != null) trashBagText.text = $"{trashBagCount}/10";

        foreach (var playerInfo in playerInfos)
        {
            Debug.Log($"Player Name: {playerInfo.playerName}, Total Trash Collected: {playerInfo.totalTrashCount}");
        }
    }

    private void ActivateUIIfNeeded()
    {
        // UI를 활성화하는 조건을 추가합니다.
        if (storedCircleCount > 0 || storedCylinderCount > 0 || storedSquareCount > 0 ||
            storedBeerCanCount > 0 || storedPetBottleCount > 0 || storedTrashBagCount > 0)
        {
            // UI 활성화 로직 (예: 특정 UI 요소 활성화)
            // 예를 들어, 어떤 UI 오브젝트를 활성화하려면:
            // someUIElement.SetActive(true);
            Debug.Log("UI 활성화: 쓰레기가 저장되었습니다.");
        }
    }

    public PlayerInfo[] GetPlayerInfos()
    {
        return playerInfos;
    }

    public void EndGame()
    {
        for (int i = 0; i < playerInfos.Length; i++)
        {
            PlayerPrefs.SetInt("Player" + i + "TrashCount", playerInfos[i].totalTrashCount);
            PlayerPrefs.SetString("Player" + i + "Name", playerInfos[i].playerName);
        }

        UnityEngine.SceneManagement.SceneManager.LoadScene("ResultScene");
    }
}
