using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // 게임뷰에 커서를 락을 걸고, 보이지 않도록 처리합니다.
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        GameAudioManager.Setting();
        GameAudioManager.PlayBackground("bg", 0.5F);
        GameObject startPos = GameObject.Find("StartPos");
        if(startPos != null)
        {
            Controller controller = Resources.Load<Controller>("Prefabs/Archer");

            controller = Instantiate(controller, startPos.transform.position,startPos.transform.rotation);
            controller.Init();
        }
        // 2022.04.11 테스트 코드
        GameAI gameAI = GameObject.FindObjectOfType<GameAI>();
        gameAI.Init();
        //GameObject
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
