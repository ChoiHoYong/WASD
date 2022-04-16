using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameAI : MonoBehaviour
{
    public enum CharState
    {
        Idle,
        Attack,
        MoveToTarget,
    }

    [SerializeField]
    private string targetTag = "Player";
    [SerializeField]
    private string attackAni = "Attack1";
    [SerializeField]
    // ������ ���� ����Ű�� �����Դϴ�.
    private CharState current = CharState.Idle;
    private Model model;
    public Model target;

    [SerializeField]
    // ���� ���� �ð�
    private float attackTime = 3.0f;
    // ������ ������ �����ߴ� �ð���
    private float attackPrevTime = 0.0f;
    // ���� �� �� �ִ� ����
    private bool attackState = false;
    // ������ �� �ִ� �Ÿ�
    private float attackDistance = 1.5f;

    public virtual void Idle()
    {
        if(target != null)
        {
            current = CharState.MoveToTarget;
        }
    }
    public void Hit()
    {
        if(target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if(distance <= attackDistance)
            {
                HealthPoint healthPoint = target.GetComponent<HealthPoint>();
                --healthPoint.point;
                if(healthPoint.point <= 0)
                {
                    Destroy(target.gameObject);
                    //target.SetDead(true);
                    target = null;

                    // ĳ���� ����
                    print("ĳ���� ����");
                }
            }
        }
    }
    public virtual void Attack()
    {
        if(target == null || target.IsDead)
        {
            // ���¸� ��� ���·� �����ϰ�, ������Ʈ�� ���߸� ��� �ִϸ��̼��� �������� ó��
            current = CharState.Idle;
            model.StopAgent();
            target = null;
            return;
        }
        if(attackState == false)
        {
            attackState = true;
            // Time.time = ����Ƽ �÷��̹�ư�� ���� �����ϰ���� �귯���� �� ������ �ð�
            attackPrevTime = Time.time;
            // Ÿ���� �ٶ󺸵��� �մϴ�.
            model.LookAt(target.transform.position);
            // ���� �ִϸ��̼��� �����մϴ�.
            model.SetTrigger(attackAni);
            return;
        }
        // Ÿ�ٰ��� �Ÿ��� ���մϴ�
        float distance = Vector3.Distance(transform.position, target.transform.position);
        // ������ �� ���� �Ÿ���� Ÿ������ �̵�ó���մϴ�.
        if(distance > attackDistance)
        {
            // ������ ���� ����Ǿ �ִϸ����Ͱ� ������Ʈ �ϴ����̶�� �����մϴ�.
            if (model.IsInTransition())
                return;
            // ���� ����ǰ� �ִ� �ִϸ��̼� ����� �±װ��� ATK��� �����մϴ�.
            if (model.IsTag("ATK"))
                return;
            current = CharState.MoveToTarget;
            model.ResumeAgent();
        }
    }
    public virtual void MoveToTarget()
    {
        // Ÿ���� ��ġ�� ������Ʈ �մϴ�.
        model.SetDestination(target.transform.position);
        // Ÿ�ٰ��� �Ÿ��� ����մϴ�.
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // �� ���� �ݰ氪�� ���մϴ�.
        float radius = model.Radius + target.Radius;

        // �� �𵨸� ������ �Ÿ��� ���� �ߺ��Ǿ��ٸ� �ߺ����� �ʵ��� ó���ϴ� �ڵ�
        if(distance <= radius)
        {
            Vector3 direction = model.transform.position - target.transform.position;
            direction.Normalize();
            Vector3 targetPos = target.transform.position + direction * radius;
            model.SetDestination(targetPos);
        }

        // �� �𵨸� ������ �Ÿ��� üũ������ ������ �� �� �ִ� �Ÿ���� ���¸� �����մϴ�.
        if (distance < attackDistance)
        {
            current = CharState.Attack;
            model.StopAgent();
        }
    }
    // Start is called before the first frame update
    public void Init()
    {
        model = GetComponent<Model>();
        if (model != null)
            model.Init();
        GameObject enemyObj = GameObject.FindGameObjectWithTag(targetTag);
        if (enemyObj != null)
            target = enemyObj.GetComponent<Model>();
        // ���۵��ڸ��� ��� ���·� �����ϴ�.
        model.SetFloat("Forward", 0);
        current = CharState.Idle;
    }

    void UpdateAttackInfo()
    {
        // ������ �� ���¶��, ������ �ð��� �����ڿ� �ٽ� ������ �� �ֵ��� ������Ʈ �մϴ�.
        if(attackState)
        {
            float elapsedTime = Time.time - attackPrevTime;
            if(elapsedTime > attackTime)
            {
                attackState = false;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.SendMessage(current.ToString(), SendMessageOptions.DontRequireReceiver);

        if (model == null)
            return;

        UpdateAttackInfo();

        model.UpdateAgent();
    }
}
