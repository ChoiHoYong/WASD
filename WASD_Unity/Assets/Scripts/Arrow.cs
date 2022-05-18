using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ʈ�� �ö��̴��� ���� �����Դϴ�.
// �׸��� �ö��̴��� isTrigger���� true�� ������ �����Դϴ�.
// OnTriggerEnter �Լ��� ����Ѵٴ� �ǹ̸� �����ϴ�.
public class Arrow : MonoBehaviour
{
    Vector3 start;
    Vector3 pass;
    Vector3 end;
    // �̵� �ӵ�
    float speed;
    // ������Ʈ�� �Ϸ�Ǿ����� ����ų ���°�
    bool isEnd = false;
    // ��� �ð�
    float elapsed = 0;

    /// <summary>
    /// 2�� ������ �
    /// </summary>
    /// <param name="t">0~1������ �ð���</param>
    /// <param name="P0">���� ��ġ</param>
    /// <param name="P1">�߰� ��ġ</param>
    /// <param name="P2">���� ��ġ</param>
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
    // ��������Ʈ
    // Update is called once per frame
    void Update()
    {
        if (isEnd)
            return;

        elapsed += Time.deltaTime / speed;
        // ������ ��� �ð����� 0�϶� ������ ��ġ
        // 1�϶� ������ ��ġ�� �Ѱ��ִ� � �Լ��Դϴ�.
        transform.position = BezierQuadratic(elapsed, start, pass, end);
        if (elapsed >= 1.0f)
        {
            isEnd = true;
            Destroy(gameObject, 0.3f);
        }
    }
    // �ö��̴��� isTrigger�� true�� üũ�� ��� ���� �� �ִ� �Լ��Դϴ�.
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
