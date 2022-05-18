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
        // ���� Ÿ���� ��ϵǾ� ���� �ʴٸ� ����մϴ�.
        if(! monsterDic.ContainsKey(monsterType))
        {
            MonsterInfo mon = new MonsterInfo();
            mon.monType = monsterType;
            mon.number = 1;
            monsterDic.Add(monsterType, mon);
        }
        // �̹� ��ϵǾ� �ִٸ� ������ �����մϴ�.
        else
        {
            monsterDic[monsterType].number++;
        }
    }

    public void Sort()
    {
        // ���� �ִ� �����Ͱ� ������ �����Ƿ� Ŭ�����մϴ�.
        monsterInfoList.Clear();
        foreach(var kVal in monsterDic)
        {
            monsterInfoList.Add(kVal.Value);
        }
        // �����͸� ������������ �����մϴ�.
        monsterInfoList.Sort(Comparison);
    }

    public void UpdateIcons()
    {
        // ���ŵ� ������ ���� �������� �����մϴ�.
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
