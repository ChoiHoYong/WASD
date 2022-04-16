using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// 유니티에서 제공하는 기본 컴포넌트와 c#에서 제공하는 기본 데이터 타입을
// public 으로 작성하면 에디터에서 값을 확인할 수 있습니다.
// 사용자가 만든 데이터 타입은 별도의 처리를 해줘야 합니다.
public class CrossHair : MonoBehaviour
{
    // public
    // 크로스헤어가 배치될 최소 위치, 최대 위치
    [SerializeField] // 에디터창에서 전역처럼 사용할 수 있는 속성
    private float min = 10;

    [SerializeField]
    private float max = 40;

    // 크로스헤어 이미지 상,하,좌,우
    private Image T;
    private Image B;
    private Image L;
    private Image R;

    // 과녁에 적용할 수치값
    private float currSpread = 0;
    
    // 화살을 쏜 상태라면 일정시간 크로스헤어가 감소되는 애니메이션을 보여주도록
    // 처리하기 위한 변수입니다.
    private bool Fire = false;

    // 마우스 왼쪽 버튼을 클릭했을때 벌어질 스피드와
    // 축소될 스피드
    [SerializeField]
    private float upSpeed = 40;
    [SerializeField]
    private float downSpeed = 40;

    [SerializeField]
    private float maxDistance = 100;
    [SerializeField]
    private LayerMask targetLayer;

    // 크로스헤어의 센터 위치
    private Transform center;

    float UpdateCrossHair(float spread)
    {
        currSpread = Mathf.Clamp(spread, min, max); // Clamp : spread값이 min보다 작으면 min 출력

        // 0,1,0 * 40 = 0, 40, 0
        T.transform.localPosition = Vector3.up * spread;
        // 0,-1,0 * 40 = 0, -40, 0
        B.transform.localPosition = Vector3.down * spread;
        // -1,0,0 * 40 = -40, 0, 0
        L.transform.localPosition = Vector3.left * spread;
        // 1,0,0 * 40 = 40, 0, 0
        R.transform.localPosition = Vector3.right * spread;
        return currSpread;
    }
    // Start is called before the first frame update
    void Start()
    {
        // 크로스헤어 컴포넌트 하단에서 해당 이미지를 찾습니다.
        T = UtilHelper.Find<Image>(transform, "T");
        B = UtilHelper.Find<Image>(transform, "B");
        L = UtilHelper.Find<Image>(transform, "L");
        R = UtilHelper.Find<Image>(transform, "R");
        center = transform.Find("Center");
    }


    public bool Cast(Vector3 position, out RaycastHit hit)
    {
        // 유니티에서 스크린 좌표계라는 의미는 왼쪽 하단이 원점이 되는 좌표계
        // 우측으로 x좌표가 증가하고, 위로 y축이 증가하는 좌표계
        Ray ray = Camera.main.ScreenPointToRay(position);

        // 레이와 충돌된 컬라이더가 있는지 체크하는 함수입니다.
        // 충돌처리할 수 있는 레이어를 지정할 수 있다.
        return Physics.Raycast(ray, out hit, maxDistance, targetLayer);
    }

    public bool Cast(out RaycastHit hit)
    {
        return Cast(center.position, out hit);
    }

    void SetColor(Color color)
    {
        T.color = color;
        B.color = color;
        L.color = color;
        R.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        // 마우스의 왼쪽 버튼이 눌려져있는 상태라면 처리합니다.
        if(Input.GetMouseButton(0))
        {
            // 지정한 스피드값으로 증가
            currSpread += upSpeed * Time.deltaTime;
        }
        // 마우스의 왼쪽 버튼이 눌려져있는 상태가 아니라면 처리합니다.
        else
        {
            currSpread -= downSpeed * Time.deltaTime;
        }
        RaycastHit hit;
        // 크로스헤어에 적이 있는지 체크합니다.
        // 적이 있다면 과녁판의 색상을 빨간색으로 변경합니다.
        if(Cast(center.position,out hit))
        {
            // 유니티의 색상에 대한 값 범위는 0 ~ 1 까지의 범위로 사용됩니다.
            // r,g,b,a 순으로 구성됩니다.
            SetColor(Color.red);
        }
        // 적이 없다면 과녁판의 색상을 하얀색으로 변경합니다.
        else
        {
            SetColor(Color.white);
        }
        currSpread = UpdateCrossHair(currSpread);
    }
}
