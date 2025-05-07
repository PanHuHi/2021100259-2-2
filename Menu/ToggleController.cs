using UnityEngine;
using UnityEngine.UI;

public class ToggleController : MonoBehaviour
{
    public Toggle myToggle; // Toggle 컴포넌트 연결

    void Start()
    {
        // 초기화: 체크마크를 비활성화
        myToggle.isOn = false;
        myToggle.onValueChanged.AddListener(OnToggleChanged);
    }

    void OnToggleChanged(bool isOn)
    {
        if (isOn)
        {
            // Toggle이 활성화될 때
            Debug.Log("Toggle ON: 체크마크가 보입니다.");
        }
        else
        {
            // Toggle이 비활성화될 때
            Debug.Log("Toggle OFF: 체크마크가 숨겨집니다.");
        }
    }

    void OnDestroy()
    {
        // 이벤트 제거
        myToggle.onValueChanged.RemoveListener(OnToggleChanged);
    }
}