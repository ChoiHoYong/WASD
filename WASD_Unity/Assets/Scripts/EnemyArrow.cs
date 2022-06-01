using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 이펙트에 컬라이더를 넣을 예정입니다.
// 그리고 컬라이더의 isTrigger값을 true로 설정할 예정입니다.
// OnTriggerEnter 함수를 사용한다는 의미를 갖습니다.
public class EnemyArrow : MonoBehaviour
{
    CharacterStat target;
    GameAI gameAI;
    GameObject gameObject;
    float attackDistance;
    private void Update()
    {
        transform.Translate(Vector3.forward * 0.1f);
        Destroy(gameObject, 6);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            target = other.gameObject.GetComponent<CharacterStat>();
            IngameUI ingameUI = GameObject.FindObjectOfType<IngameUI>();
            ingameUI.DecreaseHP(target.HP - 1);
            target.HP--;
            Model model = target.GetComponent<Model>();
            model.SetTrigger("Damage");

            if (target.HP <= 0)
            {
                target.Die();
                target = null;

                // 캐릭터 죽음
                print("캐릭터 죽음");
            }
            Destroy(gameObject);
        }
    }
}
