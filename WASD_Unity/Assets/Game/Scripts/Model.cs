using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

// 모델 클래스는 NavMeshAgent의 Animator 컴포넌트를 제어할 목적으로
// 사용하는 컴포넌트 입니다.
public class Model : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    // 모델에 있는 반경
    [SerializeField]
    private float radius = 0.2f;

    private bool isDead = false;
    // 모델은 반경을 갖고 있어야 합니다.
    public float Radius
    {
        get
        {
            return radius;
        }
    }
    // 죽은 상태를 가리킬 속성값
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
        // x축 회전이 되지 않도록 y값을 0으로 설정합니다.
        Vector3 direction = position - transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.rotation = Quaternion.LookRotation(direction);
    }
    // 목적지를 설정하는 함수입니다.
    public void SetDestination(Vector3 position)
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
            navMeshAgent.SetDestination(position);
    }
    // Start is called before the first frame update
    // Awake, Start함수는 관리자 클래스에서만 사용하는 것이 좋습니다.
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

    // 실행되고 있는 애니메이션의 태그값이 지정한 tagName과 동일하다면 true값을 리턴하는 함수입니다.
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
        // 아래의 함수들은 값을 변경해주는 역할을 수행합니다
        // animator.SetFloat();
        // animator.SetTrigger();
        // animator.SetBool();
        // 애니메이터의 변수들의 값을 변경하게 되었을때 애니메이터는 변경을 준비하려는
        // 준비단계에 돌입하게 됩니다.
        // IsInTransition함수는 애니메이션의 변경 준비과정에 있을 때 true값을 리턴합니다.
        if (animator.IsInTransition(layer))
            return true;
        return false;
    }

    public void UpdateAgent()
    {
        if(navMeshAgent != null)
        {
            // 캐릭터가 이동하고 있는 방향을 얻습니다. ( AI가 이동하는 방향 )
            Vector3 direction = navMeshAgent.velocity;

            // 캐릭터가 NavMeshAgent가 이동하는 방향을 바라보도록 처리한다.
            direction = transform.InverseTransformDirection(direction);
            
            // 현재 AI가 향할 방향의 Z축 스피드값으로 애니메이션을 실행하도록 처리합니다.
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
