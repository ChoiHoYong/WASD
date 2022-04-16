using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���̾��Ű �����쿡 �ִ� ��ü�� ã�� ���
// 1. �̸����� ã�¹��
// 2. �±� �̸����� ã�� ���
// 3. GameObject.FindObjectOfType

public class Controller : MonoBehaviour
{
    public float yRot = 1;
    public Model model;
    public CrossHair crossHair;

    // ������ ��� �־��� ���Դϴ�.
    private Vector3 start;
    private Vector3 pass;
    private Vector3 end;

    // ȭ���� �߻�� ����
    private Transform arrowStart;
    // ȭ�� ������
    private Arrow arrowPrefab;
    // ĳ������ �ö��̴�
    private Collider charCollider;

    // �ִ� �߻�Ÿ�
    private float maximumDistance = 30;

    // ���������� �н��� ��η� ���� ����
    private float maximumHeight = 7;
    // Ÿ���� ������ �߻�� ������ �Դϴ�.
    private Transform dummyTarget;
    // Start, Awake�� ����ڰ� ���ϴ� ������ ��Ȯ�� ȣ����� ���� �� �ֽ��ϴ�.
    public void Init()
    {
        dummyTarget = transform.Find("DummyTarget");
        // Controller������Ʈ�� ����� ������ �ö��̴��� ã���ϴ�.
        charCollider = GetComponent<Collider>();
        // �������� ���� �����ϴ� �ؽ�Ʈ �����Դϴ�.
        // FrostMissileTiny ��� �ؽ�Ʈ ������ �ε��ؼ�
        // FrostMissileTiny ���� ����ϴ� �̹����� ������Ʈ�� ��ġ�մϴ�.
        // �޸𸮸� �ε��մϴ�.
        arrowPrefab = Resources.Load<Arrow>("Prefabs/FrostMissileTiny");

        // ȭ���� �߻�� ������ ã���ϴ�.
        GameObject obj = GameObject.Find("ArrowStart");
        if (obj != null)
            arrowStart = obj.transform;
        model = GetComponent<Model>();
        if(model != null)
        {
            model.Init();
        }
        // ���� ���̾��Ű���� ��ġ�� ũ�ν���� ������Ʈ�� ã���ϴ�.
        crossHair = GameObject.FindObjectOfType<CrossHair>();
    }
    void Fire()
    {
        // �߻� ó��
        // �������� �����մϴ�.
        start = arrowStart.position;
        // �������� ��ǥ�������� �Ÿ��� ���մϴ�.
        float distance = Vector3.Distance(start, end);
        // �ִ�Ÿ� : �ִ���� = ����Ÿ� : x
        float height = (distance * maximumHeight) / maximumDistance;
        // (0,0,0) + (0,0,5) * 0.5
        pass = (start + end) * 0.5f + Vector3.up * height;
        // �ִ�Ÿ��� 30���� - �̵��� �ɸ��� �ð� 1�� - 
        // ����Ÿ� / 30 = ���������� �����ϴ� �ð�
        float speed = distance / 30;

        Arrow arrow = Instantiate(arrowPrefab, start, Quaternion.identity);
        Collider arrowColider = arrow.GetComponent<Collider>();
        // ȭ��� ȭ���� �߻��� ĳ���Ͱ� �浹���� �ʵ��� ó���մϴ�.
        Physics.IgnoreCollision(arrowColider,charCollider);
        // �ö��̴��� üũ�ڽ��� ���ݴϴ�.
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
        // -1 ~ 1������ ���� �����ϴ� �Լ��Դϴ�.
        float horizontal = Input.GetAxis("Mouse X");
        float vertical = Input.GetAxis("Mouse Y");

        if(horizontal != 0)
            transform.Rotate(Vector3.up, horizontal * yRot * Time.deltaTime);

        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal");
        moveDir.z = Input.GetAxis("Vertical");

        // ������ ��ư�� ���������� ó���մϴ�.
        if( Input.GetMouseButtonDown(1))
        {
            RaycastHit hit;
            bool state = crossHair.Cast(out hit);

            if(state)
            {
                end = hit.point;
                // �ִ��Ÿ� �̻��̶�� Ÿ���� ���� �ִ��Ÿ��� �߻��մϴ�.
                if(Vector3.Distance(transform.position,end)>maximumDistance)
                {
                    // Ÿ����ġ���� ����ġ�� ���� Ÿ���� ����Ű�� ���Ͱ� ���´ٰ� �մϴ�.
                    // ���Ⱚ�� ������ ���ؼ� normalized�� �ؼ� ���̸� 1�� ���ͷ� �����մϴ�.
                    // (0,0,5) - (0,0,0) = (0,0,5)
                    Vector3 dir = (hit.point - transform.position).normalized;
                    // �� ��ġ���� ���⺤��(���̰� 1�� ����) * �ִ��Ÿ�
                    end = transform.position + dir * maximumDistance;
                }
            }
            // ũ�ν����� �浹�Ǵ� ������ ���ٸ� ĳ������ �������� �߻���ġ�� �����մϴ�.
            else
            {
                end = dummyTarget.position;
            }
            model.SetTrigger("RangeAttack");
        }

        // z�� ���� ���� �˻��ؼ� �ִϸ��̼��� �����մϴ�.
        if(moveDir.z != 0)
        {
            model.SetFloat("X", 0);
            model.SetFloat("Z", moveDir.z);

            // �ڷ� ����� �����Ǿ��ٸ� ���ǵ尪�� �����մϴ�.
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
        // ������ ���̸� 1�� ����� �Լ��Դϴ�.
        moveDir.Normalize();

        transform.Translate(moveDir * Time.deltaTime , Space.Self);

    }
}
