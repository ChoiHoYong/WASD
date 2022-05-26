using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 모든 시작점은 GameController 입니다.
public class GameController : MonoBehaviour
{
    Controller controller;
    CharacterStat characterStat;
    static Fade fade;
    void GameStart()
    {
        //GameAudioManager.Instance.Setting();
        //GameAudioManager.Instance.PlayBackground("bg", 0.5F);
        GameObject startPos = GameObject.Find("StartPos");
        if (startPos != null)
        {
            // Prefabs/Archer 파일을 로드합니다.
            // .prefab이라는 텍스트 파일을 읽어들여서 텍스트 파일을 분석해서
            // 실질적인 리소스( 3d 모델링, 이미지, 애니메이션 )를 로드하는 역할을 합니다.
            controller = Resources.Load<Controller>("Prefabs/Player");

            Transform t = controller.transform.Find("M03");
            t.gameObject.SetActive(false);

            // Instantiate함수는 읽어들인 메모리의 복사본을 만들어서 하이어라키 윈도우에 생성하는 역할을 수행합니다.
            controller = Instantiate(controller, startPos.transform.position, startPos.transform.rotation);

            Transform root = controller.transform.Find("CameraRoot");
            // 하이어라키에 배치된 메인카메라의 부모로서 캐릭터로 선택합니다.
            Camera.main.transform.SetParent(root);
            Camera.main.transform.localPosition = Vector3.zero;
            Camera.main.transform.rotation = Quaternion.identity;
            controller.Init();
            characterStat = controller.GetComponent<CharacterStat>();

        }
        IngameUI ingameUI = GameObject.FindObjectOfType<IngameUI>();
        ingameUI.Init();

        StageManager mng = GameObject.FindObjectOfType<StageManager>();
        mng.Init();

        // 몬스터의 정보값으로 초기화합니다.
        ingameUI.SetMonster(mng.spawnList);
    }
    // Start is called before the first frame update
    void Start()
    {
        //능력 리스트를 초기화합니다.
        GameData.avility_list.Clear();
        GameData.isClear = false;
        GameData.isReady = false;

        // 게임뷰에 커서를 락을 걸고, 보이지 않도록 처리합니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // 타이틀 화면을 구성한 뒤에 옮겨질 내용입니다.
        if(fade == null)
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // 신이 변경되더라도 파괴되지 않도록 처리합니다.
            DontDestroyOnLoad(fade.gameObject);
        }
        Fade.Instance.FadeIn();

        GameAudioManager.Instance.Setting();
        GameAudioManager.Instance.PlayBackground("bg", 0.5f);

        // 페이드 인이 완료된 이후에 캐릭터와 몬스터 생성 준비를 시작합니다.
        Invoke("GameStart", 1.0f);
    }


    // Update is called once per frame
    void Update()
    {
        if(characterStat == null || characterStat.IsDead)
        {
            enabled = false;
        }
    }
}
