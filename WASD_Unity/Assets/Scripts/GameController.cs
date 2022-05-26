using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ��� �������� GameController �Դϴ�.
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
            // Prefabs/Archer ������ �ε��մϴ�.
            // .prefab�̶�� �ؽ�Ʈ ������ �о�鿩�� �ؽ�Ʈ ������ �м��ؼ�
            // �������� ���ҽ�( 3d �𵨸�, �̹���, �ִϸ��̼� )�� �ε��ϴ� ������ �մϴ�.
            controller = Resources.Load<Controller>("Prefabs/Player");

            Transform t = controller.transform.Find("M03");
            t.gameObject.SetActive(false);

            // Instantiate�Լ��� �о���� �޸��� ���纻�� ���� ���̾��Ű �����쿡 �����ϴ� ������ �����մϴ�.
            controller = Instantiate(controller, startPos.transform.position, startPos.transform.rotation);

            Transform root = controller.transform.Find("CameraRoot");
            // ���̾��Ű�� ��ġ�� ����ī�޶��� �θ�μ� ĳ���ͷ� �����մϴ�.
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

        // ������ ���������� �ʱ�ȭ�մϴ�.
        ingameUI.SetMonster(mng.spawnList);
    }
    // Start is called before the first frame update
    void Start()
    {
        //�ɷ� ����Ʈ�� �ʱ�ȭ�մϴ�.
        GameData.avility_list.Clear();
        GameData.isClear = false;
        GameData.isReady = false;

        // ���Ӻ信 Ŀ���� ���� �ɰ�, ������ �ʵ��� ó���մϴ�.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        // Ÿ��Ʋ ȭ���� ������ �ڿ� �Ű��� �����Դϴ�.
        if(fade == null)
        {
            fade = Instantiate(Resources.Load<Fade>("Prefabs/Fade"));
            fade.Init();
            // ���� ����Ǵ��� �ı����� �ʵ��� ó���մϴ�.
            DontDestroyOnLoad(fade.gameObject);
        }
        Fade.Instance.FadeIn();

        GameAudioManager.Instance.Setting();
        GameAudioManager.Instance.PlayBackground("bg", 0.5f);

        // ���̵� ���� �Ϸ�� ���Ŀ� ĳ���Ϳ� ���� ���� �غ� �����մϴ�.
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
