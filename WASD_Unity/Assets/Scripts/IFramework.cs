using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 자기 자신만의 메모리를 갖지 못하게 됩니다.
// 형식을 강제시키기 위해서 사용됩니다.
// 인터페이스에는 함수의 본문이 있을 수 없습니다.
// 변수 또한 갖고 있을 수 없습니다.
public interface IFramework
{
    void Die();
    void Init();
}
