using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// ui와 TextMeshPro에 접근하기 위해서 두 네임스페이스를 넣어주셔야 합니다.
using UnityEngine.UI;
using TMPro;

public class MonsterIcon : MonoBehaviour
{
    private RawImage rawImage;
    private TMP_Text tmpText;
    private MonsterType monsterType;
    public AnimationCurve animationCurve;
    private Vector3 showPos = Vector3.zero;
    private Vector3 hidePos = Vector3.zero;
    private bool moveTo = false;

    IEnumerator IEMoveTo(Vector3 start, Vector3 end, float speed)
    {
        moveTo = true;
        float elapsed = 0;
        while (true)
        {
            elapsed += Time.deltaTime / speed;
            if (elapsed > 1.0)
                elapsed = 1.0f;
            // 주어진 시간에 해당하는 그래프 값을 얻습니다.
            float graphVal = animationCurve.Evaluate(elapsed);
            Vector3 pos = Vector3.Lerp(start, end, graphVal);
            transform.position = pos;
            if (elapsed >= 1.0f)
                break;
            yield return null;
        }
        moveTo = false;
    }
    private void MoveTo(Vector3 start, Vector3 end, float speed)
    {
        if(! moveTo)
            StartCoroutine(IEMoveTo(start, end, speed));
    }

    public void Show(float targetTime)
    {
        MoveTo(hidePos, showPos, targetTime);
    }

    public void Hide(float targetTime)
    {
        MoveTo(showPos, hidePos, targetTime);
    }

    // 읽기전용 속성
    public MonsterType MonType
    {
        get { return monsterType; }
    }
    // Start is called before the first frame update
    public void Init()
    {
        rawImage = UtilHelper.Find<RawImage>(transform, "RawImage");
        tmpText = UtilHelper.Find<TMP_Text>(transform, "Text (TMP)");

        gameObject.SetActive(false);

        // (200, 300, 0) + (-200,0,0)
        showPos = transform.position;
        hidePos = transform.position + -Vector3.right * 200;
    }

    public void SetInfo(MonsterType monsterType, int number)
    {
        rawImage.texture = Resources.Load<RenderTexture>($"RenderTexture/{monsterType}");
        tmpText.text = "x" + number;
        
        this.monsterType = monsterType;
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
