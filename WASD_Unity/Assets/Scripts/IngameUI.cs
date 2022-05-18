using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class IngameUI : MonoBehaviour
{
    private TMP_Text gameMessage;
    private Hearts hearts;
    private GameTimer gameTimer;
    private MonsterIconGroup monsterGroup;
    private Minimap minimap;
    //// ������ �ڵ��Դϴ�.
    //private void Start()
    //{
    //    Init();
    //}
    public void Init()
    {
        gameMessage = UtilHelper.Find<TMP_Text>(transform, "GameMessage",false,false);
        hearts = UtilHelper.Find<Hearts>(transform, "Hearts",true);
        gameTimer = UtilHelper.Find<GameTimer>(transform, "GameTimer", true);
        monsterGroup = UtilHelper.Find<MonsterIconGroup>(transform, "LeftBottom", true);

        minimap = UtilHelper.Find<Minimap>(transform, "Minimap", true);

        gameTimer.Execute(1);
    }
    public void SetMonster(List<SpawnInfo> spawnList)
    {
        foreach(var spawn in spawnList)
        {
            monsterGroup.AddMonster(spawn.monsterType);
        }
        // ���� Ÿ���� �������� ���Ľ�ŵ�ϴ�.
        monsterGroup.Sort();
        // �������� �����մϴ�.
        monsterGroup.UpdateIcons();
        // ���� �������� ���ʿ��� ���������� �������� ó���մϴ�.
        monsterGroup.Show();
    }
    public void DecreaseHP(int point)
    {
        hearts.Play(point);
    }
    public void MissionFailed()
    {
        if (gameMessage != null)
            gameMessage.gameObject.SetActive(true);
    }
}