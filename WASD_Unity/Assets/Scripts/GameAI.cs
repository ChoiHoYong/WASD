using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ����ڰ� ���� Ŭ������ ����Ƽ �󿡼� ������ �� �ֵ��� ���
[System.Serializable]
public class SkillInfo
{
    public bool update = false;
    public bool execute = false;
    public float coolTime = 10;
    public float elapsedTime = 0;
    // ��ų�� ����� �� �ִ� �Ÿ�
    // ���� ��ų, 
    public float skillDist = 3;
    // ����ؾ��� �ð�
    public float waitTime = 2;
    public float waitingElap = 0;

    // ��ų�� ���ԵǴ� ������ ��ų�� ������ ������ ���� �����ؾ� �մϴ�.

    public void Update()
    {
        if (update == false)
            return;

        // ��ų�� ����� ���¶�� �Լ��� �����մϴ�.
        // ��ų�� �Ϸ�� ������ ��Ÿ���� ����Ǹ� �ȵǱ� ������ �ڵ带 ���� �����ϴ�.
        // update������ true�� �ǰ�, execute������ false�ɶ���
        // ��Ÿ���� ȸ���ǵ��� ó���Ѵ�.
        if (execute)
            return;

        // ��ų�� ����� ���¶�� ��Ÿ���� �����ϵ��� ó���մϴ�.
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

// ���Ͱ� ����ϴ� ��Ʈ�ѷ�
public class GameAI : MonoBehaviour, IFramework
{
    [SerializeField]
    private string targetTag = "Player";
    [SerializeField]
    private string attackAni = "Attack1";

    // ������ ���� ����Ű�� �����Դϴ�.
    [SerializeField]
    private CharState current = CharState.Idle;
    private Model model;

    // �ڽ��� ����
    private CharacterStat characterStat;
    public CharacterStat target;

    [SerializeField]
    // ���� ���� �ð�
    private float attackTime = 3.0f;
    // ������ ������ �����ߴ� �ð���
    private float attackPrevTime = 0.0f;
    // ���� �� �� �ִ� ����
    private bool attackState = false;
    // ������ �� �ִ� �Ÿ�
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
                // Ÿ�� �ִϸ��̼��� ���
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

    // 0��° ��ο��� RedSlash����
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

        // Ÿ���� ��ġ�� ������Ʈ �մϴ�.
        model.SetDestination(target.transform.position);
        // Ÿ�ٰ��� �Ÿ��� ����մϴ�.
        float distance = Vector3.Distance(target.transform.position, transform.position);
        // �� ���� �ݰ氪�� ���մϴ�.
        float radius = characterStat.Radius + target.Radius;

        // �� �𵨸� ������ �Ÿ��� ���� �ߺ��Ǿ��ٸ� �ߺ����� �ʵ��� ó���ϴ� �ڵ�
        if(distance <= radius)
        {
            Vector3 direction = model.transform.position - target.transform.position;
            direction.Normalize();
            Vector3 targetPos = target.transform.position + direction * radius;
            model.SetDestination(targetPos);
        }

        // �� �𵨸� ������ �Ÿ��� üũ������ ������ �� �� �ִ� �Ÿ���� ���¸� �����մϴ�.
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
        // ���۵��ڸ��� ��� ���·� �����ϴ�.
        model.SetFloat("Forward", 0);
        current = CharState.Idle;
        // �� ������Ʈ�� �޾Ƽ� ����� �� �ֵ��� ó���մϴ�.
        characterStat = GetComponent<CharacterStat>();
        if (characterStat != null)
            characterStat.SetModel(model);

        //slashParticle = GetComponentInChildren<ParticleSystem>();
        //slashParticle.Stop();
    }

    void UpdateSkillInfo()
    {
        
        // ��ų�� ���� ��Ÿ��ó��
        for (int i = 0; i < skillList.Count; ++i)
        {
            skillList[i].Update();

        }
        if (currSkill != null)
            return;

        // ���� �������� ������ �� �ִ� ��ų�� �ϳ� �޴´�
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
        UpdateAttackInfo();
        UpdateSkillInfo();
        transform.SendMessage(current.ToString(), SendMessageOptions.DontRequireReceiver);

        model.UpdateAgent();
    }

    public void Die()
    {
        // GameAI ������Ʈ�� üũ�ڽ��� ���ݴϴ�.
        enabled = false;

        // �ö��̴��� ���༭ �ٸ� ��ü�� �浹���� �ʵ��� ó���մϴ�.
        Collider collider = GetComponent<Collider>();
        if (collider != null)
            collider.enabled = false;

        // ���� ĳ���Ͱ� �̵����� �ʵ��� ó���մϴ�.
        model.StopAgent();
        model.SetFloat("Forward", 0);
        // 2�ʵڿ� �ı��ǵ��� ó���մϴ�.
        Destroy(gameObject,2.0f);
        // ĳ���͸� ���� ���·� �����մϴ�.
        characterStat.SetDead(true);
    }
}
