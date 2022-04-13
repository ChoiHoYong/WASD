using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationListener : MonoBehaviour
{
    void FootL()
    {
        GameAudioManager.Play2D("walking");
    }
    void FootR()
    {
        GameAudioManager.Play2D("walking");
    }
    // �� �Լ��� �ִϸ��̼ǿ��� ȣ��Ǵ� �Լ��Դϴ�.
    // RangeAttack1 �ִϸ��̼ǿ��� �߻��Ǵ� �̺�Ʈ�Դϴ�.
    // Events�� ��ġ�� �����Ͻø� �˴ϴ�.
    void Shoot()
    {
        transform.SendMessageUpwards("Fire",SendMessageOptions.DontRequireReceiver);
    }
    // ���� �ִϸ��̼ǿ��� ȣ��� �Լ��Դϴ�.
    void CheckDamage()
    {
        transform.SendMessageUpwards("Hit", SendMessageOptions.DontRequireReceiver);
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
