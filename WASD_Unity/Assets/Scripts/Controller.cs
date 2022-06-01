using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���̾��Ű �����쿡 �ִ� ��ü�� ã�� ��� 
// 1. �̸����� ã�¹��
// 2. �±� �̸����� ã�� ���
// 3. GameObject.FindObjectOfType

// ������ ����ϴ� ��Ʈ�ѷ�
public class Controller : MonoBehaviour, IFramework
{
    // �̴ϸʿ� ���� ����� ������ �� �۾��� �߱� ������,
    // Ư�� ���� �̻��� �������� �Ǹ�, ����ũ�� �̹�����
    // ������ ó���� ���� ���Դϴ�.
    public float xMin = -120;
    public float xMax = 120;
    public float zMin = -120;
    public float zMax = 120;

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

    // ���������� �н��� ��η� ���� ���� ( ������ ��� �н��� ��θ� �������� �ʽ��ϴ�. )
    private float maximumHeight = 7;

    // Ÿ���� ������ �߻�� ������ �Դϴ�.
    private Transform dummyTarget;

    private CharacterStat characterStat;

    private float moveSpeed = 0;

    // ĳ������ �𵨸��� �����˴ϴ�.
    public void Die()
    {
        Transform t = transform.Find("M03");
        if (t != null)
            Destroy(t.gameObject, 2.0f);

        // ĳ���͸� ���� ���·� �����մϴ�.
        characterStat.SetDead(true);
        PostProcessController postController = GameObject.FindObjectOfType<PostProcessController>();
        postController.ExecuteDepthOfField(1.0f, 10, 1);

        IngameUI ingameUI = GameObject.FindObjectOfType<IngameUI>();
        if (ingameUI != null)
        {
            ingameUI.MissionFailed();
        }
    }

    // Start, Awake�� ����ڰ� ���ϴ� ������ ��Ȯ�� 
    // ȣ����� ���� �� �ֽ��ϴ�.
    public void Init()
    {

        // ĳ���Ϳ� �ϴܿ� ��ġ�� DummyTarget�� ã���ϴ�.
        dummyTarget = transform.Find("DummyTarget");

        // Controller������Ʈ�� ����� ������ �ö��̴��� ã���ϴ�.
        charCollider = GetComponent<Collider>();
        // �������� ���� �����ϴ� �ؽ�Ʈ �����Դϴ�.
        // FrostMissileTiny ��� �ؽ�Ʈ ������ �ε��ؼ�
        // FrostMissileTiny ���� ����ϴ� �̹����� ������Ʈ�� ��ġ�մϴ�.
        // �޸𸮸� �ε��մϴ�.
        arrowPrefab = Resources.Load<Arrow>("Prefabs/FrostMissileTiny");

        FirePos firePos = GetComponentInChildren<FirePos>(true);
        // ȭ���� �߻�� ������ ã���ϴ�.
        //GameObject obj = GameObject.Find("ArrowStart");
        if (firePos != null)
            arrowStart = firePos.transform;

        model = GetComponent<Model>();
        if (model != null)
        {
            model.Init();
        }

        // �� ������Ʈ�� �޾Ƽ� ����� �� �ֵ��� ó���մϴ�.
        characterStat = GetComponent<CharacterStat>();
        if (characterStat != null)
        {
            characterStat.SetModel(model);
        }
        // ���� ���̾��Ű���� ��ġ�� ũ�ν���� ������Ʈ�� ã���ϴ�.
        crossHair = GameObject.FindObjectOfType<CrossHair>();

        Invoke("ShowChar", 1.0f);

        if (GameData.HP == -1)
        {
             GameData.HP = characterStat.HP;
        }
        characterStat.HP = GameData.HP;
    }
    void ShowChar()
    {
        Transform t = transform.Find("M03");
        if (t != null)
            t.gameObject.SetActive(true);
    }

    void Fire()
    {
        // �߻�ó��
        // �������� �����մϴ�.
        start = arrowStart.position;
        // (0,0,0) + (0,0, 5) * 0.5 + (0, 1, 0) * 10

        // �������� ��ǥ�������� �Ÿ��� ���մϴ�. 
        float distance = Vector3.Distance(start, end);
        // �ִ�Ÿ� : �ִ���� =  ���� �Ÿ� : x
        float height = (distance * maximumHeight) / maximumDistance;

        pass = (start + end) * 0.5f + Vector3.up * height;
        // �ִ�Ÿ��� 30���� - �̵��� �ɸ��� �ð� 1�� -
        // ����Ÿ� / 30 = ���������� �����ϴ� �ð�
        float speed = distance / maximumDistance;

        Arrow arrow = Instantiate(arrowPrefab, start, Quaternion.identity);
        Collider arrowCollider = arrow.GetComponent<Collider>();
        // ȭ���, ȭ���� �߻��� ĳ���Ͱ� �浹���� �ʵ��� ó���մϴ�.
        Physics.IgnoreCollision(arrowCollider, charCollider);
        // �ö��̴��� üũ�ڽ��� ���ݴϴ�.
        arrowCollider.enabled = true;

        arrow.Shoot(start, pass, end, speed);

    }

