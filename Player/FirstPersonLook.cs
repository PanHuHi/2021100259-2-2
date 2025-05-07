using UnityEngine;
using Photon.Pun;

public class FirstPersonLook : MonoBehaviourPunCallbacks
{
    [SerializeField]
    public Transform character; // 캐릭터의 몸체를 가리킴 (Y축 회전용)
    public float sensitivity = 2; // 마우스 감도
    public float smoothing = 1.5f; // 마우스 스무딩
    public Camera playerCamera; // 플레이어 카메라

    [Header("상태창 버튼")]
    public GameObject panel; // 패널을 드래그하여 연결합니다.
    public KeyCode keyToPress; // 상태창을 활성화하는 키
    public GameObject settingsPanel; // 설정 창 패널을 연결

    private Vector2 velocity; // 마우스 이동 속도
    private Vector2 frameVelocity; // 프레임 속도
    private FirstPersonController firstPersonController; // FirstPersonController 스크립트 참조
    private Rigidbody rb; // Rigidbody 참조
    private bool isSettingsOpen = false; // 설정 창 열림 상태 확인 변수

    private TaskUIManager taskUIManager; // TaskUIManager 참조
    private float xRotation = 0f; // 카메라 상하 회전 제한 변수

    // 초기화 시 호출
    void Reset()
    {
        character = GetComponent<FirstPersonController>().transform; // FirstPersonController Transform 연결
    }

    void Start()
    {
        // UI 패널 초기화
        settingsPanel = GameObject.Find("ESC");
        panel = GameObject.Find("Tap");

        settingsPanel.SetActive(false); // 설정 창 초기 비활성화

        // 필요한 컴포넌트 참조
        taskUIManager = GetComponent<TaskUIManager>();
        firstPersonController = GetComponentInParent<FirstPersonController>();
        rb = GetComponentInParent<Rigidbody>();

        if (rb != null)
        {
            rb.freezeRotation = true; // Rigidbody 회전 고정
        }

        Camera playerCamera = GetComponentInChildren<Camera>(); // 자식 오브젝트의 카메라 참조
        if (playerCamera != null)
        {
            playerCamera.gameObject.SetActive(photonView.IsMine); // 자신의 카메라만 활성화
        }

        // 마우스 커서 고정 및 숨기기
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        if (photonView.IsMine) // 자신의 캐릭터일 경우만 동작
        {
            // ESC 키로 설정 창 토글
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ToggleSettingsPanel();
            }

            // 설정 창이 닫혀 있을 때만 캐릭터 회전 가능
            if (!isSettingsOpen)
            {
                RotateCharacter();
            }
            else if (rb != null)
            {
                rb.angularVelocity = Vector3.zero; // 설정 창 열림 시 Rigidbody 각속도 초기화
            }

            // 특정 키 입력으로 패널 활성화
            if (Input.GetKey(keyToPress))
            {
                panel.SetActive(true);
                taskUIManager.UpdateUI();
            }
                else
                {
                    panel.SetActive(false);
                }
        }
    }

    // 캐릭터 회전 메서드
    private void RotateCharacter()
    {
        if (photonView.IsMine) // 자신의 캐릭터일 경우만 동작
        {
            float mouseX = Input.GetAxis("Mouse X") * sensitivity * Time.deltaTime * 100; // 마우스 X 이동
            float mouseY = Input.GetAxis("Mouse Y") * sensitivity * Time.deltaTime * 100; // 마우스 Y 이동

            xRotation -= mouseY; // 카메라 상하 회전값 조정
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // 상하 회전 제한

            playerCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // 카메라 상하 회전
            character.Rotate(Vector3.up * mouseX); // 캐릭터 좌우 회전

            /*
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);

            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
            */
        }
    }

    // FirstPersonController 활성화/비활성화 메서드
    private void ToggleFirstPersonController(bool isActive)
    {
        if (firstPersonController != null)
        {
            firstPersonController.enabled = isActive;
        }
    }

    // 추가한 메서드
    private void ToggleSettingsPanel()
    {
        isSettingsOpen = !isSettingsOpen;
        settingsPanel.SetActive(isSettingsOpen);

        if (isSettingsOpen) // 설정 창이 열렸을 때
        {
            Cursor.visible = true; // 마우스 커서 표시
            Cursor.lockState = CursorLockMode.None; // 마우스 이동 가능
            ToggleFirstPersonController(false); // FirstPersonController 비활성화

            // 움직임 및 소리 중단
            if (firstPersonController != null)
            {
                firstPersonController.StopMovement();
            }
        }
        else // 설정 창이 닫혔을 때
        {
            Cursor.visible = false; // 마우스 커서 숨기기
            Cursor.lockState = CursorLockMode.Locked; // 마우스 고정
            ToggleFirstPersonController(true); // FirstPersonController 활성화

            // 움직임 및 소리 재개
           /* if (firstPersonController != null)
            {
                firstPersonController.ResumeMovement();
            }*/
           
        }
    }
}