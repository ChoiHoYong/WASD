using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessController : MonoBehaviour
{
    public AnimationCurve animationCurve;
    private DepthOfField depthOfField;
    private PostProcessVolume processVolume;

    private void Start()
    {
        processVolume = GetComponent<PostProcessVolume>();
        if(processVolume != null)
        {
            processVolume.profile.TryGetSettings<DepthOfField>(out depthOfField);
            depthOfField.active = false;
            depthOfField.enabled.value = false;
            depthOfField.focusDistance.value = 20;
        }
    }

    IEnumerator IEDepthOfField(float speed, float start, float end)
    {
        float elapsedTime = 0;

        // DepthOfField의 체크박스를 켜주도록 설정합니다.
        depthOfField.enabled.value = true;
        depthOfField.focusDistance.value = start;
        depthOfField.active = true;
        while (true)
        {
            elapsedTime += Time.deltaTime/speed;
            float delta = Mathf.Lerp(start, end, elapsedTime);
            depthOfField.focusDistance.value = delta;
            if (elapsedTime >= 1.0f)
                break;
            yield return null;
        }
    }
    public void ExecuteDepthOfField(float speed, float start,float end)
    {
        // PostProcessController 계층에서 실행되고 있는 모든 코루틴을 종료합니다.
        StopAllCoroutines();
        // DepthOfField효과를 실행합니다.
        StartCoroutine(IEDepthOfField(speed, start, end));
    }
}
