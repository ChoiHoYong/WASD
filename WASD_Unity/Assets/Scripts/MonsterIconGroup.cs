using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class MonsterInfo
{
    public int number;
    public MonsterType monType;
}

public class MonsterIconGroup : MonoBehaviour
{
    private List<MonsterIcon> monsterIconList = new List<MonsterIcon>();

    private Dictionary<MonsterType, MonsterInfo> monsterDic = new Dictionary<MonsterType, MonsterInfo>();
    private List<MonsterInfo> monsterInfoList = new List<MonsterInfo>();

    private int Comparison(MonsterInfo left, MonsterInfo right)
    {
        return right.monType.CompareTo(left.monType);
    }
    // Start is called before the first frame update
    public void Init()
    {
        monsterIconList.AddRange(GetComponentsInChildren<MonsterIcon>(true));
        foreach (var monster in monsterIconList)
            monster.Init();
    }

    public void AddMonster(MonsterType monsterType)
    {
        // 몬스터 타입이 등록되어 있지 않다면 등록합니다.
        if(! monsterDic.ContainsKey(monsterType))
        {
            MonsterInfo mon = new MonsterInfo();
            mon.monType = monsterType;
            mon.number = 1;
            monsterDic.Add(monsterType, mon);
        }
        // 이미 등록되어 있다면 개수를 증가합니다.
        else
        {
            monsterDic[monsterType].number++;
        }
    }

    public void Sort()
    {
        // 갖고 있는 데이터가 있을수 있으므로 클리어합니다.
        monsterInfoList.Clear();
        foreach(var kVal in monsterDic)
        {
            monsterInfoList.Add(kVal.Value);
        }
        // 데이터를 내림차순으로 정렬합니다.
        monsterInfoList.Sort(Comparison);
    }

    public void UpdateIcons()
    {
        // 갱신된 정보로 몬스터 아이콘을 변경합니다.
        for(int i = 0; i < monsterInfoList.Count; ++i)
        {
            monsterIconList[i].SetInfo(monsterInfoList[i].monType, monsterInfoList[i].number);
        }
    }

    public void Show()
    {
        for(int i = 0; i < monsterIconList.Count; ++i)
        {
            if(monsterIconList[i].isActiveAndEnabled)  
                monsterIconList[i].Show(0.7f + i * 0.2f);
        }
    }

    public void Hide()
    {
        for (int i = 0; i < monsterIconList.Count; ++i)
        {
            if (monsterIconList[i].isActiveAndEnabled)
                monsterIconList[i].Hide(0.5f + i * 0.1f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
