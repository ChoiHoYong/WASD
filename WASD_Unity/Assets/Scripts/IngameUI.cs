using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IngameUI : MonoBehaviour
{
    private TMP_Text gameMessage;
    private Hearts hearts;
    private GameTimer gameTimer;
    private MonsterIconGroup monsterGroup;
    private Minimap minimap;
    //// 삭제할 코드입니다.
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

        //gameTimer.Execute(1);
    }
    public void SetMonster(List<SpawnInfo> spawnList)
    {
        foreach(var spawn in spawnList)
        {
            monsterGroup.AddMonster(spawn.monsterType);
        }
        // 몬스터 타입을 기준으로 정렬시킵니다.
        monsterGroup.Sort();
        // 아이콘을 갱신합니다.
        monsterGroup.UpdateIcons();
        // 몬스터 아이콘이 왼쪽에서 오른쪽으로 나오도록 처리합니다.
        monsterGroup.Show();
    }
    public void DecreaseHP(int point)
    {
        hearts.Play(point);
    }
    public void MissionFailed()
    {
        if (gameMessage != null)
        {
            gameMessage.gameObject.SetActive(true);
            
            Invoke("ChangeMain", 3);
        }
    }
    void ChangeMain()
    {
        PlayerPrefs.DeleteKey("avility1");
        PlayerPrefs.DeleteKey("avility2");
        PlayerPrefs.DeleteKey("avility3");
        GameData.currStage = 1;
        GameData.clearTime = new System.DateTime(0);
        GameData.elapsed = 0;
        GameData.Damage = 1;
        GameData.speed = 2.5f;
        GameData.HP = -1;

        //커서
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        SceneManager.LoadSceneAsync("MainScene");
    }
}