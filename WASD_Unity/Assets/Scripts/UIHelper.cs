using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIHelper
{
    // 3���� ��ǥ���� ���� ����� ���� ui ��ǥ��� �������ִ� �Լ��Դϴ�.
    // ����� ������ : ui ������ = ���� ��ġ : x (ui��ġ)
    public static Vector2 WorldPosToMapPos(Vector3 worldPos, float worldWidth, float worldDepth, float uiMapWidth, float uiMapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (worldPos.x * uiMapWidth) / worldWidth;
        result.y = (worldPos.z * uiMapHeight) / worldDepth;
        return result;
    }

    // ui���� ��ġ�� �޾Ƽ� ���� ����� ���� �� ũ�Ⱚ���� ������ 3D���� ��ǥ���� ���ϴ� �Լ��Դϴ�.
    public static Vector3 MapPosToWorldPos(Vector3 uiPos, float worldWidth, float worldDepth, float uiMapWidth, float uiMapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (uiPos.x * worldWidth) / uiMapWidth;
        result.y = (uiPos.y * worldDepth) / uiMapHeight;
        return result;
    }
    // ù��° �Ű������� 3���� ĳ������ Transform,
    // �ι�° �Ű������� ui���� ĳ���� ������
    public static void LookAt(Transform world, Transform uiTarget)
    {
        // ���� ���� ������ �Ѱ��ְ� �ȴ�.
        float angleZ = Mathf.Atan2(world.forward.z, world.forward.x) * Mathf.Rad2Deg;

        uiTarget.eulerAngles = new Vector3(0, 0, angleZ - 90);
    }
}
