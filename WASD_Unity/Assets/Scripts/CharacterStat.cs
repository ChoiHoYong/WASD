using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 공격력 이동속도 체력에 관련된 컴포넌트입니다.
public class CharacterStat : MonoBehaviour
{
    [SerializeField]
    public int HP = 3;
    // 모델 컴포넌트의 초기화는 컨트롤러에서 한 번 초기화 해줍니다.
    public Model model;
    private bool isDead = false;
    // 모델에 있는 반경
    [SerializeField]
    private float radius = 0.2f;
    // 죽은 상태를 가리킬 속성값
    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    // 모델은 반경을 갖고 있어야 합니다.
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

        // 캐릭터가 죽을때마다 StageManager의 몬스터삭제 함수를 호출합니다.
        StageManager stageMng = GameObject.FindObjectOfType<StageManager>();
        if (stageMng != null)
            stageMng.ReleaseMonster(this);
    }
}
