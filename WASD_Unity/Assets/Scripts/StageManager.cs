using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ���� �����ϱ� ���ؼ� �߰�
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // ������ ������ , stage.spawnCount�� ����Ͽ� �ִ� ������ ������ ����
    public SpawnScriptableObject stage;

    // �������� �������� ����� �������� �ʱ� ���ؼ� ������ �����Ͽ����ϴ�.
    public List<SpawnInfo> spawnList = new List<SpawnInfo>();

    // ������ ���� ����
    public List<CharacterStat> monsterList = new List<CharacterStat>();

    private float elapsedTime = 0;
    public List<Transform> spawnPoint = new List<Transform>();

    // ������ �ð��� �����ڿ� ����� �� �ֵ��� ������ �߰��Ͽ����ϴ�.
    private bool update = false;

    public void ReleaseMonster(CharacterStat characterStat)
    {
        if(monsterList.Contains(characterStat))
        {
            monsterList.Remove(characterStat);
            // ���͸� �����ϰ� ���� ������ 0�̶�� �������� Ŭ���� ó���մϴ�.
            if(monsterList.Count == 0)
            {
                print("���� Ŭ����");
                if((GameData.currStage + 1) <= GameData.maxStage)
                {
                    // �������� �̵��� �����Ҷ��� �޼����� ����� ���Ŀ� ���������� �̵��� �� �ִٷ� ó��
                    GameData.currStage++;
                    SceneManager.LoadSceneAsync("Stage" + GameData.currStage.ToString());
                }
                else
                {
                    // ���� ��ü�� Ŭ������ ���¶�� ��� ������ �̵�
                    SceneManager.LoadSceneAsync("End");
                }
                Fade.Instance.FadeOut();
            }
        }
    }

    // Start is called before the first frame update
    public void Init()
    {
        spawnList.AddRange(stage.spawnList);
        // �������� �Ŵ��� �ϴܿ� ��ġ�� �ڽ� Transform�� ã���ϴ�.
        for (int i = 0; i < transform.childCount; ++i)
            spawnPoint.Add(transform.GetChild(i));
        update = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (update == false)
            return;
        // ������ ���Ͱ� ���ٸ� ������Ʈ�� ����� ���ݴϴ�.
        if(spawnList.Count == 0)
        {
            enabled = false;
            return;
        }
        // �ð��� �����մϴ�.
        elapsedTime += Time.deltaTime;
        // �ð����� ���ؼ� ����� �ð��� �� ũ�ٸ� ���͸� �����մϴ�.
        if(spawnList[0].spawnTime <= elapsedTime)
        {
            // Resources.Load�� ����� ���� �Ϲ����� �������̽� ���·� �ε��� �� �����ϴ�.
            // Resources.Load�Լ��� UnityEngine.Object�� ��ӹ��� Ŭ������ ����� �� �ֽ��ϴ�.
            Transform t = Resources.Load<Transform>("Prefabs/" + spawnList[0].monsterType);
            int index = spawnList[0].spawnIndex;
            t = Instantiate(t, spawnPoint[index].position, spawnPoint[index].rotation);
            IFramework framework = t.GetComponent<IFramework>();
            if (framework != null)
                framework.Init();
            // ù��° ����� �����մϴ�.
            spawnList.RemoveAt(0);

            // ���͸� �����մϴ�.
            monsterList.Add(t.GetComponent<CharacterStat>());
        }
    }
}
