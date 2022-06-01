using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트에 컬라이더를 넣을 예정입니다.
// 그리고 컬라이더의 isTrigger값을 true로 설정할 예정입니다.
// OnTriggerEnter 함수를 사용한다는 의미를 갖습니다.
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
