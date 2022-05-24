using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ü�¿� ���õ� ������Ʈ�Դϴ�.
// ���ݷ�, ����, ġ��Ÿ Ȯ��, ġ��Ÿ ������ ����� �ʿ���� ������ HealthPoint��� �̸��� �۸��Ͽ����ϴ�.
// HealthPoint�� ��� ĳ���Ͱ� �����ִ� ������Ʈ �Դϴ�.
public class CharacterStat : MonoBehaviour
{
    public int point = 3;
    // �� ������Ʈ�� �ʱ�ȭ�� ��Ʈ�ѷ����� �� �� �ʱ�ȭ ���ݴϴ�.
    public Model model;
    private bool isDead = false;
    // �𵨿� �ִ� �ݰ�
    [SerializeField]
    private float radius = 0.2f;
    // ���� ���¸� ����ų �Ӽ���
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    // ���� �ݰ��� ���� �־�� �մϴ�.
    public float Radius
    {
        get
        {
            return radius;
        }
    }

    public void SetDead(bool state)
    {
        isDead = state;
    }

    public void SetModel(Model model)
    {
        this.model = model;
    }
    public void Die()
    {
        isDead = true;
        model.SetTrigger("Death");
        IFramework framework = GetComponent<IFramework>();
        if (framework != null)
            framework.Die();

        // ĳ���Ͱ� ���������� StageManager�� ���ͻ��� �Լ��� ȣ���մϴ�.
        StageManager stageMng = GameObject.FindObjectOfType<StageManager>();
        if (stageMng != null)
            stageMng.ReleaseMonster(this);
    }
}
