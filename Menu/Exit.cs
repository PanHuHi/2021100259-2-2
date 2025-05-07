using UnityEngine;

public class Exit : MonoBehaviour
{
    // Settings Panel
    public GameObject settingsPanel;

    // 비활성화 함수
    public void CloseSettings()
    {
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Settings Panel is not assigned!");
        }
    }
}
