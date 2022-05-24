using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 신을 변경하기 위해서 추가
using UnityEngine.SceneManagement;

public class StageManager : MonoBehaviour
{
    // 참고할 데이터 , stage.spawnCount를 사용하여 최대 개수를 조정할 예정
    public SpawnScriptableObject stage;

    // 실질적인 스테이지 목록을 수정하지 않기 위해서 변수를 선언하였습니다.
    public List<SpawnInfo> spawnList = new List<SpawnInfo>();

    // 생성된 몬스터 개수
    public List<CharacterStat> monsterList = new List<CharacterStat>();

    private float elapsedTime = 0;
    public List<Transform> spawnPoint = new List<Transform>();

    // 일정한 시간이 지난뒤에 실행될 수 있도록 변수를 추가하였습니다.
    private bool update = false;

    public void ReleaseMonster(CharacterStat characterStat)
    {
        if(monsterList.Contains(characterStat))
        {
            monsterList.Remove(characterStat);
            // 몬스터를 삭제하고 남은 개수가 0이라면 스테이지 클리어 처리합니다.
            if(monsterList.Count == 0)
            {
                print("게임 클리어");
                if((GameData.currStage + 1) <= GameData.maxStage)
                {
                    // 스테이지 이동이 가능할때는 메세지를 출력한 이후에 스테이지를 이동할 수 있다록 처리
                    GameData.currStage++;
                    SceneManager.LoadSceneAsync("Stage" + GameData.currStage.ToString());
                }
                else
                {
                    // 게임 전체를 클리어한 상태라면 결과 신으로 이동
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
        // 스테이지 매니저 하단에 배치된 자식 Transform을 찾습니다.
        for (int i = 0; i < transform.childCount; ++i)
            spawnPoint.Add(transform.GetChild(i));
        update = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (update == false)
            return;
        // 생성할 몬스터가 없다면 컴포넌트의 기능을 꺼줍니다.
        if(spawnList.Count == 0)
        {
            enabled = false;
            return;
        }
        // 시간을 누적합니다.
        elapsedTime += Time.deltaTime;
        // 시간값을 비교해서 경과된 시간이 더 크다면 몬스터를 생성합니다.
        if(spawnList[0].spawnTime <= elapsedTime)
        {
            // Resources.Load를 사용할 때는 일반적인 인터페이스 형태로 로드할 수 없습니다.
            // Resources.Load함수는 UnityEngine.Object를 상속받은 클래스만 사용할 수 있습니다.
            Transform t = Resources.Load<Transform>("Prefabs/" + spawnList[0].monsterType);
            int index = spawnList[0].spawnIndex;
            t = Instantiate(t, spawnPoint[index].position, spawnPoint[index].rotation);
            IFramework framework = t.GetComponent<IFramework>();
            if (framework != null)
                framework.Init();
            // 첫번째 목록을 삭제합니다.
            spawnList.RemoveAt(0);

            // 몬스터를 저장합니다.
            monsterList.Add(t.GetComponent<CharacterStat>());
        }
    }
}
