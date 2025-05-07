using System.Collections;
using UnityEngine;

public class LightController : MonoBehaviour
{
    public Light[] lights;  // 여러 라이트를 담을 배열
    public float delayTime = 5.0f;  // 불을 끄기 전 대기 시간

    private void Start()
    {
        // 일정 시간이 지난 후 라이트를 끄는 코루틴 실행
        StartCoroutine(TurnOffLightsAfterDelay());
    }

    private IEnumerator TurnOffLightsAfterDelay()
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delayTime);

        // 각 라이트의 활성화 상태를 비활성화로 변경 (불을 끄기)
        foreach (Light light in lights)
        {
            light.enabled = false;
        }
    }
}
