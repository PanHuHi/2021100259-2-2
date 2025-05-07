using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class OptionsMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject optionsPanel;
    public GameObject tutorialPanel;

    [Header("Buttons")]
    public Button openOptionsButton;
    public Button closeOptionsButton;
    public Button exitButton;
    public Button openTutorialButton;
    public Button closeTutorialButton;

    void Start()
    {
        // 패널 비활성화
        SetPanelActive(optionsPanel, false);
        SetPanelActive(tutorialPanel, false);

        // 버튼 이벤트 설정
        AddButtonListener(openOptionsButton, () => SetPanelActive(optionsPanel, true));
        AddButtonListener(closeOptionsButton, () => SetPanelActive(optionsPanel, false));
        AddButtonListener(openTutorialButton, () => SetPanelActive(tutorialPanel, true));
        AddButtonListener(closeTutorialButton, () => SetPanelActive(tutorialPanel, false));
        AddButtonListener(exitButton, ExitGame);
    }

    void SetPanelActive(GameObject panel, bool isActive)
    {
        if (panel != null)
            panel.SetActive(isActive);
    }

    void AddButtonListener(Button button, UnityEngine.Events.UnityAction action)
    {
        if (button != null)
            button.onClick.AddListener(action);
    }

    void ExitGame()
    {
        Application.Quit();
    }
}