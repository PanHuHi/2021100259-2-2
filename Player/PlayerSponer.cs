using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
public class PlayerSponer : MonoBehaviourPunCallbacks
{
    public GameObject playerPrefab; // 플레이어 프리팹
    public Vector3[] spawnPositions; // 스폰 위치 배열
    void Awake()
    {
        spawnPositions = new Vector3[]{new Vector3(-1, 2, 0),new Vector3(-1, 2, -1)};
    }
    void Start()
    {
        SpawnPlayers();
    }

    void SpawnPlayers()
    {
        if (spawnPositions == null || spawnPositions.Length == 0)
        {
            Debug.LogError("Spawn positions are not set or empty.");
            return;
        }

        if (playerPrefab == null)
        {
            Debug.LogError("Player prefab is not assigned.");
            return;
        }

        if (PhotonNetwork.InRoom)
        {
            int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;
            Debug.Log("Current player count: " + playerCount);
            if (playerCount > 0 && playerCount <= spawnPositions.Length)
            {
                Vector3 spawnPosition = spawnPositions[playerCount - 1]; // 플레이어 수에 따라 스폰 위치 선택
                PhotonNetwork.Instantiate("Player", spawnPosition, Quaternion.identity);
            }
            else
            {
                Debug.LogError("Spawn position is null.");
            }
        }
        else
        {
            Debug.LogError("Not in a room.");
        }
    }
}