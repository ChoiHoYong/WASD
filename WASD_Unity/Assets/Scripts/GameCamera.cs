using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCamera : MonoBehaviour
{
    [SerializeField]
    // ī�޶� ������ ������ ����
    private float fovMin = 45;
    [SerializeField]
    // ī�޶� ������ ������ ����
    private float fovMax = 60;
    [SerializeField]
    // Ȯ�� ����Ҷ��� ���ǵ�
    private float fovSpeed = 10;

    // ������ field of view
    private float currFov = 60;

    // ������ x�� ȸ����
    private float currXAngle = 0;

    // x�� ȸ���� ���� ���� ����
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
        //// ������Ʈ�� ã�� �� ���ų�, ĳ���Ͱ� ���� ���¶�� ī�޶� �������� 
        //// �ʵ��� ó���մϴ�.
        //if( healthPoint == null || healthPoint.IsDead )
        //    return;

        // -1 ~ 1���� �Ѱ��ִ� �Լ��Դϴ�.
        currXAngle -= Input.GetAxis("Mouse Y");
        // ȸ������ ���� ������ �����Ѵ�.!
        currXAngle = Mathf.Clamp(currXAngle, XAngleMin, XAngleMax);

        Camera.main.transform.localRotation = Quaternion.Euler(currXAngle, 0, 0);

        // ���콺 ������ ��ư�� ������ �ִٸ� ó���մϴ�.
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
