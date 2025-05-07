using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerCamera : MonoBehaviour
{
    public Camera playerCamera;  // 플레이어 카메라를 인스펙터에서 할당
    public float mouseSensitivity = 2f;  // 마우스 감도
    public Transform player;
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    void Start()
    {
        // playerCamera가 null이면 Camera.main으로 할당 시도
        if (playerCamera == null)
        {
            playerCamera = Camera.main;
        }
    }

    void Update()
    {
        // 마우스 입력 받기
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        // X축 회전 (좌우 회전)
        horizontalRotation += mouseX;
        transform.localRotation = Quaternion.Euler(0f, horizontalRotation, 0f);

        // Y축 회전 (상하 회전)
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // 카메라의 상하 회전 적용
        if (playerCamera != null)
        {
            playerCamera.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
        }
    }
}