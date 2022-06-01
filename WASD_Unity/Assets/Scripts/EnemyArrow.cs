using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ʈ�� �ö��̴��� ���� �����Դϴ�.
// �׸��� �ö��̴��� isTrigger���� true�� ������ �����Դϴ�.
// OnTriggerEnter �Լ��� ����Ѵٴ� �ǹ̸� �����ϴ�.
public class EnemyArrow : MonoBehaviour
{
    private void Update()
    {
        transform.Translate(Vector3.forward * 0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
