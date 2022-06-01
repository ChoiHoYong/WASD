using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 사용자가 만든 클래스를 유니티 상에서 접근할 수 있도록 허용
[System.Serializable]
public class SkillInfo
{
    public bool update = false;
    public bool execute = false;
    public float coolTime = 10;
    public float elapsedTime = 0;
    // 스킬을 사용할 수 있는 거리
    // 버프 스킬, 
    public float skillDist = 3;
    // 대기해야할 시간
    public float waitTime = 2;
    public float waitingElap = 0;

    // 스킬이 진입되는 시점과 스킬이 끝나는 시점을 따로 구별해야 합니다.

    public void Update()
    {
        if (update == false)
            return;

        // 스킬이 실행된 상태라면 함수를 종료합니다.
        // 스킬이 완료될 때까지 쿨타임이 실행되면 안되기 때문에 코드를 막아 놓습니다.
        // update변수가 true이 되고, execute변수가 false될때만
        // 쿨타임이 회복되도록 처리한다.
        if (execute)
            return;

        // 스킬이 실행된 상태라면 쿨타임을 동작하도록 처리합니다.
        elapsedTime += Time.deltaTime;
        if(elapsedTime > coolTime)
        {
            elapsedTime = 0;
            update = false;
        }
    }
}
public enum CharState
{
    Idle,
    Attack,
    MoveToTarget,
}

// 몬스터가 사용하는 컨트롤러
public class GameAI : MonoBehaviour, IFramework
{
    [SerializeField]
    private string targetTag = "Player";
    [SerializeField]
    private string attackAni = "Attack1";

    // 현재의 상태 가리키는 변수입니다.
    [SerializeField]
    private CharState current = CharState.Idle;
    private Model model;

    // 자신의 스탯
    private CharacterStat characterStat;
    public CharacterStat target;

    [SerializeField]
    // 공격 간격 시간
    private float attackTime = 3.0f;
    // 이전에 공격을 시작했던 시간값
    private float attackPrevTime = 0.0f;
    // 공격 할 수 있는 여부
    private bool attackState = false;
    // 공격할 수 있는 거리
    [SerializeField]
    public float attackDistance = 2.0f;

    public List<SkillInfo> skillList = new List<SkillInfo>();

    public SkillInfo currSkill = null;

    public ParticleSystem slashParticle;

    public List<Transform> slashPathList = new List<Transform>();

    public virtual void Idle()
    {
        if(target != null)
        {
            current = CharState.MoveToTarget;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Arrow")
        {
            if (characterStat != null)
            {
                model.SetTrigger("Damage");
                characterStat.HP -= GameData.Damage;
                // 타격 애니메이션을 출력
                if (characterStat.HP <= 0)
                {
                    characterStat.Die();
                }
            }
        }
    }
    public void Hit()
    {
        if(target != null)
        {
            float distance = Vector3.Distance(target.transform.position, transform.position);
            if(distance <= attackDistance)
            {
                CharacterStat characterStat = target.GetComponent<CharacterStat>();
                IngameUI ingameUI = GameObject.FindObjectOfType<IngameUI>();
                ingameUI.DecreaseHP(characterStat.HP - 1);
                characterStat.HP -= GameData.Damage;
                Model model = target.GetComponent<Model>();
                model.SetTrigger("Damage");

                if(characterStat.HP <= 0)
                {
                    characterStat.Die();
                    target = null;
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

    // 0번째 경로에서 RedSlash실행
    //public void ShowSlash(int index)
    //{
    //    slashParticle.Play();
    //}
    public void ShowSlash(string info)
    {
        // 0
        // RedSlash
        string[] arr = info.Split('-');
        Transform t = Resources.Load<Transform>("Prefabs/" + arr[1]);
        int index = 0;
        int.TryParse(arr[0], out index);
        if(t != null)
        {
            t = Instantiate(t, slashPathList[index].position, slashPathList[index].rotation);
            ParticleSystem p = t.GetComponentInChildren<ParticleSystem>(true);
            //float speed = 0;
            //float startSize = 0;
            //float.TryParse(arr[3], out speed);
            //float.TryParse(arr[2], out startSize);
            //var main = p.main;
            //main.simulationSpeed = speed;
            //main.startSize = startSize;
            p.Play();
        }
        //slashParticle.Play();
    }

    public virtual void MoveToTarget()
    {
        if (model.IsInTransition() || model.IsTag("ATK"))
        {
            model.StopAgent();
            return;
        }
        model.ResumeAgent();

        // 타겟의 위치를 업데이트 합니다.
        model.SetDestination(target.transform.position);
        // 타겟과의 거리를 계산합니다.
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // 두 모델의 반경값을 구합니다.
        float radius = characterStat.Radius + target.Radius;

        // 두 모델링 사이의 거리가 서로 중복되었다면 중복되지 않도록 처리하는 코드
        if(distance <= radius)
        {
            Vector3 direction = model.transform.position - target.transform.position;
            direction.Normalize();
            Vector3 targetPos = target.transform.position + direction * radius;
            model.SetDestination(targetPos);
        }

        // 두 모델링 사이의 거리를 체크했을때 공격을 할 수 있는 거리라면 상태를 변경합니다.
        else if (distance < attackDistance)
        {
            current = CharState.Attack;
            model.StopAgent();
        }
    }
    // Start is called before the first frame update
    public void Init()
    {
        currSkill = null;
        model = GetComponent<Model>();
        if (model != null)
            model.Init();
        GameObject enemyObj = GameObject.FindGameObjectWithTag(targetTag);
        if (enemyObj != null)
            target = enemyObj.GetComponent<CharacterStat>();
        // 시작되자마자 대기 상태로 놓습니다.
        model.SetFloat("Forward", 0);
        current = CharState.Idle;
        // 모델 컴포넌트를 받아서 사용할 수 있도록 처리합니다.
        characterStat = GetComponent<CharacterStat>();
        if (characterStat != null)
            characterStat.SetModel(model);

        //slashParticle = GetComponentInChildren<ParticleSystem>();
        //slashParticle.Stop();
    }

    void UpdateSkillInfo()
    {
        
        // 스킬에 대한 쿨타임처리
        for (int i = 0; i < skillList.Count; ++i)
        {
            skillList[i].Update();

        }
        if (currSkill != null)
            return;

        // 현재 시점에서 실행할 수 있는 스킬을 하나 받는다
        for(int i = 0;i<skillList.Count;++i)
        {
            if(skillList[i].update == false)
            {
                currSkill = skillList[i];
                current = CharState.MoveToTarget;
            }
        }
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
        UpdateAttackInfo();
        UpdateSkillInfo();
        transform.SendMessage(current.ToString(), SendMessageOptions.DontRequireReceiver);

        model.UpdateAgent();
    }

    public void Die()
    {
        // GameAI 컴포넌트의 체크박스를 꺼줍니다.
        enabled = false;

        // 컬라이더를 꺼줘서 다른 물체와 충돌되지 않도록 처리합니다.
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        // 죽은 캐릭터가 이동되지 않도록 처리합니다.
        model.StopAgent();
        model.SetFloat("Forward", 0);
        // 2초뒤에 파괴되도록 처리합니다.
        Destroy(gameObject,2.0f);
        // 캐릭터를 죽은 상태로 변경합니다.
        characterStat.SetDead(true);
    }
}
