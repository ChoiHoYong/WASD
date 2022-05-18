using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트에 컬라이더를 넣을 예정입니다.
// 그리고 컬라이더의 isTrigger값을 true로 설정할 예정입니다.
// OnTriggerEnter 함수를 사용한다는 의미를 갖습니다.
public class Arrow : MonoBehaviour
{
    Vector3 start;
    Vector3 pass;
    Vector3 end;
    // 이동 속도
    float speed;
    // 업데이트가 완료되었는지 가리킬 상태값
    bool isEnd = false;
    // 경과 시간
    float elapsed = 0;

    /// <summary>
    /// 2차 베지어 곡선
    /// </summary>
    /// <param name="t">0~1까지의 시간값</param>
    /// <param name="P0">시작 위치</param>
    /// <param name="P1">중간 위치</param>
    /// <param name="P2">도착 위치</param>
    /// <returns></returns>
    public Vector3 BezierQuadratic(float t, Vector3 P0, Vector3 P1, Vector3 P2)
    {
        float t2 = (1 - t) * (1 - t);
        return t2 * P0 + 2 * t * (1 - t) * P1 + t * t * P2;
    }

    public void Shoot(Vector3 start, Vector3 pass, Vector3 end, float speed)
    {
        this.start = start;
        this.pass = pass;
        this.end = end;
        this.speed = speed;
        this.isEnd = false;
    }
    // 델리게이트
    // Update is called once per frame
    void Update()
    {
        if (isEnd)
            return;

        elapsed += Time.deltaTime / speed;
        // 베지어 곡선은 시간값이 0일때 시점의 위치
        // 1일때 종점의 위치를 넘겨주는 곡선 함수입니다.
        transform.position = BezierQuadratic(elapsed, start, pass, end);
        if (elapsed >= 1.0f)
        {
            isEnd = true;
            Destroy(gameObject, 0.3f);
        }
    }
    // 컬라이더의 isTrigger를 true로 체크할 경우 사용될 수 있는 함수입니다.
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
