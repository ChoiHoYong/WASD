using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    // 카메라 초점을 설정할 변수
    private float fovMin = 45;
    [SerializeField]
    // 카메라 초점을 설정할 변수
    private float fovMax = 60;

    [SerializeField]
    // 확대 축소할때의 스피드
    private float fovSpeed = 10;

    // 현재의 field of view
    private float currFov = 60;
    // 현재의 x축 회전값
    private float currXAngle = 0;
    // x축 회전에 대한 범위 설정
    [SerializeField]
    private float XAngleMin = -5;
    [SerializeField]
    private float XAngleMax = 5;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // -1 ~ 1값을 넘겨주는 함수
        currXAngle -= Input.GetAxis("Mouse Y");
        // 회전값에 대한 범위를 지정한다.
        currXAngle = Mathf.Clamp(currXAngle, XAngleMin, XAngleMax);

        Camera.main.transform.localRotation = Quaternion.Euler(currXAngle, 0, 0);

        // 마우스 왼쪽 버튼을 누르고 있다면 처리합니다.
        if(Input.GetMouseButton(0))
        {
            currFov -= Time.deltaTime * fovSpeed;
            currFov = Mathf.Clamp(currFov, fovMin, fovMax);
        }
        else
        {
            currFov = 60;
        }
        Camera.main.fieldOfView = currFov;
    }
}
