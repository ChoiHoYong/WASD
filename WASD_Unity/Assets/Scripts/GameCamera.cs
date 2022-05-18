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

    //private HealthPoint healthPoint;

    // Start is called before the first frame update
    void Start()
    {
        //GameObject player = GameObject.FindGameObjectWithTag("Player");
        //if( player != null )
        //{
        //    healthPoint = player.GetComponent<HealthPoint>();
        //}

    }

    // Update is called once per frame
    void Update()
    {
        //// 컴포넌트를 찾을 수 없거나, 캐릭터가 죽은 상태라면 카메라가 동작하지 
        //// 않도록 처리합니다.
        //if( healthPoint == null || healthPoint.IsDead )
        //    return;

        // -1 ~ 1값을 넘겨주는 함수입니다.
        currXAngle -= Input.GetAxis("Mouse Y");
        // 회전값에 대한 범위를 지정한다.!
        currXAngle = Mathf.Clamp(currXAngle, XAngleMin, XAngleMax);

        Camera.main.transform.localRotation = Quaternion.Euler(currXAngle, 0, 0);

        // 마우스 오른쪽 버튼을 누르고 있다면 처리합니다.
        if (Input.GetMouseButton(1))
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
