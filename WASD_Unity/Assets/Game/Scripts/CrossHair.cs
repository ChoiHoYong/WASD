using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// ����Ƽ���� �����ϴ� �⺻ ������Ʈ�� c#���� �����ϴ� �⺻ ������ Ÿ����
// public ���� �ۼ��ϸ� �����Ϳ��� ���� Ȯ���� �� �ֽ��ϴ�.
// ����ڰ� ���� ������ Ÿ���� ������ ó���� ����� �մϴ�.
public class CrossHair : MonoBehaviour
{
    // public
    // ũ�ν��� ��ġ�� �ּ� ��ġ, �ִ� ��ġ
    [SerializeField] // ������â���� ����ó�� ����� �� �ִ� �Ӽ�
    private float min = 10;

    [SerializeField]
    private float max = 40;

    // ũ�ν���� �̹��� ��,��,��,��
    private Image T;
    private Image B;
    private Image L;
    private Image R;

    // ���ῡ ������ ��ġ��
    private float currSpread = 0;
    
    // ȭ���� �� ���¶�� �����ð� ũ�ν��� ���ҵǴ� �ִϸ��̼��� �����ֵ���
    // ó���ϱ� ���� �����Դϴ�.
    private bool Fire = false;

    // ���콺 ���� ��ư�� Ŭ�������� ������ ���ǵ��
    // ��ҵ� ���ǵ�
    [SerializeField]
    private float upSpeed = 40;
    [SerializeField]
    private float downSpeed = 40;

    [SerializeField]
    private float maxDistance = 100;
    [SerializeField]
    private LayerMask targetLayer;

    // ũ�ν������ ���� ��ġ
    private Transform center;

    float UpdateCrossHair(float spread)
    {
        currSpread = Mathf.Clamp(spread, min, max); // Clamp : spread���� min���� ������ min ���

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
        // ũ�ν���� ������Ʈ �ϴܿ��� �ش� �̹����� ã���ϴ�.
        T = UtilHelper.Find<Image>(transform, "T");
        B = UtilHelper.Find<Image>(transform, "B");
        L = UtilHelper.Find<Image>(transform, "L");
        R = UtilHelper.Find<Image>(transform, "R");
        center = transform.Find("Center");
    }


    public bool Cast(Vector3 position, out RaycastHit hit)
    {
        // ����Ƽ���� ��ũ�� ��ǥ���� �ǹ̴� ���� �ϴ��� ������ �Ǵ� ��ǥ��
        // �������� x��ǥ�� �����ϰ�, ���� y���� �����ϴ� ��ǥ��
        Ray ray = Camera.main.ScreenPointToRay(position);

        // ���̿� �浹�� �ö��̴��� �ִ��� üũ�ϴ� �Լ��Դϴ�.
        // �浹ó���� �� �ִ� ���̾ ������ �� �ִ�.
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
        // ���콺�� ���� ��ư�� �������ִ� ���¶�� ó���մϴ�.
        if(Input.GetMouseButton(0))
        {
            // ������ ���ǵ尪���� ����
            currSpread += upSpeed * Time.deltaTime;
        }
        // ���콺�� ���� ��ư�� �������ִ� ���°� �ƴ϶�� ó���մϴ�.
        else
        {
            currSpread -= downSpeed * Time.deltaTime;
        }
        RaycastHit hit;
        // ũ�ν��� ���� �ִ��� üũ�մϴ�.
        // ���� �ִٸ� �������� ������ ���������� �����մϴ�.
        if(Cast(center.position,out hit))
        {
            // ����Ƽ�� ���� ���� �� ������ 0 ~ 1 ������ ������ ���˴ϴ�.
            // r,g,b,a ������ �����˴ϴ�.
            SetColor(Color.red);
        }
        // ���� ���ٸ� �������� ������ �Ͼ������ �����մϴ�.
        else
        {
            SetColor(Color.white);
        }
        currSpread = UpdateCrossHair(currSpread);
    }
}
