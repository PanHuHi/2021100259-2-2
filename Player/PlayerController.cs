using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float interactionDistance = 3f; // 상호작용 거리
    public GameObject hiddenObject; // 숨겨질 오브젝트

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, interactionDistance))
            {
                if (hit.collider.CompareTag("Haunted")) // 태그 확인(태그는 아무렇게나 바꿔도 됩니다)
                {
                    Destroy(hit.collider.gameObject); // HauntedObject 삭제
                    if (hiddenObject != null)
                    {
                        hiddenObject.SetActive(true); // 숨겨진 오브젝트 활성화
                    }
                }
            }
        }

    }
    // 이게 클릭하면 오브젝트 바뀌는 이벤트



    private void OnTriggerEnter(Collider other)
    {
        // "Event" 태그를 가진 오브젝트와 접촉했을 때(태그는 아무렇게나 바꿔도 됩니다)
        if (other.CompareTag("Event"))
        {
            // 자식 오브젝트 중 TV 태그를 가진 오브젝트 찾기(태그는 아무렇게나 바꿔도 됩니다)
            foreach (Transform child in other.transform)
            {
                foreach (Transform grandChild in child)  // 자식 오브젝트의 자식 오브젝트 찾기
                {
                    if (grandChild.CompareTag("TV")) //(태그는 아무렇게나 바꿔도 됩니다)
                    {
                        // TV 활성화
                        grandChild.gameObject.SetActive(true);

                        // AudioSource 컴포넌트를 찾아서 소리 재생
                        AudioSource audioSource = grandChild.GetComponent<AudioSource>();
                        if (audioSource != null)
                        {
                            audioSource.Play();
                        }
                    }
                    // 이게 콜라이더 진입하면 티비 켜지는 이벤트
                }

                // 1 태그를 가진 자식 오브젝트 찾기
                if (child.CompareTag("1")) // (태그는 아무렇게나 바꿔도 됩니다)
                {
                    // 1 활성화
                    child.gameObject.SetActive(true);
                }
            }
        }
        // 이게 콜라이더 진입하면 뭔가가 나타나는 이벤트
    }

}