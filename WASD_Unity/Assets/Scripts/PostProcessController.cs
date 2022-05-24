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

        // DepthOfField�� üũ�ڽ��� ���ֵ��� �����մϴ�.
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
        // PostProcessController �������� ����ǰ� �ִ� ��� �ڷ�ƾ�� �����մϴ�.
        StopAllCoroutines();
        // DepthOfFieldȿ���� �����մϴ�.
        StartCoroutine(IEDepthOfField(speed, start, end));
    }
}
