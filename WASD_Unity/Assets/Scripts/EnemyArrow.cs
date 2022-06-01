using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ����Ʈ�� �ö��̴��� ���� �����Դϴ�.
// �׸��� �ö��̴��� isTrigger���� true�� ������ �����Դϴ�.
// OnTriggerEnter �Լ��� ����Ѵٴ� �ǹ̸� �����ϴ�.
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

                // ĳ���� ����
                print("ĳ���� ����");
            }
            Destroy(gameObject);
        }
    }
}
