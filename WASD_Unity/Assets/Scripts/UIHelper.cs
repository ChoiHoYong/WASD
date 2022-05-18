using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UIHelper
{
    // 3차원 좌표값을 비율 계산을 통해 ui 좌표계로 변경해주는 함수입니다.
    // 월드맵 사이즈 : ui 사이즈 = 월드 위치 : x (ui위치)
    public static Vector2 WorldPosToMapPos(Vector3 worldPos, float worldWidth, float worldDepth, float uiMapWidth, float uiMapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (worldPos.x * uiMapWidth) / worldWidth;
        result.y = (worldPos.z * uiMapHeight) / worldDepth;
        return result;
    }

    // ui상의 위치를 받아서 월드 사이즈를 곱한 후 크기값으로 나눠서 3D상의 좌표값을 구하는 함수입니다.
    public static Vector3 MapPosToWorldPos(Vector3 uiPos, float worldWidth, float worldDepth, float uiMapWidth, float uiMapHeight)
    {
        Vector3 result = Vector3.zero;
        result.x = (uiPos.x * worldWidth) / uiMapWidth;
        result.y = (uiPos.y * worldDepth) / uiMapHeight;
        return result;
    }
    // 첫번째 매개변수는 3차원 캐릭터의 Transform,
    // 두번째 매개변수는 ui상의 캐릭터 아이콘
    public static void LookAt(Transform world, Transform uiTarget)
    {
        // 라디안 각도 단위를 넘겨주게 된다.
        float angleZ = Mathf.Atan2(world.forward.z, world.forward.x) * Mathf.Rad2Deg;

        uiTarget.eulerAngles = new Vector3(0, 0, angleZ - 90);
    }
}
