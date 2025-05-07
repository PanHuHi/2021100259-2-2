using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic; // List를 사용하기 위해 필요한 네임스페이스 추가

public class SettingsMenu : MonoBehaviour
{
    public Dropdown resolutionDropdown; // 해상도 드롭다운
    public Toggle fullscreenToggle;     // 전체화면 토글

    private Resolution[] resolutions;   // 시스템에서 사용할 수 있는 해상도 목록 저장

    void Start()
    {
        // 해상도 목록 가져오기
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        // 해상도 목록을 문자열로 변환하여 드롭다운 옵션으로 추가
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            // 현재 해상도와 일치하는 해상도 찾기
            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        // 현재 창모드 설정 반영
        fullscreenToggle.isOn = Screen.fullScreen;

        // 이벤트 리스너 추가
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);
    }

    // 해상도 설정 메서드
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    // 전체화면 설정 메서드
    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}