    // Update is called once per frame
    void Update()
    {
        // ������Ʈ�� null���̰ų�, ĳ���Ͱ� ���� ���¶�� �Լ��� �����մϴ�.
        if (characterStat == null || characterStat.IsDead)
        {
            // ĳ���Ͱ� �׾��ٸ� ũ�ν����� ����ī�޶� ã�Ƽ� ����� �����մϴ�.
            // �׸��� ControllerŬ������ ��ɶ��� �����մϴ�.
            if (crossHair != null)
                crossHair.enabled = false;

            GameCamera gameCamera = GameObject.FindObjectOfType<GameCamera>();
            if (gameCamera != null)
                gameCamera.enabled = false;

            enabled = false;
            return;
        }


        // -1, 0, 1������ ���� �����ϴ� �Լ��Դϴ�.
        //  float key =Input.GetAxisRaw("Horizontal");
        float horizontal = Input.GetAxis("Mouse X");
        if (horizontal != 0)
            transform.Rotate(Vector3.up, horizontal * yRot * Time.deltaTime);

        Vector3 moveDir = Vector3.zero;
        moveDir.x = Input.GetAxis("Horizontal"); // 1
        moveDir.z = Input.GetAxis("Vertical");  // 1

        // ���� ��ư�� ���������� ó���մϴ�.
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            bool state = crossHair.Cast(out hit);

            if (state)
            {
                end = hit.point;

                // �ִ��Ÿ� �̻��̶�� Ÿ���� ���� �ִ��Ÿ��� �߻��մϴ�.
                if (Vector3.Distance(transform.position, end) > maximumDistance)
                {
                    // Ÿ����ġ���� ����ġ�� ���� Ÿ���� ����Ű�� ���Ͱ� ���´ٰ� 
                    // �̾߱⸦ �ϰ� �˴ϴ�.
                    // ���Ⱚ�� ������ ���ؼ� normalized�� �ؼ� ���̸� 1�� ���ͷ� 
                    // �����մϴ�.
                    // (0,0,5) - (0,0,0) = (0,0,5) => (0,0,1)
                    Vector3 dir = (hit.point - transform.position).normalized;
                    // �� ��ġ + ���⺤��( ���̰� 1�� ���� ) * �ִ��Ÿ�
                    end = transform.position + dir * maximumDistance;
                }
            }
            // ũ�ν����� �浹�Ǵ� ������ ���ٸ� ĳ������ �������� �߻���ġ�� 
            // �����մϴ�.
            else
            {
                end = dummyTarget.position;
            }
            model.SetTrigger("RangeAttack");
        }

        // z�� ���� ���� �˻��ؼ� �ִϸ��̼��� �����մϴ�.
        if (moveDir.z != 0)
        {
            model.SetFloat("X", 0);
            model.SetFloat("Z", moveDir.z);

            // �ڷ� ����� �����Ǿ��ٸ� ���ǵ尪�� �����մϴ�.
            if (moveDir.z < 0)
            {

            }
        }
        else if (moveDir.x != 0)
        {
            moveDir.z = 0;
            model.SetFloat("Z", 0);
            model.SetFloat("X", moveDir.x);
        }

        if (moveDir.magnitude < 0.001f)
        {
            model.SetFloat("X", 0);
            model.SetFloat("Z", 0);
        }


        moveSpeed = GameData.speed;
        // ������ ���̸� 1�� ����� �Լ��Դϴ�.
        moveDir.Normalize();
        // ���� ����Ǵ� �ִϸ��̼� ����� �±װ��� ATK��� �Լ��� �����մϴ�.
        if (model.IsTag("ATK"))
            return;
        // SetFloat, SetTrigger���� ���ؼ� ������ ���� ���޵Ǿ� ������Ʈ�� �Ϸ��� �غ����̶��
        // �Լ��� �����մϴ�.
        //if( model.IsInTransition() )
        //    return;
        transform.Translate(moveDir * moveSpeed * Time.deltaTime, Space.Self);
        Vector3 position = transform.position;
        position.x = Mathf.Clamp(position.x, xMin, xMax);
        position.z = Mathf.Clamp(position.z, zMin, zMax);
        transform.position = position;
    }
}
