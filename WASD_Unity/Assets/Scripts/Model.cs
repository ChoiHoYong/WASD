using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// �� Ŭ������ NavMeshAgent�� Animator ������Ʈ�� ������ ��������
// ����ϴ� ������Ʈ �Դϴ�.
public class Model : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    // �𵨿� �ִ� �ݰ�
    [SerializeField]
    private float radius = 0.2f;

    private bool isDead = false;
    // ���� �ݰ��� ���� �־�� �մϴ�.
    public float Radius
    {
        get
        {
            return radius;
        }
    }
    // ���� ���¸� ����ų �Ӽ���
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    public void SetDead(bool state)
    {
        isDead = state;
    }
    public void LookAt(Vector3 position)
    {
        // x�� ȸ���� ���� �ʵ��� y���� 0���� �����մϴ�.
        Vector3 direction = position - transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }
    // �������� �����ϴ� �Լ��Դϴ�.
    public void SetDestination(Vector3 position)
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
            navMeshAgent.SetDestination(position);
    }
    // Start is called before the first frame update
    // Awake, Start�Լ��� ������ Ŭ���������� ����ϴ� ���� �����ϴ�.
    public void Init()
    {
        animator = GetComponentInChildren<Animator>(true);
        navMeshAgent = GetComponent<NavMeshAgent>();
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            radius = collider.bounds.extents.x;
    }
    public void SetFloat(string name, float value)
    {
        if(animator != null)
            animator.SetFloat(name, value);
    }

    public void SetTrigger(string trigger)
    {
        if (animator != null)
            animator.SetTrigger(trigger);
    }

    public void ResetTrigger(string trigger)
    {
        if (animator != null)
            animator.ResetTrigger(trigger);
    }

    // ����ǰ� �ִ� �ִϸ��̼��� �±װ��� ������ tagName�� �����ϴٸ� true���� �����ϴ� �Լ��Դϴ�.
    public bool IsTag(string tagName, int layer = 0)
    {
        AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(layer);
        if (stateInfo.IsTag(tagName))
            return true;
        return false;
    }

    public void StopAgent()
    {
        if (navMeshAgent != null)
            navMeshAgent.isStopped = true;
    }

    public void ResumeAgent()
    {
        if (navMeshAgent != null)
            navMeshAgent.isStopped = false;
    }

    public bool IsInTransition(int layer = 0)
    {
        // �Ʒ��� �Լ����� ���� �������ִ� ������ �����մϴ�
        // animator.SetFloat();
        // animator.SetTrigger();
        // animator.SetBool();
        // �ִϸ������� �������� ���� �����ϰ� �Ǿ����� �ִϸ����ʹ� ������ �غ��Ϸ���
        // �غ�ܰ迡 �����ϰ� �˴ϴ�.
        // IsInTransition�Լ��� �ִϸ��̼��� ���� �غ������ ���� �� true���� �����մϴ�.
        if (animator.IsInTransition(layer))
            return true;
        return false;
    }

    public void UpdateAgent()
    {
        if(navMeshAgent != null)
        {
            // ĳ���Ͱ� �̵��ϰ� �ִ� ������ ����ϴ�. ( AI�� �̵��ϴ� ���� )
            Vector3 direction = navMeshAgent.velocity;

            // ĳ���Ͱ� NavMeshAgent�� �̵��ϴ� ������ �ٶ󺸵��� ó���Ѵ�.
            direction = transform.InverseTransformDirection(direction);
            
            // ���� AI�� ���� ������ Z�� ���ǵ尪���� �ִϸ��̼��� �����ϵ��� ó���մϴ�.
            if(animator != null)
            {
                animator.SetFloat("Forward", direction.z);
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
