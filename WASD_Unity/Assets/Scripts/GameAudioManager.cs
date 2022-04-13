using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// static
// AudioListener : ��
// AudioSource : �÷��̾� ���
// AudioClip : �������� ���� ����
public static class GameAudioManager
{
    // ��������� ������ ������ҽ�
    private static AudioSource background;
    // ȿ������ ������ �����
    private static Dictionary<string, AudioClip> soundDic = new Dictionary<string, AudioClip>();
    // ��������� ������ �����
    private static Dictionary<string, AudioClip> backgroundDic = new Dictionary<string, AudioClip>();

    private static List<AudioSource> audioList = new List<AudioSource>();

    public static void LoadBackground()
    {
        AudioClip[] clips = Resources.LoadAll<AudioClip>("Sounds/Background");
        // ������ ���� ��ŭ ��ȸ�մϴ�.
        for(int i = 0; i<clips.Length;i++)
        {
            if (backgroundDic.ContainsKey(clips[i].name) == false)
                backgroundDic.Add(clips[i].name, clips[i]);
        }
    }

    public static void Setting()
    {
        LoadBackground();
        GameObject music = new GameObject("Background",typeof(AudioSource));
        // ������ ���� ������Ʈ���� ����� �ҽ��� ����ϴ�.
        background = music.GetComponent<AudioSource>();
        // �ݺ����
        background.loop = true;
        // ���� 0 �̸� 2D, 1 �̸� 3D���� ���
        background.spatialBlend = 0;

        // �ٸ� ������ �̵��Ǵ��� ��������� �����ϱ� ���ؼ�
        // ������ ���ӿ�����Ʈ�� �ı����� �ʵ��� ó���մϴ�.
        Object.DontDestroyOnLoad(music);
    }
    public static void PlayBackground(string name, float volume = 1, float spatialBlend = 0)
    {
        // ������� ���� ������ ��ϵǾ� �ִ��� üũ�մϴ�.
        if(backgroundDic.ContainsKey(name))
        {
            // ���� ���� ������ �޽��ϴ�.
            background.clip = backgroundDic[name];
            // 2D�� ������ ������ �����մϴ�.
            background.spatialBlend = spatialBlend;
            // �Ҹ��� ũ�⸦ �����մϴ�.
            background.volume = volume;
            // ������ �÷����մϴ�.
            background.Play();
        }
    }
    public static void Play2D(string name, float volume = 1)
    {
        if(soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

            if (clip == null)
                return;

            // ������ �ε��� �� �ִٸ� ���� ��Ͽ� �����մϴ�.
            soundDic.Add(name, clip);
        }
        // ���� ����� �� �ִ� ������ҽ��� �޽��ϴ�.
        AudioSource audioSource = Pooling();
        // 2D ������ ������ �� �ִ� ���·� �����մϴ�.
        audioSource.spatialBlend = 0;
        // ����� �ҽ��� ����� ������ �����մϴ�.
        audioSource.clip = soundDic[name];
        audioSource.volume = volume;
        // ������� �����մϴ�.
        audioSource.Play();
        audioSource.transform.SendMessage("DeactiveDelay", soundDic[name].length, SendMessageOptions.DontRequireReceiver);
    }
    public static void Play3D(string name, Vector3 point, float volume = 1)
    {
        if(soundDic.ContainsKey(name) == false)
        {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/Effect/" + name);

            // ������ �ε��� �� �ִٸ� ���� ��Ͽ� �����մϴ�.
            if (clip == null)
            {
                return;
            }
            soundDic.Add(name, clip);
        }
        // ���� ����� �� �ִ� ������ҽ��� �޽��ϴ�.
        AudioSource audioSource = Pooling();
        // 3D ������ ������ �� �ִ� ���·� �����մϴ�.
        audioSource.spatialBlend = 1;
        // ����� �ҽ��� ��ġ�� �����մϴ�.
        audioSource.gameObject.transform.position = point;
        // ����� �ҽ��� ����� ������ �����մϴ�.
        audioSource.clip = soundDic[name];
        audioSource.volume = volume;
        // ������� �����մϴ�.
        audioSource.Play();
    }
    public static AudioSource Pooling()
    {
        AudioSource audioSource = null;
        for(int i = 0;i<audioList.Count;++i)
        {
            if (audioList[i].gameObject.activeSelf == false)
            {
                audioSource = audioList[i];
                audioSource.gameObject.SetActive(true);
                break;
            }
        }
        if(audioSource == null)
        {
            GameObject obj = new GameObject("Sound", typeof(AudioSource), typeof(PoollingObject));
            audioSource = obj.GetComponent<AudioSource>();
            audioSource.playOnAwake = false;
            audioSource.spatialBlend = 1;
            audioList.Add(audioSource);
        }
        return audioSource;
    }
}
