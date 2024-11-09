using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCtrl : MonoBehaviour
{
    [Header("Camera Settings")]
    public Transform target;         // 추적할 타겟 (플레이어)
    [SerializeField] private float distance = 10f;  // 카메라와 플레이어 간 거리
    [SerializeField] private float height = 5f;    // 카메라의 높이
    [SerializeField] private float leftOffsetValue = -2f; // 카메라가 플레이어 왼쪽으로 이동하는 거리

    // Start is called before the first frame update
    void Start()
    {
        if (target == null)
        {
            target = Camera.main.transform;  // 기본값은 카메라가 주로 따라갈 타겟을 지정
        }

        Cursor.lockState = CursorLockMode.Locked;  // 커서 잠금
        Cursor.visible = false;  // 커서 숨기기
    }

    // Update is called once per frame
    void Update()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        // 카메라의 목표 위치 계산: 플레이어 뒤쪽 + 왼쪽 + 위쪽
        Vector3 leftOffset = -target.right * leftOffsetValue;  // 왼쪽으로 이동하는 거리
        Vector3 heightOffset = Vector3.up * height; // 플레이어 위로 설정된 높이

        // 카메라는 플레이어의 뒤쪽과 왼쪽, 위쪽에 위치하게 됨
        Vector3 targetPosition = target.position - target.forward * distance + leftOffset + heightOffset;

        // 카메라는 플레이어가 바라보는 방향을 따라가야 함 (회전)
        transform.position = targetPosition;

        // 카메라는 플레이어와 같은 방향으로 바라보게 설정 (회전값을 플레이어와 평행하게 유지)
        transform.rotation = Quaternion.Euler(0f, target.eulerAngles.y, 0f);  // 플레이어의 y 회전값을 따라가며 카메라는 평행하게 회전
    }
}

