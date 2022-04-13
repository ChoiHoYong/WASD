using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 하이어라키 윈도우에 있는 물체를 찾는 방법
// 1. 이름으로 찾는방법
// 2. 태그 이름으로 찾는 방법
// 3. GameObject.FindObjectOfType

public class Controller : MonoBehaviour
{
    public float yRot = 1;
    public Model model;
    public CrossHair crossHair;

    // 베지어 곡선에 넣어줄 값입니다.
    private Vector3 start;
    private Vector3 pass;
    private Vector3 end;

    // 화살이 발사될 시점
    private Transform arrowStart;
    // 화살 프리팹
    private Arrow arrowPrefab;
    // 캐릭터의 컬라이더
    private Collider charCollider;

    // 최대 발사거리
    private float maximumDistance = 30;

    // 목적지까지 패스할 경로로 사용될 높이
    private float maximumHeight = 7;
    // 타겟이 없을때 발사될 목적지 입니다.
    private Transform dummyTarget;
    // Start, Awake는 사용자가 원하는 시점에 정확히 호출되지 않을 수 있습니다.
    public void Init()
    {
        dummyTarget = transform.Find("DummyTarget");
        // Controller컴포넌트가 연결된 계층의 컬라이더를 찾습니다.
        charCollider = GetComponent<Collider>();
        // 프리팹은 모델을 구성하는 텍스트 파일입니다.
        // FrostMissileTiny 라는 텍스트 파일을 로드해서
        // FrostMissileTiny 에서 사용하는 이미지와 컴포넌트를 배치합니다.
        // 메모리를 로드합니다.
        arrowPrefab = Resources.Load<Arrow>("Prefabs/FrostMissileTiny");

        // 화살이 발사될 시점을 찾습니다.
        GameObject obj = GameObject.Find("ArrowStart");
        if (obj != null)
            arrowStart = obj.transform;
        model = GetComponent<Model>();
        if(model != null)
        {
            model.Init();
        }
        // 현재 하이어라키내에 배치된 크로스헤어 컴포넌트를 찾습니다.
        crossHair = GameObject.FindObjectOfType<CrossHair>();
    }
    void Fire()
    {
        // 발사 처리
        // 시작점을 설정합니다.
        start = arrowStart.position;
        // 시작점과 목표점까지의 거리를 구합니다.
        float distance = Vector3.Distance(start, end);
        // 최대거리 : 최대높이 = 현재거리 : x
        float height = (distance * maximumHeight) / maximumDistance;
        // (0,0,0) + (0,0,5) * 0.5
        pass = (start + end) * 0.5f + Vector3.up * height;
        // 최대거리는 30미터 - 이동에 걸리는 시간 1초 - 
        // 현재거리 / 30 = 도착지점에 도달하는 시간
        float speed = distance / 30;

        Arrow arrow = Instantiate(arrowPrefab, start, Quaternion.identity);
        Collider arrowColider = arrow.GetComponent<Collider>();
        // 화살과 화살을 발사한 캐릭터가 충돌되지 않도록 처리합니다.
        Physics.IgnoreCollision(arrowColider,charCollider);
        // 컬라이더의 체크박스를 켜줍니다.
        arrowColider.enabled = true;

        arrow.Shoot(start, pass, end, 1);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetKey(KeyCode.A))
        {

        }
        if(Input.GetKeyDown(KeyCode.A))
        {

        }
        if (Input.GetKeyUp(KeyCode.A))
        {

        }
        Input.GetAxis("Horizontal");
        Input.GetAxisRaw("Vertical");*/
        // -1 ~ 1까지의 값을 리턴하는 함수입니다.
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        if(horizontal != 0)
            transform.Rotate(Vector3.up, horizontal * yRot * Time.deltaTime);

        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");

        // 오른쪽 버튼이 눌려졌을때 처리합니다.
        if( Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            bool state = crossHair.Cast(out hit);

            if(state)
            {
                end = hit.point;
                // 최대사거리 이상이라면 타겟을 향해 최대사거리로 발사합니다.
                if(Vector3.Distance(transform.position,end)>maximumDistance)
                {
                    // 타겟위치에서 내위치를 빼면 타겟을 가리키는 벡터가 나온다고 합니다.
                    // 방향값만 얻어오기 위해서 normalized를 해서 길이를 1인 벡터로 변경합니다.
                    // (0,0,5) - (0,0,0) = (0,0,5)
                    Vector3 dir = (hit.point - transform.position).normalized;
                    // 내 위치에서 방향벡터(길이가 1인 벡터) * 최대사거리
                    end = transform.position + dir * maximumDistance;
                }
            }
            // 크로스헤어와 충돌되는 지점이 없다면 캐릭터의 전방으로 발사위치를 설정합니다.
            else
            {
                end = dummyTarget.position;
            }
            model.SetTrigger("RangeAttack");
        }

        // z축 값을 먼저 검사해서 애니메이션을 실행합니다.
        if(moveDir.z != 0)
        {
            model.SetFloat("X", 0);
            model.SetFloat("Z", moveDir.z);

            // 뒤로 가기로 설정되었다면 스피드값을 변경합니다.
            if(moveDir.z < 0)
            {

            }
        }
        else if(moveDir.x != 0)
        {
            moveDir.z = 0;
            model.SetFloat("Z", 0);
            model.SetFloat("X", moveDir.x);
        }

        if(moveDir.magnitude < 0.001f)
        {
            model.SetFloat("X", 0);
            model.SetFloat("Z", 0);
        }
        // 벡터의 길이를 1로 만드는 함수입니다.
        moveDir.Normalize();

        transform.Translate(moveDir * Time.deltaTime , Space.Self);

    }
}
