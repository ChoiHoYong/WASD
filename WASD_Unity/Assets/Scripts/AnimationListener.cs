using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    // Archery/Model
    void FootL()
    {
        //GameAudioManager.Instance.Play2D("walking");
    }
    void FootR()
    {
        //GameAudioManager.Instance.Play2D("walking");
    }

    // 이 함수는 애니메이션에서 호출되는 함수입니다.
    // RangeAttack1 애니메이션에서 발생되는 이벤트입니다.
    // Events의 위치를 참고하시면 됩니다.
    void Shoot()
    {
        // SendMessageUpwards함수는 현재 계층부터 상위계층까지에 있는 컴포넌트의 
        // 함수를 호출하는 기능입니다.
        transform.SendMessageUpwards("Fire", SendMessageOptions.DontRequireReceiver);
    }

    // 공격 애니메이션에서 호출될 함수입니다.!
    void CheckDamage()
    {
        transform.SendMessageUpwards("Hit", SendMessageOptions.DontRequireReceiver);
    }

    void Land()
    {

    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
