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
    // 현재의 상태 가리키는 변수입니다.
    private CharState current = CharState.Idle;
    private Model model;
    public Model target;

    [SerializeField]
    // 공격 간격 시간
    private float attackTime = 3.0f;
    // 이전에 공격을 시작했던 시간값
    private float attackPrevTime = 0.0f;
    // 공격 할 수 있는 여부
    private bool attackState = false;
    // 공격할 수 있는 거리
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

                    // 캐릭터 죽음
                    print("캐릭터 죽음");
                }
            }
        }
    }
    public virtual void Attack()
    {
        if(target == null || target.IsDead)
        {
            // 상태를 대기 상태로 변경하고, 에이전트를 멈추면 대기 애니메이션이 나오도록 처리
            current = CharState.Idle;
            model.StopAgent();
            target = null;
            return;
        }
        if(attackState == false)
        {
            attackState = true;
            // Time.time = 유니티 플레이버튼을 눌러 시작하고부터 흘러가는 초 단위의 시간
            attackPrevTime = Time.time;
            // 타겟을 바라보도록 합니다.
            model.LookAt(target.transform.position);
            // 공격 애니메이션을 실행합니다.
            model.SetTrigger(attackAni);
            return;
        }
        // 타겟과의 거리를 구합니다
        float distance = Vector3.Distance(transform.position, target.transform.position);
        // 공격할 수 없는 거리라면 타겟으로 이동처리합니다.
        if(distance > attackDistance)
        {
            // 변수의 값이 변경되어서 애니메이터가 업데이트 하는중이라면 종료합니다.
            if (model.IsInTransition())
                return;
            // 지금 실행되고 있는 애니메이션 노드의 태그값이 ATK라면 종료합니다.
            if (model.IsTag("ATK"))
                return;
            current = CharState.MoveToTarget;
            model.ResumeAgent();
        }
    }
    public virtual void MoveToTarget()
    {
        // 타겟의 위치를 업데이트 합니다.
        model.SetDestination(target.transform.position);
        // 타겟과의 거리를 계산합니다.
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // 두 모델의 반경값을 구합니다.
        float radius = model.Radius + target.Radius;

        // 두 모델링 사이의 거리가 서로 중복되었다면 중복되지 않도록 처리하는 코드
        if(distance <= radius)
        {
            Vector3 direction = model.transform.position - target.transform.position;
            direction.Normalize();
            Vector3 targetPos = target.transform.position + direction * radius;
            model.SetDestination(targetPos);
        }

        // 두 모델링 사이의 거리를 체크했을때 공격을 할 수 있는 거리라면 상태를 변경합니다.
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
        // 시작되자마자 대기 상태로 놓습니다.
        model.SetFloat("Forward", 0);
        current = CharState.Idle;
    }

    void UpdateAttackInfo()
    {
        // 공격을 한 상태라면, 일정한 시간이 지난뒤에 다시 공격할 수 있도록 업데이트 합니다.
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
